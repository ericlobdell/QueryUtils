using System.Collections.Generic;

namespace QueryParser
{
  public class ParseResult
  {
    public Dictionary<string,string> Filters { get; set; }
    public List<string> Includes { get; set; }
    public PagingParameters PagingParams { get; set; }
  }
}
