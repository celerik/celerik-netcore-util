using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Provides query utilities.
    /// </summary>
    public static class QueryUtility
    {
        /// <summary>
        /// Builds an OrderBy function using a property name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type in the data source.</typeparam>
        /// <param name="query">Object to evaluate queries against the data source.</param>
        /// <param name="propName">The property name to sort by.</param>
        /// <param name="comparer">Defines the method to compare the objects. This parameter
        /// does not work with Entity Framework and should be left out if using Linq to Sql.
        /// </param>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> query, string propName, IComparer<object> comparer = null)
        {
            var orderedList = ApplyOrder(query, "OrderBy", propName, comparer);
            return orderedList;
        }

        /// <summary>
        /// Builds an OrderByDescending function using a property name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type in the data source.</typeparam>
        /// <param name="query">Object to evaluate queries against the data source.</param>
        /// <param name="propName">The property name to sort by.</param>
        /// <param name="comparer">Defines the method to compare the objects. This parameter
        /// does not work with Entity Framework and should be left out if using Linq to Sql.
        /// </param>
        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(
            this IQueryable<TEntity> query, string propName, IComparer<object> comparer = null)
        {
            var orderedList = ApplyOrder(query, "OrderByDescending", propName, comparer);
            return orderedList;
        }

        /// <summary>
        /// Builds a ThenBy function using a property name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type in the data source.</typeparam>
        /// <param name="query">Object to evaluate queries against the data source.</param>
        /// <param name="propName">The property name to sort by.</param>
        /// <param name="comparer">Defines the method to compare the objects. This parameter
        /// does not work with Entity Framework and should be left out if using Linq to Sql.
        /// </param>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(
            this IOrderedQueryable<TEntity> query, string propName, IComparer<object> comparer = null)
        {
            var orderedList = ApplyOrder(query, "ThenBy", propName, comparer);
            return orderedList;
        }

        /// <summary>
        /// Builds a ThenByDescending function using a property name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type in the data source.</typeparam>
        /// <param name="query">Object to evaluate queries against the data source.</param>
        /// <param name="propName">The property name to sort by.</param>
        /// <param name="comparer">Defines the method to compare the objects. This parameter
        /// does not work with Entity Framework and should be left out if using Linq to Sql.
        /// </param>
        public static IOrderedQueryable<TEntity> ThenByDescending<TEntity>(
            this IOrderedQueryable<TEntity> query, string propName, IComparer<object> comparer = null)
        {
            var orderedList = ApplyOrder(query, "ThenByDescending", propName, comparer);
            return orderedList;
        }

        /// <summary>
        /// Builds the Queryable functions using a TSource property name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type in the data source.</typeparam>
        /// <param name="query">Object to evaluate queries against the data source.</param>
        /// <param name="methodName">OrderBy, OrderByDescending, ThenBy, ThenByDescending.</param>
        /// <param name="propName">The property name to sort by.</param>
        /// <param name="comparer">Defines the method to compare the objects. This parameter
        /// does not work with Entity Framework and should be left out if using Linq to Sql.
        /// </param>
        private static IOrderedQueryable<TEntity> ApplyOrder<TEntity>(
            IQueryable<TEntity> query, string methodName, string propName, IComparer<object> comparer = null)
        {
            if (query == null)
                throw new ArgumentException(
                    UtilResources.Get("Common.ArgumentCanNotBeNull", nameof(query)));
            if (propName == null)
                throw new ArgumentException(
                    UtilResources.Get("Common.ArgumentCanNotBeNull", nameof(propName)));

            var param = Expression.Parameter(typeof(TEntity), "x");
            var body = propName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var orderedList = comparer != null
                ? (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(TEntity), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(TEntity), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );

            return orderedList;
        }
    }
}
