using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryUtils
{
  public class QueryParser
  {
    public static ParseResult Parse(QueryParams q)
    {
      return new ParseResult
      {
        Filters = ParseFilters(q),
        Includes = ParseIncludes(q),
        PagingParams = new PagingParameters
        {
          PageNumber = q.PageNumber,
          PageSize = q.PageSize
        }
      };
    }

    private static List<string> ParseIncludes(QueryParams q)
    {
      if (string.IsNullOrWhiteSpace(q.Includes))
        return new List<string>();
      
      return q.Includes
        .Split(',')
        .Select(x => x.Trim())
        .ToList();
    }

    private static Dictionary<string, string> ParseFilters(QueryParams q)
    {
      var filters = new Dictionary<string, string>();

      if (string.IsNullOrWhiteSpace(q.Filters))
        return filters;

      var pairs = q.Filters.Split(',');

      foreach (var pair in pairs)
      {
        var parts = pair.Split('=');
        var key = parts[0].Trim();
        var value = parts[1].Trim();

        if (filters.ContainsKey(key))
          filters[key] = value;
        else
          filters.Add(key, value);
      }

      return filters;
    }

    public static IQueryable<T> MapFiltersToQuery<T>(ParseResult parseResult, IQueryable<T> query)
    {
      var props = typeof(T).GetProperties();

      foreach (var filter in parseResult.Filters)
      {
        if (!props.Any(p => p.Name.ToLower() == filter.Key))
          throw new InvalidOperationException($"{filter.Key} is not a property of {typeof(T).Name}");

        query = query.Where(x => x.GetType().GetProperty(filter.Key).GetValue(x).ToString() == filter.Value);
      }

      return query;
    }
  }
}
