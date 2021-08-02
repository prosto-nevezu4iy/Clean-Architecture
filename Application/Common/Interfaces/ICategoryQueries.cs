using Application.Common.Models;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICategoryQueries
    {
        Task<PagedResult<Category>> GetCategories(int page = 1, int pageSize = 10);
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetParentCategories();
    }
}
