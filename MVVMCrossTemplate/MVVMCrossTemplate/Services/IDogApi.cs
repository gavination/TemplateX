using MVVMCrossTemplate.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Services
{
    [Headers("Accept: application/json")]
    public interface IDogApi
    {
        [Get("/dogs")]
        Task<List<DogProfile>> GetDogs();
    }
}
