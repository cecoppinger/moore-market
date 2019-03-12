using System;
using System.Collections.Generic;
using System.Linq;
using MooreMarket.Dtos;
using MooreMarket.Models;
using MooreMarket.Services;

namespace MooreMarket.Data 
{
    public class DbInitializer
    {
        public static void Initialize(MooreMarketContext context, IUserService userService)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            UserModel farmer1 = new UserModel
            {
                Username = "farmer1",
                AccountType = AccountType.Vendor
            };
            farmer1 = userService.Create(farmer1, "password");

            UserModel farmer2 = new UserModel
            {
                Username = "farmer2",
                AccountType = AccountType.Vendor
            };
            farmer2 = userService.Create(farmer2, "password");            

            UserModel user1 = new UserModel
            {
                Username = "user1",
                AccountType = AccountType.User
            };
            user1 = userService.Create(user1, "password");

            UserModel user2 = new UserModel
            {
                Username = "user2",
                AccountType = AccountType.User
            };
            user2 = userService.Create(user2, "password");

            // context.Users.Add(farmer1);
            // context.Users.Add(farmer2);
            // context.Users.Add(user1);
            // context.Users.Add(user2);

            // context.SaveChanges();
            
            ProductCategory TestCat1 = new ProductCategory("TestCategory1");
            TestCat1.Id = 1;
            ProductCategory TestCat2 = new ProductCategory("TestCategory2");
            TestCat2.Id = 2;
            context.Categories.Add(TestCat1);
            context.Categories.Add(TestCat2);
            context.SaveChanges();

            Product farmer1Product1 = new Product
            {
                Name = "farmer1's Product1",
                Description = "farmer1 product1 description",
                CategoryId = 1,
                Category = TestCat1,
                Quantity = 1,
                Price = 5.50f,
                IsPerishable = 0,
                UserId = 1
            };

            Product newProduct1 = new Product 
            {
              Name = "Bananas From Far Away",
              Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas lobortis malesuada lacus vitae ultricies. Suspendisse rutrum nulla sit amet mi ullamcorper tempor. Nunc fringilla diam luctus nunc tincidunt, id accumsan elit sollicitudin. Morbi nisi nulla, condimentum quis ultricies sed, congue at nibh. Phasellus massa tellus, dictum in imperdiet sit amet, pellentesque vel nibh. Pellentesque aliquet, justo ac mollis faucibus, ex justo congue ipsum, non blandit ligula arcu at nisl.",
              CategoryId = 1,
              Quantity = 11,
              Price = 1.11f,
              IsPerishable = 1,
              KeepRoom = 1,
              ShelfLifeRoom = "some time",
              KeepFridge = 1,
              ShelfLifeFridge = "something",
              KeepFreezer = 1,
              ShelfLifeFreezer = "something",
              UserId = 1
            };
            context.Add(newProduct1);

            Product newProduct2 = new Product
            {
              Name = "Apples",
              Description = "some apples",
              CategoryId = 1,
              Quantity = 6,
              Price = 12.22f,
              IsPerishable = 1,
              KeepRoom = 1,
              ShelfLifeRoom = "shelf life",
              UserId = 1
            };
            context.Add(newProduct2);

            Product newProduct3 = new Product
            {
              Name = "I'm Always Out of Stock",
              Description = "Does it really matter? I'm always going to be out of stock. No bueno.",
              CategoryId = 2,
              Quantity = 0,
              Price = 3.33f,
              UserId = 2
            };
            context.Add(newProduct3);

            Product newProduct4 = new Product
            {
              Name = "Super Expensive",
              Description = "Is This Love",
              CategoryId = 1,
              Quantity = 420,
              Price = 710,
              UserId = 2
            };
            context.Add(newProduct4);

            Product newProduct5 = new Product
            {
              Name = "Thing",
              Description = "description of thing",
              CategoryId = 2,
              Quantity = 3,
              Price = 7.10f,
              UserId = 1
            };
            context.Add(newProduct5);

            Product newProduct6 = new Product
            {
              Name = "Other thing",
              Description = "Is This Love",
              CategoryId = 2,
              Quantity = 420,
              Price = 74.12f,
              UserId = 2
            };
            context.Add(newProduct6);

            Product newProduct7 = new Product
            {
              Name = "Other thing",
              Description = "Is This Love",
              CategoryId = 1,
              Quantity = 420,
              Price = 74.12f,
              UserId = 2
            };
            context.Add(newProduct7);

            context.SaveChanges();

            Message farmer1messageA = new Message 
            {
                Title = "Message A Title",
                Body = "Here's a body of Message A",
                StartDate = DateTime.Today.Date,
                EndDate = DateTime.Today.AddDays(7),
                UserId = 1
            };

            Message farmer1messageB = new Message 
            {
                Title = "Message B Title",
                Body = "Here's a body of Message B",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(14),
                UserId = 1
            };

            Message farmer2messageC = new Message 
            {
                Title = "Message C Title",
                Body = "Here's a body of Message C",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                UserId = 2
            };

            Message farmer2messageD = new Message 
            {
                Title = "Message D Title",
                Body = "Here's a body of Message D",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(14),
                UserId = 2
            };

            context.Messages.Add(farmer1messageA);
            context.Messages.Add(farmer1messageB);
            context.Messages.Add(farmer2messageC);
            context.Messages.Add(farmer2messageD);
            context.SaveChanges();      
        }
    }
}
