using Microsoft.AspNetCore.Mvc;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.GenericService;



namespace AngularApp1.Server.Services.UserService
{
    public interface IUserService : IGenericService<AppUser>
    {
        Task<AppUser> GetById(string id);
    }
}
