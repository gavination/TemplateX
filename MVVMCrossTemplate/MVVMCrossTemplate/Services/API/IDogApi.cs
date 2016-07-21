using MVVMCrossTemplate.Model;
using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Services
{
    [Headers("Accept: application/json", "Authorization: Bearer")]
    public interface IDogApi
    {
        [Get("/dogs")]
        Task<List<DogProfile>> GetDogs();

        [Get("/dogs/{id}")]
        Task<DogProfile> GetDog(string id);
    }
}
