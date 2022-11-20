using Microsoft.EntityFrameworkCore;
using StudentManagementProject.Data.Context;
using System.Linq.Expressions;

namespace StudentManagementProject.Repositories
{
    //A generic repository that all models will use in common has been written
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> table;
        public BaseRepository(ApplicationDbContext _context)
        {
            context = _context;
            table = context.Set<T>();
        }

        public void Add(T entity)
        {
            table.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T? existing = table.Find(id);
            table.Remove(existing);
            context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public DbSet<T>? GetEntity()
        {
            return table;
        }

        public T? GetById(int id)
        {
            return table.Find(id);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

}
