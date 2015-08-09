using System;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Data.SqlClient;

//ADD REGISTER USER FUNCTION

public class Person
{
    
    private const int SaltValueSize = 4;

    public int PersonID { get; protected set; }
    public string Email { get; protected set; }
    protected string Password { get; set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string DateOfBirth { get; protected set; }
    public string DateRegistered { get; protected set; }

	public Person()
	{
	}

    public Person(System.Data.DataTable table) {
        
        PersonID = (int)table.Rows[0]["UserID"];
        Email = table.Rows[0]["Email"].ToString();
        Password = table.Rows[0]["Password"].ToString();
        FirstName = table.Rows[0]["FirstName"].ToString();
        LastName = table.Rows[0]["LastName"].ToString();
        DateOfBirth = table.Rows[0]["DateOfBirth"].ToString();
        DateRegistered = table.Rows[0]["DateRegistered"].ToString();
    }

    public static bool RegisterPerson()
    {
        string Email, Password, FirstName, LastName, DateOfBirth, DateHired;
        int isEmployee, isCustomer;

        Console.WriteLine("\n\n------------Register User---------------");
        Console.Write("\n\tEmail: ");
        Email = Console.ReadLine();
        Console.Write("\n\tPassword: ");
        Password = Console.ReadLine();
        Console.Write("\n\tFirstname: ");
        FirstName = Console.ReadLine();
        Console.Write("\n\tLastname: ");
        LastName = Console.ReadLine();
        Console.Write("\n\tDate of birth (yyyy-mm-dd): ");
        DateOfBirth = Console.ReadLine();

        Console.Write("\n\tEmployee (0 or 1): ");
        isEmployee = (Console.ReadLine() == "0" ? 0 : 1);
        Console.Write("\n\tCustomer (0 or 1): ");
        isCustomer = (Console.ReadLine() == "0" ? 0 : 1);

        if (isEmployee == 1)
        {
            Console.Write("\n\tDate hired (yyyy-mm-dd): ");
            DateHired = Console.ReadLine();
        }
        else
        {
            DateHired = null;
        }
        try
        {
            using (SqlConnection conn = new SqlConnection())
            {
                // Opens database via connectionstring
                conn.ConnectionString = "Data Source=(localdb)\\MyInstance;Initial Catalog=RentACar;Integrated Security=True";
                conn.Open();

                // Sets up sql query
                SqlCommand command = new SqlCommand(@"INSERT INTO [User] (Email, Password, FirstName, LastName, DateOfBirth, DateRegistered, isEmployee, isCustomer, DateHired) 
                    VALUES  (@Email, @Password, @FirstName, @LastName, @DateOfBirth, GETDATE(), @isEmployee, @isCustomer, @DateHired)", conn);

                // Adds parameters to query variables

                command.Parameters.Add(new SqlParameter("@Email", Email));
                command.Parameters.Add(new SqlParameter("@Password", Password));
                command.Parameters.Add(new SqlParameter("@FirstName", FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", LastName));
                command.Parameters.Add(new SqlParameter("@DateOfBirth", DateTime.ParseExact(DateOfBirth + " 00:00:00.000","yyyy-MM-dd HH:mm:ss.fff", null)));
                command.Parameters.Add(new SqlParameter("@isEmployee", isEmployee));
                command.Parameters.Add(new SqlParameter("@isCustomer", isCustomer));
                command.Parameters.Add(new SqlParameter("@DateHired", (DateHired == null ? (object) DBNull.Value : DateHired)));

                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        Console.WriteLine("\tRegistration successful!");

        return true;
    }

    public static Person RegisterPerson(int PersonID) 
    {
        return new Person();
    }

    public static void DeletePerson(int PersonID)
    {

    }

    /* Generates hash to be used for user passwords
     * @author https://msdn.microsoft.com/en-us/library/aa545602(v=cs.70).aspx */
    public static string GenerateSaltValue()
    {
        UnicodeEncoding utf16 = new UnicodeEncoding();

        if (utf16 != null)
        {
            // Create a random number object seeded from the value
            // of the last random seed value. This is done
            // interlocked because it is a static value and we want
            // it to roll forward safely.

            Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            if (random != null)
            {
                // Create an array of random values.

                byte[] saltValue = new byte[SaltValueSize];

                random.NextBytes(saltValue);

                // Convert the salt value to a string. Note that the resulting string
                // will still be an array of binary values and not a printable string. 
                // Also it does not convert each byte to a double byte.

                string saltValueString = utf16.GetString(saltValue);

                // Return the salt value as a string.

                return saltValueString;
            }
        }

        return null;
    }

    /* Hashes a password using GenerateSaltValue() function
     * @author https://msdn.microsoft.com/en-us/library/aa545602(v=cs.70).aspx */
    private static string HashPassword(string clearData, string saltValue, HashAlgorithm hash)
    {
        UnicodeEncoding encoding = new UnicodeEncoding();

        if (clearData != null && hash != null && encoding != null)
        {
            // If the salt string is null or the length is invalid then
            // create a new valid salt value.

            if (saltValue == null)
            {
                // Generate a salt string.
                saltValue = Person.GenerateSaltValue();
            }

            // Convert the salt string and the password string to a single
            // array of bytes. Note that the password string is Unicode and
            // therefore may or may not have a zero in every other byte.

            byte[] binarySaltValue = new byte[SaltValueSize];

            binarySaltValue[0] = byte.Parse(saltValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[1] = byte.Parse(saltValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[2] = byte.Parse(saltValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[3] = byte.Parse(saltValue.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);

            byte[] valueToHash = new byte[SaltValueSize + encoding.GetByteCount(clearData)];
            byte[] binaryPassword = encoding.GetBytes(clearData);

            // Copy the salt value and the password to the hash buffer.

            binarySaltValue.CopyTo(valueToHash, 0);
            binaryPassword.CopyTo(valueToHash, SaltValueSize);

            byte[] hashValue = hash.ComputeHash(valueToHash);

            // The hashed password is the salt plus the hash value (as a string).

            string hashedPassword = saltValue;

            foreach (byte hexdigit in hashValue)
            {
                hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat);
            }

            // Return the hashed password as a string.

            return hashedPassword;
        }

        return null;
    }
}

public class Associate : Person
{
    private string DateHired { get; set; }

    public Associate() : base () { }

    public Associate(System.Data.DataTable table) : base(table)
    {
        DateHired = table.Rows[0]["DateHired"].ToString();
    }
}

public class Customer : Person
{
    public Customer() : base()
    {

    }
}
