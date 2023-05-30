using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public record AuthenticationData(string? Username, string? Password);
    public record UserData(int Id, string? FirstName, string? LastName, string? UserName);

    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);

        if (user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);
        
        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:SecretKey")));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        List<Claim> claims = new();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));
            claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));

        var token = new JwtSecurityToken(
            _configuration.GetValue<string>("Authentication:Issuer"),
            _configuration.GetValue<string>("Authentication:Audience"),
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials);
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // THIS IS NOT PRODUCTION CODE - REPLACE WITH YOUR OWN AUTHENTICATION LOGIC

        if (CompareValues(data.Username, "mlungu") && CompareValues(data.Password, "test123"))
        {
            return new UserData(1, "Marius", "Lungu", data.Username!);
        }

        if (CompareValues(data.Username, "admin") && CompareValues(data.Password, "admin"))
        {
            return new UserData(2, "Admin", "Adminescu", data.Username!);
        }

        return null;
    }

    private bool CompareValues(string? actual, string? expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected)) 
            {
                return true;
            }
        }

        return false;
    }
}