using System;
using System.Collections.Generic;
using System.Text;

namespace QueryUtils
{
  public class PagedResponse<T>
  {
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; }
    public int NextPage => GetNextPage();
    public int PreviousPage => GetPreviousPage();
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedResponse() { }

    public PagedResponse(List<T> items, int count, int pageNumber, int pageSize)
    {
      TotalCount = count;
      PageSize = pageSize;
      CurrentPage = pageNumber;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);
      Items = items ?? new List<T>();
    }

    private int GetNextPage()
    {
      if (HasNext)
        return CurrentPage + 1;
      return 0;
    }

    private int GetPreviousPage()
    {
      if (!HasPrevious)
        return 0;

      if (CurrentPage > TotalPages)
        return TotalPages;

      return CurrentPage - 1;
    }
  }
}
