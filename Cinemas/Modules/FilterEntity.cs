using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cinemas.Modules
{
    public class FilterEntity
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string OrderBy { get; set; }
        public OrderType? OrderType { get; set; }

        public IQueryable<T> Order<T>(IQueryable<T> Source)
        {
            OrderType = OrderType ?? Modules.OrderType.Asc;
            var Command = OrderType == Modules.OrderType.Desc ? "OrderByDescending" : "OrderBy";
            var Type = typeof(T);
            var Parameter = Expression.Parameter(Type, "p");
            var Property = Type.GetProperty("Id");
            if (!string.IsNullOrEmpty(OrderBy) && OrderType != Modules.OrderType.None)
                Property = Type.GetProperty(OrderBy);
            var PropertyAccess = Expression.MakeMemberAccess(Parameter, Property);
            var OrderByExpression = Expression.Lambda(PropertyAccess, Parameter);
            var ResultExpression = Expression.Call(typeof(Queryable), Command,
                new[] { Type, Property.PropertyType },
                Source.Expression, Expression.Quote(OrderByExpression));
            return Source.Provider.CreateQuery<T>(ResultExpression);
        }
        /// <summary>
        /// Giới hạn kết quả một entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source">Entity</param>
        /// <returns></returns>
        public IQueryable<T> SkipAndTake<T>(IQueryable<T> Source)
        {
            if (Skip.HasValue && Skip >= 0 && Take.HasValue && Take > 0)
            {
                Source = Source.Skip((int)Skip.Value);
                Source = Source.Take((int)Take.Value);
            }
            return Source;
        }
    }
    public enum OrderType
    {
        None = 0,
        Desc = 1,
        Asc = 2
    }
}