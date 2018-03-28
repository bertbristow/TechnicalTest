using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Interview
{
    // Please create an in memory implementation of IRepository<T> 

    public interface IRepository<T> where T : IStoreable
    {
        IEnumerable<T> All();
        void Delete(IComparable id);
        void Save(T item);
        T FindById(IComparable id);
    }

    public class Repository<T> : IRepository<T> where T : IStoreable
    {
        private ICollection<T> dataStore;

        public Repository() : this(new HashSet<T>())
        {
        }

        public Repository(ICollection<T> dataStore)
        {
            if (dataStore == null) throw new ArgumentNullException(nameof(dataStore));

            this.dataStore = dataStore;
        }

        public IEnumerable<T> All()
        {
            return this.dataStore;
        }

        public void Delete(IComparable id)
        {
            T entity = this.FindById(id);
                                            
            this.dataStore.Remove(entity);
        }

        public void Save(T item)
        {
            if(item == null) throw new ArgumentNullException(nameof(item));

            this.dataStore.Add(item);
        }

        public T FindById(IComparable id)
        {
            if(id == null) throw new ArgumentNullException(nameof(id));
           
            return this.dataStore.First(item => Equals(item.Id, id));
        }
    }
}
