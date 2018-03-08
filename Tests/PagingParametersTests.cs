using QueryUtils;
using Xunit;

namespace Tests
{
  public class PagingParametersTests
  {
    [Fact]
    public void PageNumber_uses_default_when_value_invalid()
    {
      var invalidPageNumber = -12;

      var sut = new PagingParameters
      {
        PageNumber = invalidPageNumber
      };

      Assert.Equal(PagingParameters.DefaultPageNumber, sut.PageNumber);
    }

    [Fact]
    public void PageNumber_uses_default_when_no_value_set()
    {
      var sut = new PagingParameters();

      Assert.Equal(PagingParameters.DefaultPageNumber, sut.PageNumber);
    }

    [Fact]
    public void PageNumber_uses_value_when_valid()
    {
      var validPageNumber = 12;

      var sut = new PagingParameters
      {
        PageNumber = validPageNumber
      };

      Assert.Equal(validPageNumber, sut.PageNumber);
    }

    [Fact]
    public void PageSize_uses_default_when_value_invalid()
    {
      var invalidPageSize = -12;

      var sut = new PagingParameters
      {
        PageSize = invalidPageSize
      };

      Assert.Equal(PagingParameters.DefaultPageSize, sut.PageSize);
    }

    [Fact]
    public void PageSize_uses_default_when_no_value_set()
    {
      var sut = new PagingParameters();

      Assert.Equal(PagingParameters.DefaultPageSize, sut.PageSize);
    }

    [Fact]
    public void PageSize_uses_max_value_when_value_exceeds_maximum()
    {
      var tooLargePageSize = 500;

      var sut = new PagingParameters
      {
        PageSize = tooLargePageSize
      };

      Assert.Equal(PagingParameters.MaxPageSize, sut.PageSize);
    }

    [Fact]
    public void PageSize_uses_value_when_value_valid()
    {
      var validPageSize = 12;

      var sut = new PagingParameters
      {
        PageSize = validPageSize
      };

      Assert.Equal(validPageSize, sut.PageSize);
    }

    [Fact]
    public void Skip_returns_correct_value()
    {
      var pageSize = 5;
      var pageNumber = 3;
      var expected = 10;

      var sut = new PagingParameters
      {
        PageSize = pageSize,
        PageNumber = pageNumber
      };

      Assert.Equal(expected, sut.Skip());
    }

    [Fact]
    public void Take_returns_correct_value()
    {
      var pageSize = 5;
      var pageNumber = 3;
      var expected = pageSize;

      var sut = new PagingParameters
      {
        PageSize = pageSize,
        PageNumber = pageNumber
      };

      Assert.Equal(expected, sut.Take());
    }
  }
}
