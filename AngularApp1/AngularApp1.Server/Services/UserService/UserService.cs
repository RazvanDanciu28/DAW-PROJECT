using Microsoft.AspNetCore.Mvc.ActionConstraints;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.GenericService;
using Microsoft.EntityFrameworkCore;



namespace AngularApp1.Server.Services.UserService
{
    public class UserService : GenericService<AppUser>, IUserService
    {
        public UserService(AppDbContext db) : base(db) { }

        public Task<AppUser> GetById(string id)
        {

            return Task.FromResult<AppUser>(_db.Users.Find(id));
        }
    }
}
