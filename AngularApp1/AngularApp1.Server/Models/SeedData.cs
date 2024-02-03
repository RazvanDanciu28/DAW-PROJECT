using Microsoft.AspNetCore.Identity;
using AngularApp1.Server.DataContext;


namespace AngularApp1.Server.Models
{
    public class SeedData
    {
        private readonly AppDbContext dbContext;

        public SeedData(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Initialize()
        {
            SeedRoles();
        }

        public void SeedRoles()
        {
            if (dbContext.Roles.Any())
            {
                return; //bd contine deja roluri
            }

            //crearea rolurilor in bd, se creaza daca bd nu contine roluri
            dbContext.Roles.AddRange(
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7210", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd7212", Name = "User", NormalizedName = "User".ToUpper() }
            );

            var hasher = new PasswordHasher<AppUser>();

            // CREAREA USERILOR IN BD
            // Se creeaza cate un user pentru fiecare rol
            dbContext.Users.AddRange(
                new AppUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                    FirstName = "MyAdmin",
                    LastName = "Admin",
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
                new AppUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb2",
                    FirstName = "MyUser",
                    LastName = "User",
                    UserName = "user@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "USER@TEST.COM",
                    Email = "user@test.com",
                    NormalizedUserName = "USER@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "User1!")
                }
            );

            // ASOCIEREA USER-ROLE
            dbContext.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                },
               new IdentityUserRole<string>
               {
                   RoleId = "2c5e174e-3b0e-446f-86af483d56fd7212",
                   UserId = "8e445865-a24d-4543-a6c6-9443d048cdb2"
               }
            );

            dbContext.SaveChanges();

        }
    }
}
