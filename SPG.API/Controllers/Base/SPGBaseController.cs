using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SPG.API.Controllers.Base
{
  public class SPGBaseController<T> : ControllerBase
  {
    protected static IEnumerable<T> ApplyFilters(IEnumerable<T> data, Dictionary<string, string> filters)
    {
      if (filters == null)
        return data;

      var values = new List<string>();

      foreach (var filter in filters)
      {
        PropertyInfo? property = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (filter.Value.Contains("list("))
        {
          values = ParseList(filter.Value);
          if (property != null)
            data = data.Where(item =>
            {
              var value = property.GetValue(item)?.ToString();
              if (value != null)
              {
                return values.Contains(value);
              }
              return false;
            }).ToList();
          continue;
        }
        
        if (property != null)
          data = data.Where(item => property.GetValue(item)?.ToString() == filter.Value);
      }

      return data;
    }

    private static List<string> ParseList(string input)
    {
      if (!input.StartsWith("list(") || !input.EndsWith(")"))
        throw new FormatException("Input string format is not valid.");

      string content = input.Substring(5, input.Length - 6);
      string[] items = content.Split(',');

      var resultList = new List<string>();

      foreach (var item in items)
      {
        string trimmedItem = item.Trim('\'');
        resultList.Add(trimmedItem);
      }

      return resultList;
    }
  }
}
