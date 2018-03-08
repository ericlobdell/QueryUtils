using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QueryParser.Tests
{
  public class PagedResponseTests
  {
    [Theory, AutoData]
    public void Throws_when_page_number_invalid(List<int> items, int pageSize, int count)
    {
      var negativePageNumber = -1;
      var ex = Assert.Throws<ArgumentException>(() =>
          new PagedResponse<int>(items, count, negativePageNumber, pageSize));

      Assert.True(ex.Message.ContainsTerms("page", "number"));
    }

    [Theory, AutoData]
    public void Sets_current_page_when_page_number_valid(List<int> items, int pageSize, int count, int pageNumber)
    {
      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(pageNumber, sut.CurrentPage);
    }

    [Theory, AutoData]
    public void Throws_when_page_size_invalid(List<int> items, int pageNumber, int count)
    {
      var negativePageSize = -1;
      var ex = Assert.Throws<ArgumentException>(() =>
          new PagedResponse<int>(items, count, pageNumber, negativePageSize));

      Assert.True(ex.Message.ContainsTerms("page", "size"));
    }

    [Theory, AutoData]
    public void Sets_page_size_when_value_valid(List<int> items, int pageSize, int count, int pageNumber)
    {
      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(pageSize, sut.PageSize);
    }

    [Theory, AutoData]
    public void Calculates_total_pages_correctly_when_count_greater_than_page_size(List<int> items, int pageNumber)
    {
      var count = 13;
      var pageSize = 3;
      var expectedPageCount = 5;

      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(expectedPageCount, sut.TotalPages);
    }

    [Theory, AutoData]
    public void Calculates_total_pages_correctly_when_count_less_than_page_size(List<int> items, int pageNumber)
    {
      var count = 2;
      var pageSize = 3;
      var expectedPageCount = 1;

      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(expectedPageCount, sut.TotalPages);
    }

    [Theory, AutoData]
    public void Calculates_total_pages_correctly_when_count_zero(List<int> items, int pageNumber)
    {
      var count = 0;
      var pageSize = 3;
      var expectedPageCount = 0;

      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(expectedPageCount, sut.TotalPages);
    }

    [Theory, AutoData]
    public void Sets_Items_to_empty_list_when_null(int count, int pageNumber, int pageSize)
    {
      List<int> nullItems = null;

      var sut = new PagedResponse<int>(nullItems, count, pageNumber, pageSize);

      Assert.IsType<List<int>>(sut.Items);
      Assert.Empty(sut.Items);
    }

    [Theory, AutoData]
    public void HasPrevious_false_when_current_page_is_first_page(List<int> items, int pageSize, int count)
    {
      var firstPageNumber = 1;
      var sut = new PagedResponse<int>(items, count, firstPageNumber, pageSize);

      Assert.False(sut.HasPrevious);
    }

    [Theory, AutoData]
    public void HasPrevious_true_when_current_page_is_greater_than_first_page(List<int> items)
    {
      var notFirstPageNumber = 2;
      int pageSize = 1;
      int count = 10;

      var sut = new PagedResponse<int>(items, count, notFirstPageNumber, pageSize);

      Assert.True(sut.HasPrevious);
    }

    [Theory, AutoData]
    public void Sets_PreviousPage_zero_when_HasPrevious_false(List<int> items, int pageSize, int count)
    {
      var firstPageNumber = 1;
      var expectedPreviousPage = 0;

      var sut = new PagedResponse<int>(items, count, firstPageNumber, pageSize);

      Assert.Equal(expectedPreviousPage, sut.PreviousPage);
    }

    [Theory, AutoData]
    public void Sets_PreviousPage_last_page_when_current_page_exceeds_TotalPages(List<int> items)
    {
      var tooLargePageNumber = 100;
      var count = 2;
      var pageSize = 1;
      var lastPageNumber = 2;

      var expectedPreviousPage = lastPageNumber;

      var sut = new PagedResponse<int>(items, count, tooLargePageNumber, pageSize);

      Assert.Equal(expectedPreviousPage, sut.PreviousPage);
    }

    [Theory, AutoData]
    public void Sets_PreviousPage_as_previous_when_HasPrevious_true(List<int> items)
    {
      var notFirstPageNumber = 2;
      int pageSize = 1;
      int count = 10;
      var expectedPreviousPage = 1;

      var sut = new PagedResponse<int>(items, count, notFirstPageNumber, pageSize);

      Assert.Equal(expectedPreviousPage, sut.PreviousPage);
    }

    [Theory, AutoData]
    public void HasNext_false_when_current_page_is_last_page_or_greater(List<int> items)
    {
      var lastPageNumber = 2;
      int pageSize = 5;
      int count = 10;

      var sut = new PagedResponse<int>(items, count, lastPageNumber, pageSize);

      Assert.False(sut.HasNext);
    }

    [Theory, AutoData]
    public void HasNext_true_when_current_page_is_less_than_last_page(List<int> items)
    {
      var notLastPageNumber = 2;
      int pageSize = 1;
      int count = 10;

      var sut = new PagedResponse<int>(items, count, notLastPageNumber, pageSize);

      Assert.True(sut.HasNext);
    }

    [Theory, AutoData]
    public void Sets_next_page_zero_when_HasNext_false(List<int> items)
    {
      var pageNumber = 2;
      int pageSize = 5;
      int count = 10;
      int expectedNextPage = 0;

      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(expectedNextPage, sut.NextPage);
    }

    [Theory, AutoData]
    public void Sets_next_page_correctly_when_HasNext_true(List<int> items)
    {
      var pageNumber = 2;
      int pageSize = 1;
      int count = 10;
      int expectedNextPage = 3;

      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(expectedNextPage, sut.NextPage);
    }

    [Theory, AutoData]
    public void Maps_count_to_TotalCount(List<int> items, int pageSize, int count, int pageNumber)
    {
      var sut = new PagedResponse<int>(items, count, pageNumber, pageSize);

      Assert.Equal(count, sut.TotalCount);
    }
  }
}
