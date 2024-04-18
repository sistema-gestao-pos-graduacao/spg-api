using System.Text.RegularExpressions;

namespace SPG.Domain.Utils
{
  public class CPFUtils
  {
    public static string RemoveSpecialCharacters(string cpf)
    {
      string cleanedCPF = Regex.Replace(cpf, @"\D", "");

      return cleanedCPF;
    }
  }
}

