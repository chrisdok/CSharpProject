using System;
using System.Data.SqlClient;


//CHANGE DUMMY SQL CODE TO ADD CHECK FOR PASSWORD

/**
 * The class of the logged in user, an associate
 **/
public class User 
{
    private static User Instance = null; 
    private Associate LoggedInUser { get; set; }

    public User() 
    {
        Login();
    }

    /**
     * Implements the singleton pattern to easily locate the logged in users object
     **/
    public static User GetInstance()
    {
        if (Instance != null) 
        {
            return Instance;
        }
        else 
        {
            Instance = new User();
            return Instance;
        }
    }

    /**
     * Displays console menu for user to login, uses LoginUser to check username and password input
     **/
    public void Login() 
    {
        string email = "";
        string password = "";

        do
        {
            Console.WriteLine("Email: ");
            email = Console.ReadLine();

            Console.WriteLine("Password: ");
            password = Console.ReadLine();
        }
        while (!LoginUser(email, password));
    }

    /**
     * Logs in user, saves associate object and returns true if successful, else returns false
     **/
    private bool LoginUser(string username, string password)
    {

        using (SqlConnection conn = new SqlConnection())
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

        }

        if (true)
        {
            LoggedInUser = new Associate();
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public static void Logout()
    {

    }

    private bool RegisterUser()
    {
        return true;
    }
}