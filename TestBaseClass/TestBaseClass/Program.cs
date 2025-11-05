using System;
using System.Collections.Generic;
using static System.Console;

namespace TestBaseClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<AnimalClass> animals = new List<AnimalClass>();

            AnimalClass ani = new AnimalClass(5);

            AnimalClass dogAnimal = new DogClass(4);


            DogClass dog = new DogClass(3);

            dog.Speak(); // Output: Woof!
            dog.DrawAga(); // Output: Age: 3

            WriteLine();

            CatClass cat = new CatClass(2);
            cat.Speak(); // Output: Meow!
            cat.DrawType(); // Output: Animal Type: Cat


            animals.Add(ani);
            animals.Add(dogAnimal);
            animals.Add(dog);
            animals.Add(cat);

            WriteLine();

            foreach (var animal in animals)
            {
                animal.Speak();
                animal.DrawType();
                animal.DrawAga();
                WriteLine();
            }



            List<DogClass> dogs = new List<DogClass>(); 

            dogs.Add(dog);

            //dogs.Add(cat); // Error: CatClass cannot be converted to DogClass
            //dogs.Add(ani); // Error: AnimalClass cannot be converted to DogClass
        }
    }
}
