using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Web
{
    /// <summary>
    /// Dogs instead of Cats. Becuase we are fighting back.
    /// </summary>
    public class DogProfile
    {
        public int Id { get; set; }
        public DogBreed Breed { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

    public enum DogBreed
    {
        Lab =0,
        Husky,
        WhipIt
    }
}
