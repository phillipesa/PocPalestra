using FluentValidation;
using FluentValidation.Results;
using System;

namespace PocPalestra.Domain.Core.Models
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        public Guid Id { get; set; }
        public ValidationResult  ValidationResult  { get; protected set; }
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }
        public abstract bool EhValido();
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
           return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 909) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[id = " + Id + " ]";
        }
    }
}
