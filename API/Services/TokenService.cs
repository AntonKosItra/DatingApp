using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    
    public TokenService(IConfiguration configuration)
    {
       _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!)); 
    }
    
    public string CreateToken(AppUser user) // could use only string user.UserName - let's take a look later
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.NameId, user.UserName)
        };
        
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = creds,
            Expires = DateTime.Now.AddDays(7)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}