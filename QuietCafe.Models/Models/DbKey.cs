using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuietCafe.Shared.Models
{
    public class DbKey<T> : IEquatable<DbKey<T>>
    {
        public Guid Id { get; init; }

        public DbKey(Guid id)
        {
            Id = id;
        }

        public DbKey()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool Equals(DbKey<T>? other)
        {
            if(other is null)
            {
                return false;
            }

            if(ReferenceEquals(this, other))
            {
                return true;
            }

            return GetType() == other.GetType() && Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DbKey<T>);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
