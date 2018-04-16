using HappyGift.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;

namespace HappyGift.Tests
{
    public class FakeUserManager : UserManager<HappyGiftUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<HappyGiftUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<HappyGiftUser>>().Object,
                new IUserValidator<HappyGiftUser>[0],
                new IPasswordValidator<HappyGiftUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<HappyGiftUser>>>().Object)
        { }

        public override async Task<HappyGiftUser> GetUserAsync(ClaimsPrincipal claims)
        {
            var user = new HappyGiftUser
            {
                Id = "0cd950bf-5fc5-4d34-90fc-b695342b2ace",
                UserName = "Mary",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
            };
            return user;

        }
    }
}
