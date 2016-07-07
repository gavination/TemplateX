using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Model
{
    public class DogProfile
    {
        public int Id { get; set; }
        public DogBreed Breed { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Credits { get; internal set; }
    }

    public enum DogBreed
    {
        Lab = 0,
        Husky,
        WhipIt
    }
}
