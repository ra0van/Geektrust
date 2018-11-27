namespace Thrones
{
    using System;
    using System.Collections.Generic;
    using Thrones.Core.Abstract;
    using Thrones.Core.Domain;
    using Thrones.Core.Entities;

    public class Program
    {
        public static void Main(string[] args)
        {
            switch ("1")
            {
                case "1":
                    Crown();
                    break;
            }
        }

        private static void Crown()
        {
            Southeros southeros = new Southeros("Southeros");
            Console.WriteLine(southeros.Ruler.Name);
            Console.WriteLine(string.Join(",", southeros.Ruler.GetAllies()));

            IKingdom contendingKingdom = new Kingdom("Space", "gorilla");
            HashSet<IMessage> messages = new HashSet<IMessage>();
            //while (true)
            //{
            //    var input = Console.ReadLine();
            //    if (string.IsNullOrWhiteSpace(input))
            //    {
            //        break;
            //    }

            //    int separator = input.IndexOf(",");
            //    string reciever = input.Substring(0, separator);
            //    string content = input.Substring(separator + 1);

            //    try
            //    {
            //        messages.Add(new Message(contendingKingdom, southeros[reciever], content));
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("\n" + ex.Message + "\nIgnoring the line :" + input + "\n");
            //    }
            //}

            messages.Add(new Message(contendingKingdom, southeros["Air"], "oaaawaala"));
            messages.Add(new Message(contendingKingdom, southeros["Land"], "a1d22n333a4444p"));
            messages.Add(new Message(contendingKingdom, southeros["Ice"], "zmzmzmzaztzozh"));

            IBallot ballot = new Ballot(contendingKingdom, messages);

            try
            {
                southeros.ClaimTheThrone(ballot);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine(southeros.Ruler.Name);
            Console.WriteLine(string.Join(",", southeros.Ruler.GetAllies()));
            Console.ReadLine();
        }
    }
}
