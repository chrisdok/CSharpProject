using System;

/**
 * The class of the logged in user, an associate
 **/
public class User 
{
    private static User Instance = null; 
    private Associate Person { get; set; }

    public User() 
    {

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
}