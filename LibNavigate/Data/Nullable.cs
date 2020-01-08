using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNavigate.Data
{
    public sealed class Nullable<T>
    {
        private readonly T value;

        public readonly bool HasValue;

        public T Value
        {
            get
            {
                if(!HasValue)
                {
                    throw new InvalidOperationException("Does not have value");
                }

                return value;
            }
        }
        public Nullable(T value)
        {
            this.value = value;

            HasValue = true;
        }

        private Nullable()
        {
            this.value = default(T);

            HasValue = false;
        }

        public static Nullable<T> From(T value)
        {
            return new Nullable<T>(value);
        }

        public static Nullable<T> Empty()
        {
            return new Nullable<T>();
        }

        public T GetValueOrDefault(T defaultValue)
        {
            return HasValue ? value : defaultValue;
        }

        public override bool Equals(object other)
        {
            if (!HasValue) return other == null;
            if (other == null) return false;
            return value.Equals(other);
        }

        public override int GetHashCode()
        {
            return HasValue ? value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return HasValue ? value.ToString() : "";
        }


    }
}
