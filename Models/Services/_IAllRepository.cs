using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_Tracker.Models.Services
{
    internal interface _IAllRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
