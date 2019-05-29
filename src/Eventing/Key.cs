using System;
using System.Collections.Generic;

namespace Eventing
{
    public abstract class Key
    {
        protected abstract T GetValue<T>(bool throwForInvalidConversion = false);

        public static implicit operator Key(int value)
        {
            return new Key<int>(value);
        }

        public static implicit operator int(Key value)
        {
            return value.GetValue<int>();
        }

        public static implicit operator Key(string value)
        {
            return new Key<string>(value);
        }

        public static implicit operator string(Key value)
        {
            return value.GetValue<string>();
        }

        public static implicit operator Key(float value)
        {
            return new Key<float>(value);
        }

        public static implicit operator float(Key value)
        {
            return value.GetValue<float>();
        }

        public static implicit operator Key(double value)
        {
            return new Key<double>(value);
        }

        public static implicit operator double(Key value)
        {
            return value.GetValue<double>();
        }

        public static implicit operator Key(long value)
        {
            return new Key<long>(value);
        }

        public static implicit operator long(Key value)
        {
            return value.GetValue<long>();
        }

        public static implicit operator Key(DateTime value)
        {
            return new Key<DateTime>(value);
        }

        public static implicit operator DateTime(Key value)
        {
            return value.GetValue<DateTime>();
        }

        public static implicit operator Key(uint value)
        {
            return new Key<uint>(value);
        }

        public static implicit operator uint(Key value)
        {
            return value.GetValue<uint>();
        }

        public static implicit operator Key(Guid value)
        {
            return new Key<Guid>(value);
        }

        public static implicit operator Guid(Key value)
        {
            return value.GetValue<Guid>();
        }
    }

    public class Key<T> : Key, IEquatable<Key<T>>
    {
        public T Value { get; internal set; }

        internal Key()
        {
        }

        public Key(T value)
        {
            Value = value;
        }

        public static implicit operator Key<T>(T value)
        {
            return new Key<T> { Value = value };
        }

        public static implicit operator T(Key<T> value)
        {
            return value.Value;
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:Eventing.Key`1" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Key<T> left, Key<T> right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:Eventing.Key`1" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Key<T> left, Key<T> right)
        {
            return !Equals(left, right);
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (Equals(Value, default(T)))
            {
                return base.ToString();
            }

            return Convert.ToString(Value);
        }

        /// <summary>
        /// Gets the value of this Key as a <typeparamref name="TResult"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="throwForInvalidConversion">
        /// If true, will throw an InvalidCastException.
        /// If false, which is the default, will return the default value of
        /// <typeparamref name="TResult"/>.</param>
        /// <returns></returns>
        protected override TResult GetValue<TResult>(bool throwForInvalidConversion = false)
        {
            if (Value is TResult)
            {
                return (TResult)(object)Value;
            }

            if (throwForInvalidConversion)
            {
                throw new InvalidCastException($"Can't convert typeof({Value.GetType()}) to typeof({typeof(TResult)})");
            }

            return default;
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return ((IEquatable<Key<T>>)this).Equals((Key<T>)obj);
        }

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        /// <param name="other">An object to compare with this object.</param>
        bool IEquatable<Key<T>>.Equals(Key<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }
    }
}