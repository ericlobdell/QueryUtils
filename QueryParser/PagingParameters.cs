namespace QueryParser
{
  public class PagingParameters
  {
    public static int DefaultPageSize = 10;
    public static int MaxPageSize = 20;
    public static int DefaultPageNumber = 1;

    private int _pageNumber = DefaultPageNumber;

    private int _pageSize = DefaultPageSize;

    public int PageNumber
    {
      get => _pageNumber;
      set
      {
        if (value < 1)
          _pageNumber = DefaultPageNumber;
        else
          _pageNumber = value;
      }
    }

    public int PageSize
    {
      get => _pageSize;
      set
      {
        if (value < 1)
          _pageSize = DefaultPageSize;
        else if (value > MaxPageSize)
          _pageSize = MaxPageSize;
        else
          _pageSize = value;
      }
    }

    public int Skip()
    {
      return (PageNumber - 1) * PageSize;
    }

    public int Take()
    {
      return PageSize;
    }
  }
}
