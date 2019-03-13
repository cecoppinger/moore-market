using System;
using System.Collections.Generic;
using System.Linq;
using MooreMarket.Helpers;
using MooreMarket.Models;
using MooreMarket.Data;
using Microsoft.EntityFrameworkCore;

namespace MooreMarket.Services
{
  public interface IUserService
  {
    UserModel Authenticate(string username, string password);
    UserModel GetById(int id);
    UserModel Create(UserModel user, string password);
  }

  public class UserService : IUserService
  {
    private readonly MooreMarketContext _context;

    public UserService(MooreMarketContext context)
    {
      _context = context;
    }

    public UserModel Authenticate(string username, string password)
    {
      if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        return null;
      }

      var user = _context.Users
          .Include(u => u.Products)
          .SingleOrDefault(u => u.Username == username);

      if(user == null)
      {
        return null;
      }

      if(!VerifyPasswordHash(password, user.Password, user.StoredSalt))
      {
        return null;
      }

      return user;
    }

    public UserModel GetById(int id)
    {
      return _context.Users.Find(id);
    }

    public UserModel Create(UserModel user, string password)
    {
      if (string.IsNullOrWhiteSpace(password))
      {
        throw new AppException("Password can't be empty");
      }

      if(_context.Users.Any(u => u.Username == user.Username))
      {
        throw new AppException("Username already taken");
      }

      byte[] passwordHash, passwordSalt;
      CreatePasswordHash(password, out passwordHash, out passwordSalt);
      
      user.Password = passwordHash;
      user.StoredSalt = passwordSalt;

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }
    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
      if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
      if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

      using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != storedHash[i]) return false;
        }
      }

      return true;
    }
  }
}
