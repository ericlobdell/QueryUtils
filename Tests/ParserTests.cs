using QueryParser;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
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

    [Fact]
    public void Parse_returns_empty_dictionary_when_filters_null()
    {
      var q = new QueryParams
      {
        Filters = null
      };

      var result = Parser.Parse(q);

      Assert.Empty(result.Filters);
    }

    [Fact]
    public void Parse_returns_empty_list_when_includes_empty_string()
    {
      var q = new QueryParams
      {
        Includes = string.Empty
      };

      var result = Parser.Parse(q);

      Assert.Empty(result.Includes);
    }

    [Fact]
    public void Parse_returns_empty_list_when_includes_null()
    {
      var q = new QueryParams
      {
        Includes = null
      };

      var result = Parser.Parse(q);

      Assert.Empty(result.Includes);
    }

    [Fact]
    public void Parse_maps_paging_params()
    {
      var q = new QueryParams
      {
        PageNumber = 2,
        PageSize = 5
      };

      var result = Parser.Parse(q);

      Assert.Equal(q.PageNumber, result.PagingParams.PageNumber);
      Assert.Equal(q.PageSize, result.PagingParams.PageSize);
    }

    [Fact]
    public void Parse_adds_filter_to_dict()
    {
      var q = new QueryParams
      {
        Filters = "a=b"
      };

      var result = Parser.Parse(q);

      Assert.True(result.Filters.ContainsKey("a"));
      Assert.Equal("b", result.Filters["a"]);
    }

    [Fact]
    public void Parse_adds_all_filters_to_dict()
    {
      var q = new QueryParams
      {
        Filters = "a=b,x=y"
      };

      var result = Parser.Parse(q);

      Assert.True(result.Filters.ContainsKey("a"));
      Assert.Equal("b", result.Filters["a"]);

      Assert.True(result.Filters.ContainsKey("x"));
      Assert.Equal("y", result.Filters["x"]);
    }

    [Fact]
    public void Parse_uses_last_value_when_duplicate_filter()
    {
      var q = new QueryParams
      {
        Filters = "a=b,a=c"
      };

      var result = Parser.Parse(q);

      Assert.True(result.Filters.ContainsKey("a"));
      Assert.Equal("c", result.Filters["a"]);
    }

    [Fact]
    public void Parse_adds_include_to_list()
    {
      var q = new QueryParams
      {
        Includes = "a"
      };

      var result = Parser.Parse(q);

      Assert.Contains("a", result.Includes);
    }

    [Fact]
    public void Parse_adds_all_includes_to_list()
    {
      var q = new QueryParams
      {
        Includes = "a,b"
      };

      var result = Parser.Parse(q);

      Assert.Contains("a", result.Includes);
      Assert.Contains("b", result.Includes);
    }

    [Fact]
    public void MapToQuery_throws_when_filter_key_not_prop_name()
    {
      var filters = new Dictionary<string, string>();
      filters.Add("x", "y");

      var results = new ParseResult
      {
        Filters = filters
      };

      var q = new List<Foo>().AsQueryable();

      var ex = Assert.Throws<InvalidOperationException>(() => Parser.MapFiltersToQuery<Foo>(results, q));

      Assert.True(ex.Message.ContainsTerms("x", "not", "property", nameof(Foo)));
    }

    [Fact]
    public void MapToQuery_applies_filters_to_query()
    {
      var filters = new Dictionary<string, string>();
      filters.Add("A", "yes");

      var results = new ParseResult
      {
        Filters = filters
      };

      var goodFoo = new Foo { A = "yes" };
      var badFoo = new Foo { A = "no" };

      var q = new List<Foo> { goodFoo, badFoo }.AsQueryable();

      var mappedQuery = Parser.MapFiltersToQuery<Foo>(results, q);
      var filteredList = mappedQuery.ToList();

      Assert.Contains(goodFoo, filteredList);
      Assert.DoesNotContain(badFoo, filteredList);
      
    }
  }

  public class Foo
  {
    public string A { get; set; }
    public string B { get; set; }
  }
}
