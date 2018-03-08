using System.Linq;

namespace QueryParser.Tests
{
  public static class StringAssertionExtensions
  {
    public static bool ContainsTerms(this string test, params object[] terms)
    {
      var normalized = test.ToLower();
      return terms.All(term => normalized.Contains(term.ToString().ToLower()));
    }
  }
}
