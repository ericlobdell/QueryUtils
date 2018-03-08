namespace QueryUtils
{
  public class QueryParams : PagingParameters
  {
    public string Filters { get; set; }
    public string Includes { get; set; }
  }
}
