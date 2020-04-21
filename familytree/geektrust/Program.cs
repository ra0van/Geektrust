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
        static IPersonStorage personStorage;
        static FamilyGraph FamilyGraph;
        static Relationships relations;

        static void Main(string[] args)
        {
            personStorage = new PersonStorage();
            LoadPeople();

            FamilyGraph = new FamilyGraph(personStorage);
            LoadRelations();

            relations = new Relationships(FamilyGraph);
            using (var reader = new StreamReader(Path.Combine("InputFiles", "Testcase.txt")))
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
                            GetRelation(values);
                        }
                        catch (Exception) { }
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        private static void GetRelation(List<string> values)
        {
            IEnumerable<Person> response = new List<Person>();
            switch (values[2])
            {
                case "Paternal-Uncle":
                    try
                    {
                        response = relations.PaternalUncle(values[1]);
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
                        response = relations.MaternalUncle(values[1]);
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
                        response = relations.PaternalAunt(values[1]);
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
                        response = relations.MaternalAunt(values[1]);
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
                        response = relations.BrotherInLaw(values[1]);
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
                        response = relations.SisterInLaw(values[1]);
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
                        response = relations.Son(values[1]);
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
                        response = relations.Daughter(values[1]);
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
                        response = relations.Siblings(values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Output : PERSON_NOT_FOUND");
                        throw;
                    }
                    break;
            }
            var names = response.Select(m => m.Name);
            string namesAsString = names.Count() == 0 ? "NONE" : string.Join(" ", names);
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

        private static void LoadRelations()
        {
            using (var reader = new StreamReader(Path.Combine("InputFiles", "Relationships.txt")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').Select(m => m.Trim()).ToList();
                    FamilyGraph.AddRelationship(values[0], values[1], values[2]);
                }
            }
        }

        private static void LoadPeople()
        {
            using (var reader = new StreamReader(Path.Combine("InputFiles", "People.txt")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',').Select(m => m.Trim()).ToList();
                    Gender gender = values[1].ToGenderEnum();
                    personStorage.AddPerson(values[0], gender);
                }
            }
        }
    }
}
