using System;

namespace Store.Core.DomainObjects
{
    public abstract class Entity
    {
        // Com Id Guid podemos saber o Id da entidade antes dela ser persistida
        public Guid Id { get; set; }

        // Constutor protected já que a classe é abstrata
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 752) + Id.GetHashCode();
        }

        public override string ToString()
        {
            // Interpolação
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
