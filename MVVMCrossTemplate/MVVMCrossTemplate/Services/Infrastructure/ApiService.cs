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
using System.Net.Http;
using System.Threading.Tasks;
using Fusillade;
using ModernHttpClient;
using Refit;

namespace MVVMCrossTemplate.Services.Infrastructure
{
    public class ApiService<T> : IApiService<T>
    {
        public ApiService(string apiBaseAddress, Func<Task<string>> getToken)
        {
            if (string.IsNullOrWhiteSpace(apiBaseAddress))
                throw new ArgumentException("Please pass api address.", nameof(apiBaseAddress));
      

            Func<HttpMessageHandler, T> createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(apiBaseAddress)
                };

                return RestService.For<T>(client);
            };

            _background = new Lazy<T>(() => createClient(
                new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler(getToken), Priority.Background)));

            _userInitiated = new Lazy<T>(() => createClient(
                new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler(getToken), Priority.UserInitiated)));

            _speculative = new Lazy<T>(() => createClient(
                new RateLimitedHttpMessageHandler(new AuthenticatedHttpClientHandler(getToken), Priority.Speculative)));
        }

        private readonly Lazy<T> _background;
        private readonly Lazy<T> _userInitiated;
        private readonly Lazy<T> _speculative;

        public T Background
        {
            get { return _background.Value; }
        }

        public T UserInitiated
        {
            get { return _userInitiated.Value; }
        }

        public T Speculative
        {
            get { return _speculative.Value; }
        }
    }
}