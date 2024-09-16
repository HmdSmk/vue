using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sample_vue_app_server.ApiModels;
using sample_vue_app_server.Models;

namespace sample_vue_app_server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController: ControllerBase
{
    private JwtSettings jwtSettings;
    public AuthController(
        JwtSettings _jwtSettings)
    {
        jwtSettings = _jwtSettings;
    }
    
    [HttpPost]
    public string GetUserToken(GetUserTokenRequest request)
    {
        if (!(string.Equals(request.UserName, "user", StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(request.Password, "pass")))
        {
            throw new UnauthorizedAccessException();
        }
        var expireDate = DateTime.UtcNow.AddHours(1);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new Claim(SecurityConstants.UserName, request.UserName),
            }),
            Expires = expireDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptior);

        var tokenResult = tokenHandler.WriteToken(token);
        return tokenResult;
    }
    
}