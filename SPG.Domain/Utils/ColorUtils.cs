using System.Security.Cryptography;
using System.Text;

namespace SPG.Domain.Utils
{
  public class ColorUtils
  {
    public static string GenerateHexColor(string input)
    {
      byte[] inputBytes = Encoding.UTF8.GetBytes(input);
      byte[] hashBytes = MD5.HashData(inputBytes);

      byte red = hashBytes[0];
      byte green = hashBytes[1];
      byte blue = hashBytes[2];

      return $"#{red:X2}{green:X2}{blue:X2}";
    }
  }
}
