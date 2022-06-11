using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Services
{
    public interface IFileDbService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        Animal GetAnimal(string idAnimal);
        Animal AddAnimal(Animal animal);
        int UpdateAnimal(Animal animal, string idAnimal);
        int DeleteAnimal(string idAnimal);
    }
}
