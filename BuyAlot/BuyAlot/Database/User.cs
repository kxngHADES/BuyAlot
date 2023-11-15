using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BuyAlot.Database
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        [Unique]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
    [Table("Products")]
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public decimal ProductPrice { get; set; }
        public int UploadedByUserId { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }

    [Table("OrderItem")]
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int OrderItemId { get; set; }
        [Indexed]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        [Indexed]
        public int UserId { get; set; }
    }
}
