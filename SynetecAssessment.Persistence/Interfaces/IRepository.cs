using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
