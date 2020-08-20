using geesRecorder.Models;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Services
{
    public class AppProfileService : DefaultProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppProfileService(ILogger<DefaultProfileService> logger,
            UserManager<ApplicationUser> userManager) : base(logger)
        {
            _userManager = userManager;
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string username = context.Subject.Identity.Name ?? "";
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                context.IssuedClaims.AddRange(claims);
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, user.UserName));
                context.IssuedClaims.AddRange(context.Subject.Claims);
            }
        }
    }
}
