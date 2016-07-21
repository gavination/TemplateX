using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MVVMCrossTemplate.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DogsController : Controller
    {
        List<DogProfile> profiles = new List<DogProfile>()
            {
                new DogProfile()
                {
                    Id = 1,
                    Breed = DogBreed.Husky,
                    Description = "Husky's are a wonderful breed.",
                    ImageUrl = "https://flic.kr/p/5vvjGF",
                    Credits = "https://flic.kr/p/5vvjGF"
                },
                new DogProfile()
                {
                    Id = 1,
                    Breed = DogBreed.Lab,
                    Description = "Labs are mans best friend.",
                    ImageUrl = new PathString("/Images/lab.jpg"),
                    Credits = "https://flic.kr/p/g7MayP"
                }
            };

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(profiles);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(profiles.First(x => x.Id == id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
