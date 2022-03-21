using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.Domain
{
    public abstract class Entity<TPrimaryKey>
    {
        private TPrimaryKey _key;

        protected Entity(TPrimaryKey key)
        {
            _key = key;
        }

        protected Entity() : this(default(TPrimaryKey))
        {
        }


        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity<TPrimaryKey>))
            {
                return false;
            }
            return this == (Entity<TPrimaryKey>)obj;
        }

        public static bool operator ==(Entity<TPrimaryKey> entity1, Entity<TPrimaryKey> entity2)
        {
            if (entity1 == null && entity2 == null)
            {
                return true;
            }
            if (entity1 == null || entity2 == null)
            {
                return false;
            }
            if (!EqualityComparer<TPrimaryKey>.Default.Equals(entity1.Id,entity2.Id))
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(Entity<TPrimaryKey> entity1, Entity<TPrimaryKey> entity2)
        {
            return entity1 != entity2;
        }

        public TPrimaryKey Id { get { return _key; } }

        public override int GetHashCode()
        {
            return _key.GetHashCode();
        }
    }
}
