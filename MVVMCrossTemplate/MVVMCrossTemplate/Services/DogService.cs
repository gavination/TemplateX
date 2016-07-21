//Modified from https://github.com/RobGibbens/ResilientServices
//Orignial license:

//The MIT License(MIT)

//Copyright(c) 2014 Rob Gibbens

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.



using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Fusillade;
using MVVMCrossTemplate.Model;
using MVVMCrossTemplate.Services.Infrastructure;
using Plugin.Connectivity;
using Polly;

namespace MVVMCrossTemplate.Services
{
    public class DogService : IDogService
    {
        private readonly IApiService<IDogApi> _apiService;


        public DogService(IApiService<IDogApi> apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<DogProfile>> GetDogs(Priority priority)
        {
            var cache = BlobCache.LocalMachine;
            var cacheddogs = cache.GetAndFetchLatest("dogs", () => GetRemotedogsAsync(priority),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 24, minutes: 0, seconds: 0);
                });

            var dogs = await cacheddogs.FirstOrDefaultAsync();
            return dogs;
        }

        public async Task<DogProfile> GetDog(Priority priority, string slug)
        {
            var cacheddog = BlobCache.LocalMachine.GetAndFetchLatest(slug, () => GetRemotedog(priority, slug), offset =>
            {
                TimeSpan elapsed = DateTimeOffset.Now - offset;
                return elapsed > new TimeSpan(hours: 0, minutes: 30, seconds: 0);
            });

            var dog = await cacheddog.FirstOrDefaultAsync();

            return dog;
        }


        private async Task<List<DogProfile>> GetRemotedogsAsync(Priority priority)
        {
            List<DogProfile> dogs = null;
            Task<List<DogProfile>> dogsTask;
            switch (priority)
            {
                case Priority.Background:
                    dogsTask = _apiService.Background.GetDogs();
                    break;
                case Priority.UserInitiated:
                    dogsTask = _apiService.UserInitiated.GetDogs();
                    break;
                case Priority.Speculative:
                    dogsTask = _apiService.Speculative.GetDogs();
                    break;
                default:
                    dogsTask = _apiService.UserInitiated.GetDogs();
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    dogs = await Policy
                        .Handle<System.Net.WebException>()
                        .WaitAndRetryAsync
                        (
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await dogsTask.ConfigureAwait(false));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dogs;
        }

        public async Task<DogProfile> GetRemotedog(Priority priority, string slug)
        {
            DogProfile dog = null;

            Task<DogProfile> getdogTask;
            switch (priority)
            {
                case Priority.Background:
                    getdogTask = _apiService.Background.GetDog(slug);
                    break;
                case Priority.UserInitiated:
                    getdogTask = _apiService.UserInitiated.GetDog(slug);
                    break;
                case Priority.Speculative:
                    getdogTask = _apiService.Speculative.GetDog(slug);
                    break;
                default:
                    getdogTask = _apiService.UserInitiated.GetDog(slug);
                    break;
            }

            if (CrossConnectivity.Current.IsConnected)
            {
                dog = await Policy
                    .Handle<Exception>()
                    .RetryAsync(retryCount: 5)
                    .ExecuteAsync(async () => await getdogTask.ConfigureAwait(false));
            }

            return dog;
        }

    }
}