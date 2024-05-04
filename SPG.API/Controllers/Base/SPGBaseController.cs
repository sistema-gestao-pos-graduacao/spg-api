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

      foreach (var filter in filters)
      {
        PropertyInfo? property = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property != null)
          data = data.Where(item => property.GetValue(item)?.ToString() == filter.Value);
      }

      return data;
    }

  }
}
