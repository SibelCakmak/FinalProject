using Cor.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // generic constraint : kısıtlama
    // class : T için sadece class referanslılar girilsin
    // IEntity : sadece IEntity yada ondan referans alanları yaz
    // new : new lwnwbilir olmalı. IEntitiy interface olduğu için IEntity kullanılmaz.
    // onu referans alan snıflar olabilir.
    
    public interface IEntityRepository<T> where T:class,IEntity,new()
        
    {
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        // tek data getrmek için kullanılır.
        // null ı koymamaızın sebebi bir Expression girmek zorunda olması
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Uptade(T entity);
        void Delete(T entity); 
    }
}
