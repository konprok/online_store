﻿namespace UserService.Models;

public class UserLoginDto
{
    public UserLoginDto() { }
    public UserLoginDto( string password, string email)
    {
        Email = email;
        Password = password;
    }
    public string Password { get; set; }
    public string Email { get; set; }
}