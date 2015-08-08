using System;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

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

    public static int RegisterPerson()
    {
        return 1;
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

    public Associate() : base ()
    {
        
    }
}

public class Customer : Person
{
    public Customer() : base()
    {

    }
}
