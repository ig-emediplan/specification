using System.Collections.Generic;

namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public abstract class ValidationErrorValueReference<T> : ValidationErrorReference
    {
        #region Public Properties

        public T Value { get; }

        #endregion

        #region Constructors

        protected ValidationErrorValueReference(T value)
        {
            Value = value;
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{Value} ({typeof(T).Name})";

        protected bool Equals(ValidationErrorValueReference<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValidationErrorValueReference<T>)obj);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);

        #endregion
    }
}