﻿using System.Collections.Generic;

namespace QueryUtils
{
  public class PagedQueryResult<T>
  {
    public int TotalCount { get; }
    public List<T> Items { get; }

    public PagedQueryResult(int totalCount, List<T> items)
    {
      TotalCount = totalCount;
      Items = items;
    }
  }
}
