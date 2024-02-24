using Microsoft.IdentityModel.Tokens;
using SPG.Application.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Resources;
using System.Security.Claims;
using System.Text;

namespace SPG.Application.Login
{
  public class LoginService(IUserService userService) : ILoginService
  {
    private readonly IUserService _userService = userService;

    public void AuthenticateUser(LoginDto loginDto)
    {
      var user = _userService.GetUserByLogin(loginDto.Username);
      var isValidPassword = ValidatePassword(loginDto, user);

      if (!isValidPassword)
        throw new Exception(Resources.InvalidLogin);

      GenerateJWTToken(loginDto);
    }

    private void GenerateJWTToken(LoginDto loginDto, UserModel userModel)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
            new(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
          }),
        Expires = DateTime.UtcNow.AddMinutes(15),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      loginDto.Token = tokenHandler.WriteToken(token);
    }

    private static bool ValidatePassword(LoginDto loginDto, UserModel userModel)
    {
      byte[] bytes = Convert.FromBase64String(loginDto.Password);
      string convertedPassword = Encoding.UTF8.GetString(bytes);
      return userModel.VerifyPassword(convertedPassword);
    }
  }
}
