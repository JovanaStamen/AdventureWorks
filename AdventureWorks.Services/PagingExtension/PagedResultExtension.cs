using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.DAL.GenericRepository
{
    public class PagedResultExtension
    {
        public PagedResult<TEntity> GetPaged<TEntity>(IOrderedQueryable<TEntity> query, int page, int pageSize) where TEntity : class
        {
            var result = new PagedResult<TEntity>
            {
                CurrentPage = page,
                PageSize = pageSize,
                Total = query.Count()
            };

            var pageCount = (double)result.Total / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
