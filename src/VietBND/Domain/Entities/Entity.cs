using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VietBND.Domain.Entities
{
    public class Entity : Entity<int>
    {
    }
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>,IAudited
    {
        [Key]
        public TPrimaryKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public int? TenantId { get; set; }

        public virtual bool IsTransient()
        {
            if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TPrimaryKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TPrimaryKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
    }
}
