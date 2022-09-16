using System.Collections.Generic;

namespace Application.Common.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
    }
}
