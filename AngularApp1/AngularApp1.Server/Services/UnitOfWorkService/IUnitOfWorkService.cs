using Microsoft.AspNetCore.Identity;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services;




namespace AngularApp1.Server.Services.UnitOfWorkService
{
    public interface IUnitOfWorkService : IDisposable
    {
        UserService.IUserService Users { get; }

        UserManager<AppUser> getUserManager();

        int Save();
    }
}
