using Cor.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class IfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity: class,IEntity,new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            //c# da işi bitince siler.(Garbage Collector)
            using (TContext context = new TContext())
            {
                // git veri kaynağından gönderdiğim productan bir nesneyi eşleştir
                // Direk ekler.
                var addedEntity = context.Entry(entity);// ilişkilendirir
                addedEntity.State = EntityState.Added;// veri tabnına ekler.
                context.SaveChanges();// işlemleri yapar kaydeder.
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // git veri kaynağından gönderdiğim productan bir nesneyi eşleştir.
                var deletedEntity = context.Entry(entity);// ilişkilendirir
                deletedEntity.State = EntityState.Deleted;// veri tabnından siler.
                context.SaveChanges();// işlemleri yapar kaydeder.
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // İF KOŞULU SELECT * FROM Products u listeye çevirir.
                return filter == null ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Uptade(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // git veri kaynağından gönderdiğim productan bir nesneyi eşleştir
                var updatedEntity = context.Entry(entity);// ilişkilendirir
                updatedEntity.State = EntityState.Modified;// veri tabanını günceller.
                context.SaveChanges();// işlemleri yapar kaydeder.
            }
        }
    }
}
