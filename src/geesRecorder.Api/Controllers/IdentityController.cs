using geesRecorder.Api.Data;
using geesRecorder.Shared.ApiModels;
using geesRecorder.Api.Services;
using geesRecorder.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("auth")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _dbContext;

        public IdentityController(UserManager<ApplicationUser> userManager,
            AuthService authService, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _authService = authService;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(SignInDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email); 

            if(user is not null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
                return Ok(token);
            }

            return Unauthorized(Constants.InvalidLoginCredentials);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            string userName = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var user = await _userManager.FindByNameAsync(userName);
            var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(SignUpDTO signUpDTO)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDTO.Email,
                Email = signUpDTO.Email,
                Pin = signUpDTO.Pin
            };

            var result = await _userManager.CreateAsync(user, signUpDTO.Password);

            if (result.Succeeded)
            {
                var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
                return Ok(token);
            }

            string errors = string.Join(",", result.Errors);
            return BadRequest(errors);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync([FromBody]DBBackup dBBackup)
        {
            var backup = _dbContext.DBBackups.FirstOrDefault(x => x.MachineCode == dBBackup.MachineCode);
            if(backup is not null)
            {
                backup.MachineName = dBBackup.MachineName;
                backup.DBSnapshot = dBBackup.DBSnapshot;
                _dbContext.Update(backup);
            }
            else
            {
                _dbContext.DBBackups.Add(dBBackup);
            }
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
