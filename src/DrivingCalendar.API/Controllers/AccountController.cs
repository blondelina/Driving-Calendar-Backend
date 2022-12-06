using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DrivingCalendar.API.Models;
using DrivingCalendar.API.Options;
using DrivingCalendar.Business.Constants;
using DrivingCalendar.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using DrivingCalendar.Business.Abstractions.Services;

namespace DrivingCalendar.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class AccountController : ControllerBase
    {
        private readonly ApiOptions _options;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly UserManager<Student> _studentUserManager;
        private readonly UserManager<Company> _companyUserManager;
        private readonly UserManager<Instructor> _instructorUserManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IContextService _contextService;

        public AccountController(
            ApiOptions options, 
            UserManager<IdentityUser<int>> userManager,
            UserManager<Student> studentUserManager,
            UserManager<Instructor> instructorUserManager,
            SignInManager<IdentityUser<int>> signInManager,
            UserManager<Company> companyUserManager,
            IContextService contextService)
        {
            _options = options;
            _userManager = userManager;
            _studentUserManager = studentUserManager;
            _signInManager = signInManager;
            _instructorUserManager = instructorUserManager;
            _companyUserManager = companyUserManager;
            _contextService = contextService;
        }
     

        [AllowAnonymous]
        [HttpPost("students/register")]
        public async Task<IActionResult> RegisterStudentAsync([FromBody][Required] RegisterAccountRequest registerAccountRequest)
        {
            Student newStudent = new()
            {
                UserName = registerAccountRequest.Username,
                Email = registerAccountRequest.Email,
            };
            IdentityResult result = await _studentUserManager.CreateAsync(newStudent, registerAccountRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new ObjectResult(newStudent.Id)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [AllowAnonymous]
        [HttpPost("companies/register")]
        public async Task<IActionResult> RegisterCompanyAsync([FromBody][Required] RegisterCompanyRequest registerCompanyRequest)
        {
            
            Company newCompany = new()
            {   
                UserName=registerCompanyRequest.Username,
                Email = registerCompanyRequest.Email,
                CompanyName = registerCompanyRequest.CompanyName,
            };
            IdentityResult result = await _companyUserManager.CreateAsync(newCompany, registerCompanyRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new ObjectResult(newCompany.Id)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }


        [Authorize(Roles = IdentityRoles.COMPANY)]
        [HttpPost("instructors/register")]
        public async Task<IActionResult> RegisterInstructorAsync([FromBody][Required] RegisterAccountRequest registerAccountRequest)
        {
            IdentityUser<int> company = await _contextService.GetCurrentUserAsync();
            Instructor newInstructor = new()
            {
                UserName = registerAccountRequest.Username,
                Email = registerAccountRequest.Email,
                CompanyId = company.Id
            };
            IdentityResult result = await _instructorUserManager.CreateAsync(newInstructor, registerAccountRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new ObjectResult(newInstructor.Id)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] [Required] LoginRequest loginRequest)
        {
            IdentityUser<int> user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                return Unauthorized();
            }

            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            ICollection<Claim> claims = new Collection<Claim>
            {
                new(ClaimTypes.Name, user.Id.ToString())
            };

            if (await _studentUserManager.FindByIdAsync(user.Id.ToString()) is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, IdentityRoles.STUDENT));
            }
            if (await _companyUserManager.FindByIdAsync(user.Id.ToString()) is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, IdentityRoles.COMPANY));
            }
            if(await _instructorUserManager.FindByIdAsync(user.Id.ToString()) is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, IdentityRoles.INSTRUCTOR));
            }

            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtSecretKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new(
                audience: _options.JwtAudience,
                issuer: _options.JwtIssuer,

                claims: claims,

                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(_options.JwtExpirationInHours),

                signingCredentials: signingCredentials
            );

            return Ok(new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                UnixTimeExpiresAt = new DateTimeOffset(jwt.ValidTo).ToUnixTimeMilliseconds()
            });
        }
    }
}
