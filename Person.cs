using System;

public class Person
{
    public int UserID { get; }
    public string Email { get; protected set; }
    protected string Password { get; set; }
    public string FirstName { get; }
    public string LastName { get; }
    public string DateOfBirth { get; }
    public string DateRegistered { get; }

	public Person()
	{
	}

    public static Person GetUsers(int UserID) 
    {
        return new Person();
    }

    public static void DeleteUser(int UserID)
    {

    }
}

public class Associate : Person
{
    private string DateHired { get; }

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
