using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication.BLL.Authentication;
using WebApplication.BLL.ResultConstants;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication.BLL.ResultModel;

namespace WebApplication.BLL.Services
{
    class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, 
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<IResult> Login(LoginModel model)
        {
            try
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return Result.CreateSuccess(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }

                return Result.CreateFailed(AuthServiceResultConstants.UserNotFound, null);
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
            
        }

        public async Task<IResult> Register(RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return Result.CreateFailed(AuthServiceResultConstants.UserAlreadyExists);

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return Result.CreateFailed(CommonResultConstants.Failed);

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

        public async Task<IResult> RegisterAdmin(RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                    return Result.CreateFailed(AuthServiceResultConstants.UserAlreadyExists);

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return Result.CreateFailed(CommonResultConstants.Failed);

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                return Result.CreateSuccess();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(CommonResultConstants.Unexpected, e);
            }
        }

    }
}
