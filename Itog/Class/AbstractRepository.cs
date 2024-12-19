using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog.Class
{
    public abstract class Repository<T> where T : Entity
    {
        protected List<T> _entities;
        protected string _filePath;
        protected int _nextId;

        public Repository(string filePath)
        {
            _filePath = filePath;
            _entities = new List<T>();
            _nextId = 1;
        }
        public abstract void Load();
        public abstract void Save();
        public List<T> GetAll() { return _entities; }
        public void Add(T entity)
        {
            entity.Id = _nextId++;
            _entities.Add(entity);
            Save();
        }
        public void Update(T entity)
        {
            int index = _entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _entities[index] = entity;
                Save();
            }
        }
        public void Delete(int id)
        {
            _entities.RemoveAll(e => e.Id == id);
            Save();
        }
    }
}
