using IdentityServer.Abstraction;
using IdentityServer.Commands;
using IdentityServer.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public UserService(UserManager<ApplicationUser> userManager, ISendEndpointProvider sendEndpointProvider)
        {
            this.userManager = userManager;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<Messages.Response> CreateAsync(ApplicationUser applicationUser,string password)
        {
            try
            {
                var identityResult = await this.userManager.CreateAsync(applicationUser, password);
                if (identityResult.Succeeded)
                {
                    var token = await this.userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    if (!string.IsNullOrEmpty(token))
                    {
                        var createUserEmaiMessageCommand = new CreateUserEmailMessageCommand(applicationUser.Email, token, applicationUser.Id);

                        var sendEndpoint = await this.sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-user-verifiedmail"));
                        await sendEndpoint.Send<CreateUserEmailMessageCommand>(createUserEmaiMessageCommand);

                        return new Messages.SuccessResponse();
                    }
                    return new Messages.ErrorResponse("Verified token is missing.!");
                }
                return new Messages.ErrorResponse(identityResult.Errors.First().Description);
            }
            catch (Exception ex)
            {
                return new Messages.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApplicationUser> FindByIdAsync(string Id)
        {
            return await this.userManager.FindByIdAsync(Id);
        }
    }
}
