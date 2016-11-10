using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataRepository
{
    public abstract class IEntity
    {
        [Required]
        public int Id { get; set; }

    }

    public interface IRepository<T> where T : IEntity
    {

        //IEnumerable<T> List { get; }
        IEnumerable<T> List();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);

    }
}

  
