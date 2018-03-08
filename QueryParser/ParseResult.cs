using System.Collections.Generic;
using System.Linq;

namespace QueryUtils
{
  public class ParseResult
  {
    public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
    public List<string> Includes { get; set; } = new List<string>();
    public PagingParameters PagingParams { get; set; }

    public bool HasFilters => Filters.Any();
    public bool HasIncludes => Includes.Any();
  }
}
