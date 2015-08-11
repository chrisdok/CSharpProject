using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer;
using Rent_a_Car;

//-------------TODO------------------
//REMOVE DUMMY SQL 
//GET RENTING RECORDS/ECONOMY THROUGH DATABASE
//ADD INPUT VALIDATION TO STAT MENU
//FIX PASSWORD HASH N SALT


static class Program
{
    [STAThread]
    static void Main()
    {

        User user = User.GetInstance();

        string command = "";

        while (command != "q" && command != "Q")
        {
            WriteMenu();
            command = Console.ReadLine();

            switch (command)
            {
                case "r":
                    Vehicle.RentAVehicle();    
                    break;
                case "rv":
                    Vehicle.RegisterVehicle();
                    break;
                case "ru": 
                    Person.RegisterPerson();
                    break;
                case "e":
                    Statistic.ListOptions();
                    break;
                case "l": 
                    User.Logout();
                    User.GetInstance();
                    break;
            }
        }


        /*using (SqlConnection conn = new SqlConnection())
        {
            // Opens database via connectionstring
            conn.ConnectionString = "Data Source=(localdb)\\MyInstance;Initial Catalog=RentACar;Integrated Security=True";
            conn.Open();

            // Sets up sql query
            SqlCommand command = new SqlCommand("SELECT * FROM VehicleType WHERE VehicleTypeID = @0", conn);
            
            // Adds parameters to query variables
            command.Parameters.Add(new SqlParameter("@0", 1));

            // Executes query, and displays data 
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("VehicleTypeID\tVehicleTypeName\t");
                while (reader.Read())
                {
                    Console.WriteLine(String.Format("{0} \t | {1}",
                        reader[0], reader[1]));
                }
            }
            Console.WriteLine("Press a button to continue");
            Console.ReadLine();
            
        }*/
        
    }

    static void WriteMenu()
    {
        Console.WriteLine("\n\n-------------- Rent-a-Car --------------");
        Console.WriteLine("\tr: Rent a car");
        Console.WriteLine("\trv: Register vehicle");
        Console.WriteLine("\tru: Register user");
        Console.WriteLine("\te: Economy");
        Console.WriteLine("\tl: Log out user");
        Console.WriteLine("----------------------------------------");
        Console.Write("\tSelect an option: ");
    }
}

