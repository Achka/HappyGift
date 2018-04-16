using HappyGift.Data;
using HappyGift.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace HappyGift.Tests
{
    public abstract class BaseTests
    {
        protected DbContextOptions<ApplicationDbContext> GetContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "HappyGiftDb")
                .Options;
        }

        protected void AddFakeServices(ApplicationDbContext context)
        {
            context.Services.Add(new Service
            {
                Name = "ServiceName1"
            });

            context.Services.Add(new Service
            {
                Name = "ServiceName2"
            });

            context.Services.Add(new Service
            {
                Name = "ServiceName3"
            });

            context.SaveChanges();
        }

        protected string CreateFakeUser(ApplicationDbContext context)
        {
            var user = context.Users.Add(new HappyGiftUser
            {
                Id = "0cd950bf-5fc5-4d34-90fc-b695342b2ace",
                UserName = "Mary",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
            });

           
            context.SaveChanges();

            return user.Entity.Id;
        }
    }
}
