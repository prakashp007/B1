using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Expense_Tracker.Models.Services
{
    public class AllRepository<T> : _IAllRepository<T> where T : class
    {
        private DatabaseConnection dbconection;
        private IDbSet<T> entities;
        public AllRepository()
        {
            dbconection = new DatabaseConnection();
            entities = dbconection.Set<T>();
        }
        public void Delete(object id)
        {
            var existingRecod = entities.Find(id);
            entities.Remove(existingRecod);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetByID(object id)
        {
            return entities.Find(id);
        }

        public void Insert(T entity)
        {
            entities.Add(entity);
        }

        public void Update(T entity)
        {
            dbconection.Entry(entity).State = EntityState.Modified;
        }

        public void SaveAll()
        {
            dbconection.SaveChanges();

        }
    }
}