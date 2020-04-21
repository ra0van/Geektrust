using geektrust.Family.DTO;
using geektrust.Family.Enums;
using geektrust.Family.Extention;
using geektrust.Family.Implementation;
using geektrust.Family.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace geektrust.family
{
    class Program
    {
        static IPersonStore personStore;
        static FamilyGraph FamilyGraph;
        static Relationships FamilyRelationships;

        static void Main(string[] args)
        {
            personStore = new PersonStore();
            LoadPeople();

            FamilyGraph = new FamilyGraph(personStore);
            LoadRelationships();

            FamilyRelationships = new Relationships(FamilyGraph);
            using (var reader = new StreamReader(@"InputFiles\Testcase.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var input = reader.ReadLine();
                    var values = input.Split(" ").Select(m => m.Trim()).ToList();
                    Console.WriteLine($"TestCase: {input}");
                    if (values[0] == "ADD_CHILD")
                    {
                        AddChild(values);
                    }
                    else if (values[0] == "GET_RELATIONSHIP")
                    {
                        try
                        {
                            GetRelationship(values);
                        }
                        catch (Exception) { }
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        private static void GetRelationship(List<string> values)
        {
            IEnumerable<Person> result = new List<Person>();
            switch (values[2])
            {
                case "Paternal-Uncle":
                    try
                    {
                        result = FamilyRelationships.PaternalUncle(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Maternal-Uncle":
                    try
                    {
                        result = FamilyRelationships.MaternalUncle(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Paternal-Aunt":
                    try
                    {
                        result = FamilyRelationships.PaternalAunt(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Maternal-Aunt":
                    try
                    {
                        result = FamilyRelationships.MaternalAunt(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Brother-In-Law":
                    try
                    {
                        result = FamilyRelationships.BrotherInLaw(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Sister-In-Law":
                    try
                    {
                        result = FamilyRelationships.SisterInLaw(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Son":
                    try
                    {
                        result = FamilyRelationships.Son(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Daughter":
                    try
                    {
                        result = FamilyRelationships.Daughter(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
                case "Siblings":
                    try
                    {
                        result = FamilyRelationships.Siblings(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
            }
            var names = result.Select(m => m.Name);
            string namesAsString = names.Count() == 0 ? "NONE"  : string.Join(" ", names);
            Console.WriteLine($"Output : {namesAsString}");
        }

        private static void AddChild(List<string> values)
        {
            Console.Write("Output : ");
            try
            {
                FamilyGraph.AddChild(values[1], values[2], values[3]);
                Console.WriteLine("CHILD_ADDITION_SUCCEEDED");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("CHILD_ADDITION_FAILED");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("PERSON_NOT_FOUND");
            }
        }

        private static void LoadRelationships()
        {
            using (var reader = new StreamReader(@"InputFiles\Relationships.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').Select(m=> m.Trim()).ToList();
                    FamilyGraph.AddRelationship(values[0], values[1], values[2]);
                }
            }
        }

        private static void LoadPeople()
        {
            using (var reader = new StreamReader(@"InputFiles\People.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').Select(m=> m.Trim()).ToList();
                    Gender gender = values[1].ToGenderEnum();
                    personStore.AddPerson(values[0], gender);
                }
            }
        }
    }
}
