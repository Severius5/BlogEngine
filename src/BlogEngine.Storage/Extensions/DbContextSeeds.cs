using BlogEngine.Storage.Entities;
using System;

namespace Microsoft.EntityFrameworkCore
{
    internal static class DbContextSeeds
    {
        public static ModelBuilder SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(DefaultAdmin());

            return modelBuilder;
        }

        private static User DefaultAdmin()
        {
            return new User
            {
                Id = 1,
                Bio = "Write here some information about yourself.  You can use markdown here.",
                CreationDate = new DateTime(2018, 1, 1, 1, 1, 1),
                DetailsStamp = "DA56E937-FF2C-4401-9DC2-E353DD474346",
                Email = "admin@admin.admin",
                NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                IsAdmin = true,
                Username = "Administrator",
                NormalizedUsername = "ADMINISTRATOR",
                Slug = "administrator",
                Password = "AQAAAAEAACcaAAAAECekU4gLOtHZ4lSHACsEr1UjxJ5fo2dUjXTM1Rq8ZeRZzkcq+h8zWlLgtJ2LGpUU1w=="
            };
        }
    }
}
