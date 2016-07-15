using System.Collections.Generic;
using System.Threading.Tasks;
using Fusillade;
using MVVMCrossTemplate.Model;

namespace MVVMCrossTemplate.Services
{
    public interface IDogService
    {
        Task<List<DogProfile>> GetDogs(Priority priority);
        Task<DogProfile> GetDog(Priority priority, string id);
    }
}