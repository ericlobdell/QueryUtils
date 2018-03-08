using Xunit;

namespace QueryParser.Tests
{
  public class ParserTests
  {
    [Fact]
    public void Parse_returns_empty_dictionary_when_filters_empty_string()
    {
      var q = new QueryParams
      {
        Filters = string.Empty
      };

      var result = Parser.Parse(q);

      Assert.Empty(result.Filters);
    }
  }
}
