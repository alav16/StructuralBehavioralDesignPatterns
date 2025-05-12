/*
You're building a login system for a multiplatform app.
Different platforms or user preferences may require different authentication methods:
Password-based authentication
One-Time Password (OTP)
Biometric (Face ID or Fingerprint)
🎯 Task Description
Implement a system that supports different authentication strategies using the Strategy Design Pattern. The goal is to allow injecting different authentication behaviors at runtime.
Required Strategies:
PasswordAuthStrategy: Verifies username and password.
OtpAuthStrategy: Verifies OTP sent to the user's phone/email.
FaceIdAuthStrategy: Simulates a Face ID scan verification.
🏗️ Structure Hints
IAuthStrategy interface with a method bool Authenticate(User user);
AuthService class that uses an IAuthStrategy to perform authentication.
User class with relevant fields (username, password, phone number, face ID metadata, etc.)
*/
using System;


public class User
{
    public string Username { get; }       
    public string Password { get; }       
    public string PhoneNumber { get; }    
    public string FaceData { get; }       

    public User(string username, string password, string phoneNumber, string faceData)
    {
        Username = username;
        Password = password;
        PhoneNumber = phoneNumber;
        FaceData = faceData;
    }
}


public interface IAuthStrategy
{
    bool Authenticate(User user); 
}


public class PasswordAuthStrategy : IAuthStrategy
{
    public bool Authenticate(User user)
    {
      
        return user.Password == "12345";
    }
}


public class OtpAuthStrategy : IAuthStrategy
{
    public bool Authenticate(User user)
    {
       
        return user.PhoneNumber.Contains("123");
    }
}


public class FaceIdAuthStrategy : IAuthStrategy
{
    public bool Authenticate(User user)
    {
        
        return !string.IsNullOrEmpty(user.FaceData);
    }
}

public class AuthService
{
    private IAuthStrategy _strategy; 

    public AuthService(IAuthStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IAuthStrategy strategy)
    {
        _strategy = strategy;
    }

    public bool Authenticate(User user)
    {
        bool result = _strategy.Authenticate(user);
        
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        var user = new User("AA", "12345", "+374999999", "face-data");

        
        var authService = new AuthService(new PasswordAuthStrategy());
        bool isPasswordAuthSuccess = authService.Authenticate(user);

        
        authService.SetStrategy(new OtpAuthStrategy());
        bool isOtpAuthSuccess = authService.Authenticate(user);

        
        authService.SetStrategy(new FaceIdAuthStrategy());
        bool isFaceAuthSuccess = authService.Authenticate(user);
    }
}