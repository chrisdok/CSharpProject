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

    private User() 
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

        Console.WriteLine("---------------Login--------------------");

        do
        {
            Console.Write("\tEmail: ");
            email = Console.ReadLine();

            Console.Write("\tPassword: ");
            password = Console.ReadLine();
        }
        while (!LoginUser(email, password));
        Console.WriteLine("\n\tSucessfully logged in!");
    }

    /**
     * Logs in user, saves associate object and returns true if successful, else returns false
     **/
    private bool LoginUser(string email, string password)
    {
        System.Data.DataTable dt = new System.Data.DataTable();

        using (SqlConnection conn = new SqlConnection())
        {
            // Opens database via connectionstring
            conn.ConnectionString = "Data Source=(localdb)\\MyInstance;Initial Catalog=RentACar;Integrated Security=True";
            conn.Open();

            // Sets up sql query
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Email = @0 AND Password = @1", conn);

            // Adds parameters to query variables
            
            command.Parameters.Add(new SqlParameter("@0", email));
            command.Parameters.Add(new SqlParameter("@1", password));

            // Executes query, and displays data 
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    dt.Load(reader); 
                    conn.Close();
                    LoggedInUser = new Associate(dt);
                    return true;
                }
                else
                {
                    Console.WriteLine("\tWrong username/password!");
                    conn.Close();
                    return false;
                }
            }
        }
    }

    public static void Logout()
    {
        Instance = null;
        Console.WriteLine("\n\tSuccessfully logged out!\n");
    }
}