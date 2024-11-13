﻿namespace WebApplication73.Infrastructure.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool Verify(string password, string hashedPassword);
    }
}
