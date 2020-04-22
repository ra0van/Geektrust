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
        static BaseRelationships relations;

        static void Main(string[] args)
        {
            personStorage = new PersonStorage();
            LoadPeople();

            FamilyGraph = new FamilyGraph(personStorage);
            LoadRelations();

            relations = new BaseRelationships(FamilyGraph);
            using (var reader = new StreamReader(Path.Combine("InputFiles", "Testcase.txt")))
            {
                while (!reader.EndOfStream)
                {
                    var input = reader.ReadLine();
                    var values = input.Split(" ").Select(m => m.Trim()).ToList();
                    // Console.WriteLine($"TestCase: {input}");
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
            //Console.ReadKey();
        }

        private static void GetRelation(List<string> values)
        {
            IEnumerable<PersonDTO> response = new List<PersonDTO>();
            try
            {
                switch (values[2])
                {
                    case "Paternal-Uncle":
                        response = relations.PaternalUncle(values[1]);
                        break;
                    case "Maternal-Uncle":
                        response = relations.MaternalUncle(values[1]);
                        break;
                    case "Paternal-Aunt":
                        response = relations.PaternalAunt(values[1]);
                        break;
                    case "Maternal-Aunt":
                        response = relations.MaternalAunt(values[1]);
                        break;
                    case "Brother-In-Law":
                        response = relations.BrotherInLaw(values[1]);
                        break;
                    case "Sister-In-Law":
                        response = relations.SisterInLaw(values[1]);
                        break;
                    case "Son":
                        response = relations.Son(values[1]);
                        break;
                    case "Daughter":
                        response = relations.Daughter(values[1]);
                        break;
                    case "Siblings":
                        response = relations.Siblings(values[1]);
                        break;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("PERSON_NOT_FOUND");
                throw;
            }

            var names = response.Select(m => m.Name);
            string namesAsString = names.Count() == 0 ? "NONE" : string.Join(" ", names);
            //Console.WriteLine($" {namesAsString}");
            Console.WriteLine($"{namesAsString}");
        }

        private static void AddChild(List<string> values)
        {
            //Console.Write(" ");
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
