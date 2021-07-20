using IdentityServer.Messages;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Abstraction
{
    public interface IUserService 
    {
        Task<Response> CreateAsync(ApplicationUser applicationUser, string password);
        Task<ApplicationUser> FindByIdAsync(string Id);
    }
}
