using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace SPG.Application.Authorization
{
  public class JWTGenerationMiddleware(RequestDelegate next, IConfiguration configuration)
  {
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _configuration = configuration;

    public async Task InvokeAsync(HttpContext context)
    {
      var jwtSecret = GenerateJwtSecret();

      var configurationRoot = _configuration as IConfigurationRoot;
      configurationRoot?.Reload();

      if(configurationRoot != null)
      {
        var section = configurationRoot.GetSection("JwtSettings");
        section["Secret"] = jwtSecret;
      }

      await _next(context);
    }

    private static string GenerateJwtSecret(int length = 64)
    {
      const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

      byte[] randomBytes = new byte[length];

      using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomBytes);
      }

      char[] chars = new char[length];
      for (int i = 0; i < length; i++)
      {
        chars[i] = allowedChars[randomBytes[i] % allowedChars.Length];
      }

      return new string(chars);
    }
  }

  public static class GenerateJwtSecretMiddlewareExtensions
  {
    public static IApplicationBuilder UseGenerateJwtSecret(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<JWTGenerationMiddleware>();
    }
  }
}
