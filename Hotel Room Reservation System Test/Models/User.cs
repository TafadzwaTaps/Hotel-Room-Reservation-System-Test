﻿
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hotel_Room_Reservation_System_Test.Models
{
    public class User 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  Id { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? PasswordHash { get; set; }

        public string? FirstName { get; set; } = string.Empty;
            
        public string? LastName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string? PhysicalAddress { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;

        public User()
        {

        }

        public User(string _username, string _email, string _passwordHash, string _firstname, string _lastname, int _phoneNumber, string physicalAddress, string _role)

        {
            UserName = _username;
            Email = _email;
            FirstName = _firstname;
            LastName = _lastname;
            PasswordHash = _passwordHash;
            PhoneNumber = _phoneNumber;
            PhysicalAddress = physicalAddress;
            Role = _role;
        }
    }
}
