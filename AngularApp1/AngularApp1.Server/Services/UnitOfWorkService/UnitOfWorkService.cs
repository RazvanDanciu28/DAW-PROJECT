using Microsoft.AspNetCore.Identity;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using System.Runtime.CompilerServices;
using AngularApp1.Server.Services.UserService;


namespace AngularApp1.Server.Services.UnitOfWorkService
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWorkService(
            AppDbContext db, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            UserService = new UserService.UserService(this.db);
            _roleManager = roleManager;
        }

        public UserService.IUserService UserService { get; private set; }

        public IUserService Users => throw new NotImplementedException();

        public UserManager<AppUser> GetUserManager() { return _userManager; }

        public void Dispose() { db.Dispose(); }

        public int Save() { return db.SaveChanges(); }

        public UserManager<AppUser> getUserManager()
        {
            throw new NotImplementedException();
        }
    }
}
