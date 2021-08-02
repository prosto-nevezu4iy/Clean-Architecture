using Application.Common.Interfaces;
using Application.Common.Models;
using Dapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CategoryQueries(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<PagedResult<Category>> GetCategories(int page = 1, int pageSize = 10)
        {
            var results = new PagedResult<Category>();

            using (var connection = _sqlConnectionFactory.GetOpenConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Offset", (page - 1) * pageSize, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);

                var sql = @"Select * From Categories 
                            Order By Name 
                            OFFSET @Offset ROWS 
                            FETCH NEXT @PageSize ROWS ONLY;
                        
                            Select Count(*) From Categories";

                using (var multi = await connection.QueryMultipleAsync(sql, parameter))
                {
                    results.Items = multi.Read<Category>().ToList();
                    results.Count = multi.ReadFirst<int>();
                }
            }

            return results;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            using (var connection = _sqlConnectionFactory.GetOpenConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

                return await connection.QuerySingleOrDefaultAsync<Category>(
                    "Select * From Categories Where Id = @Id",
                    param: parameter
                );
            }
        }

        public async Task<IEnumerable<Category>> GetParentCategories()
        {
            using (var connection = _sqlConnectionFactory.GetOpenConnection())
            {
                return await connection.QueryAsync<Category>(
                    "Select * From Categories Where ParentId Is Null Order By Name"
                );
            }
        }
    }
}
