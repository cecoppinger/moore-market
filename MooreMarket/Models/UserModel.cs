using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MooreMarket.Data;

namespace MooreMarket.Models
{
    public enum AccountType
    {
        Vendor,
        User
    }
  public class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        private byte[] StoredSalt { get; set; }
        public AccountType AccountType { get; set; } = AccountType.User;

        public IList<Product> Products { get; set; }
    }
}
