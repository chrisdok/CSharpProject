using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rent_a_Car
{
    static class Statistic
    {
        internal static void ListOptions()
        {
            string command = "";

            Statistic.WriteMenu();

            while (command != "q" || command != "Q")
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "m":
                        Statistic.ViewEconomy("Month");
                        break;
                    case "y":
                        Statistic.ViewEconomy("Year");
                        break;
                    case "u":
                        Statistic.DisplayUsers();
                        break;
                    case "su":
                        Console.WriteLine("\tInput name of user:");
                        Statistic.DisplayUsers(Console.ReadLine());
                        break;
                    case "v":
                        Statistic.DisplayVehicles();
                        break;
                    case "sv":
                        Console.WriteLine("\tInput identificationnumber of vehicle");
                        Statistic.DisplayVehicles(Console.ReadLine());
                        break;
                    case "q":
                        break;
                    case "Q":
                        break;
                    default:
                        Console.WriteLine("\tInvalid command!");
                        break;
                }
            }
        }

        static void WriteMenu()
        {
            Console.WriteLine("\n\n-------------- Statistics --------------");
            Console.WriteLine("\tm: View monthly economy");
            Console.WriteLine("\ty: View yearly economy");
            Console.WriteLine("\tu: Display a list of all users");
            Console.WriteLine("\tsu: Search for spesific user");
            Console.WriteLine("\tv: Display all vehicles");
            Console.WriteLine("\tsv: Search for spesific vehicle");
            Console.WriteLine("----------------------------------------");
            Console.Write("\tSelect an option: ");
        }

        private static void DisplayVehicles(string vehicleID = "")
        {
            throw new NotImplementedException();
        }

        private static void DisplayUsers(string userName = "")
        {
            throw new NotImplementedException();
        }

        private static void ViewEconomy(string p)
        {
            throw new NotImplementedException();
        }

        

    }
}
