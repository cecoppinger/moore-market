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

      if(!ValidatePassword(password, user.Password))
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

      _context.Users.Add(user);
      _context.SaveChanges();

      return user;
    }

    private bool ValidatePassword(string password, string userPassword)
    {
      return password == userPassword;
    }
  }
}
