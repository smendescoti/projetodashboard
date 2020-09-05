using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly DataContext dataContext;

        protected BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public virtual void Create(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Added;
            dataContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Deleted;
            dataContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return dataContext.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            return dataContext.Set<TEntity>().Find(id);
        }
    }
}
