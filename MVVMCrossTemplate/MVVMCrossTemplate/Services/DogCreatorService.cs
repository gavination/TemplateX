using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Services
{
    public class DogCreatorService :IDogCreatorService
    {
        public Dog CreateNewDog()
        {
            Dog dog = new Dog();
            dog.Name = _names[Random(_names.Count)];
            dog.Breed = _breeds[Random(_breeds.Count)];

            return dog;
        }


        private readonly List<string> _names = new List<string>()
        {
            "Tucker",
            "Bear",
            "Duke",
            "Toby",
            "Rocky",
            "Jack",
            "Cooper",
            "Buddy",
            "Charlie",
            "Max",
            "Bailey",
            "Chloe",
            "Sophie",
            "Maggie",
            "Sadie",
            "Lola",
            "Molly",
            "Daisy",
            "Lucy",
            "Bella",
        };

        private readonly List<string> _breeds = new List<string>()
        {
            "Labrador Retriever",
            "English Cocker Spaniel",
            "English Springer Spaniel",
            "German Shepherd",
            "Staffordshire Bull Terrier",
            "Cavalier King Charles Spaniel",
            "Golden Retriever",
            "West Highland White Terrier",
            "Boxer",
            "Boxer Terrier",
            "Border Terrier",
            "Poodle",
            "Chihuahua",
            "Beagle",
            "Shih Tzu",
            "English Cocker Spaniel",
            "Border Collie",
            "Dobermann",
            "Great Dane",
            "Shiba Inu",
            "Maltese",
        };
   
        Random _random = new Random();
        protected int Random(int count)
        {
            return _random.Next(count);
        }


       
    }
}
