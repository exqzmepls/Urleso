using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Urleso.Persistence;

internal static class Extensions
{
    public static IndexBuilder<TEntity> HasUniqueIndex<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, object?>> indexExpression
    )
        where TEntity : class
    {
        return builder.HasIndex(indexExpression).IsUnique();
    }
}