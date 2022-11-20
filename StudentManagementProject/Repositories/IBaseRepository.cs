using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StudentManagementProject.Repositories
{
    public interface IBaseRepository<T> where T : class 
    {
        List<T> GetAll();
        DbSet<T> GetEntity();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
