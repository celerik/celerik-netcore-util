using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Adds some extension methods related to the Pagination functionality.
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Paginates this query according to request params.
        /// </summary>
        /// <typeparam name="TItem">The entity type in the query collection.</typeparam>
        /// <param name="query">Object against we are querying.</param>
        /// <param name="request">Object with the request arguments.</param>
        /// <param name="isAsync">Indicates whether the operation is Async.</param>
        /// <returns>Paginated result.</returns>
        public static async Task<PaginationResult<TItem>> Paginate<TItem>(
            this IQueryable<TItem> query,
            PaginationRequest request,
            bool isAsync = true)
        {
            if (request == null)
                throw new ArgumentException(UtilResources.Get("PaginationExtensions.Paginate.NullRequest"));

            var count = isAsync ? await query.CountAsync() : query.Count();

            if (!string.IsNullOrEmpty(request.SortKey))
                query = request.IsAscending
                    ? query.OrderBy(request.SortKey)
                    : query.OrderByDescending(request.SortKey);

            query = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var items = isAsync ? await query.ToListAsync() : query.ToList();

            var sorDirection = request.IsAscending
                ? SortDirection.Asc.GetDescription()
                : SortDirection.Desc.GetDescription();

            var pageCount = (int)Math.Ceiling(count / (double)request.PageSize);

            return new PaginationResult<TItem>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortKey = request.SortKey,
                SortDirection = sorDirection,
                Items = items,
                RecordCount = count,
                PageCount = pageCount
            };
        }
    }
}
