using MooreMarket.Models;

namespace MooreMarket.Dtos
{
  public class UserDto
  {
    //public int Id { get; set; }
    public string Username { get; set; }
    //public string Email { get; set; }
    public string Password { get; set; }
    public AccountType AccountType { get; set; }

  }
}