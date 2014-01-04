using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Data.Entity
{
    public class DataRepository<T> : IRepository<T> where T: class
    {
        protected IDataContext _context;           //the internal object context
        private PropertyInfo _objectSetPropertyInfo;

        private string GetPluralName
        {
            get
            {
                PluralizationService service = PluralizationService.CreateService(CultureInfo.CurrentUICulture);
                return service.Pluralize(typeof(T).Name);                
            }
        }

        /// <summary>
        /// Returns the IObjectSet<T> and creates it if it doesnt exist.
        /// </summary>
        protected virtual IDbSet<T> ObjectSet
        {
            get
            {
                if (this._objectSetPropertyInfo == null)
                {
                    this._objectSetPropertyInfo = this._context.GetType().GetProperty(this.GetPluralName);
                }

                return this._objectSetPropertyInfo.GetValue(this._context, null) as IDbSet<T>;
            }
        }

        /// <summary>
        /// DataRepository Constructor
        /// </summary>
        /// <param name="context">The ObjectContext to use.</param>
        public DataRepository(IDataContext context)
        {
            //check if the context is null
            if (context == null)
            {
                //if we have no context throw an exception to alert the coder
                throw new ArgumentException("Data Context provided was NULL.");
            }

            //set the context
            this._context = context;
        }

        public void Add(T entity)
        {
            ObjectSet.Add(entity);
        }

        public void Remove(T entity)
        {
            //remove the entity from the objectset
            this.ObjectSet.Remove(entity);
        }

        T IRepository<T>.Add(T model)
        {
            return ObjectSet.Add(model);
        }

        public IQueryable<T> AsQueryable()
        {
            return ObjectSet.AsQueryable();
        }

        public Task<T> SingleAsync(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.SingleAsync(expression);
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.SingleOrDefaultAsync(expression);
        }

        public Task<bool> AnyAsync()
        {
            return ObjectSet.AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.AnyAsync(expression);
        }

        public Task<int> CountAsync()
        {
            return ObjectSet.CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.CountAsync(expression);
        }
    }
}
