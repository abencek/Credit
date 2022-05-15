using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Credit.Data.Identity
{
    /// <summary>
    /// Context for application Identity database
    /// </summary>
    
    public class IdentityDbContext:IdentityDbContext<IdentityUser>
    {
         public IdentityDbContext (DbContextOptions<IdentityDbContext> options)
            : base(options){}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedInitialData(builder);
        }

        private static void SeedInitialData(ModelBuilder builder)
        {
            //Seeding roles to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "34569292-9598-4b9d-9465-ffc4f5199bde",
                    Name = "admins",
                    NormalizedName = "ADMINS"
                },
                new IdentityRole
                {
                    Id = "8dcd09a5-5758-4d13-810b-c164d0ba1828",
                    Name = "users",
                    NormalizedName = "USERS"
                }
            );

            //A hasher to hash the password before seeding the user to the database
            var hasher = new PasswordHasher<IdentityUser>();

            //Seeding users to AspNetUsers table
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "3a19a7c5-b139-4a69-87d4-96809d842e10",
                    UserName = "admin1",
                    NormalizedUserName = "ADMIN1",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    LockoutEnabled = false,
                    PhoneNumber = "987654321",
                    PasswordHash = hasher.HashPassword(null, "admin")
                },
              new IdentityUser
              {
                  Id = "b8de7445-a1f9-4c98-a590-223315e0df4d",
                  UserName = "user1",
                  NormalizedUserName = "USER1",
                  Email = "user1@gmail.com",
                  NormalizedEmail = "USER1@GMAIL.COM",
                  LockoutEnabled = false,
                  PhoneNumber = "1234567890",
                  PasswordHash = hasher.HashPassword(null, "user")
              }
            );

            //Seeding relations between users and roles to AspNetUserRoles table
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "34569292-9598-4b9d-9465-ffc4f5199bde",
                    UserId = "3a19a7c5-b139-4a69-87d4-96809d842e10"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "8dcd09a5-5758-4d13-810b-c164d0ba1828",
                    UserId = "b8de7445-a1f9-4c98-a590-223315e0df4d"
                }
            );

        }

    }

}
