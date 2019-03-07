using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MooreMarket.Data;
using MooreMarket.Models;

namespace MooreMarket.Controllers
{
  [Route("/api/[controller]")]
  [ApiController]
  public class VendorsController : ControllerBase
  {
    private readonly MooreMarketContext _context;

    public VendorsController(MooreMarketContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IList<UserModel>> GetAllVendors()
    {
      return _context.Users
        .Where(u => u.AccountType == AccountType.Vendor)
        .ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<UserModel> GetVendorProducts(int id)
    {
      var vendorProducts = _context.Users
        .Include(u => u.Products)
        .ThenInclude(p => p.Category)
        .SingleOrDefault(u => u.Id == id);

      if(vendorProducts == null)
      {
        return NotFound();
      }

      return vendorProducts;
    }
  }
}