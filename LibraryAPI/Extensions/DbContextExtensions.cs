using LibraryAPI.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<bool> TryUpdateAsync<T>(this DbContext context, T entity, object key) where T : class
        {
            var dbSet = context.Set<T>();
            var entityInDb = await dbSet.FindAsync(key) ?? throw new NotFoundException($"{typeof(T).Name} with ID {key} not found.");
            context.Entry(entityInDb).CurrentValues.SetValues(entity);
            return await context.SaveChangesAsync() > 0;
        }
        public static async Task<bool> EntityExists<T>(this DbContext context, int entityId, T entity) where T : class
        {
            var dbSet = context.Set<T>();
            return await dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == entityId);
        }
    }
}
