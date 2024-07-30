using System.Collections.Generic;

namespace Emediplan.ChMed23A.Validation.ValidationErrorReferences
{
    public abstract class ValidationErrorRangeReference<T> : ValidationErrorReference
    {
        #region Public Properties

        public T ValueFrom { get; }

        public T ValueTo { get; }

        #endregion

        #region Constructors

        protected ValidationErrorRangeReference(T valueFrom, T valueTo)
        {
            ValueFrom = valueFrom;
            ValueTo = valueTo;
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{ValueFrom}-{ValueTo} ({typeof(T).Name})";

        protected bool Equals(ValidationErrorRangeReference<T> other) =>
            EqualityComparer<T>.Default.Equals(ValueFrom, other.ValueFrom) && EqualityComparer<T>.Default.Equals(ValueTo, other.ValueTo);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValidationErrorRangeReference<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(ValueFrom) * 397) ^ EqualityComparer<T>.Default.GetHashCode(ValueTo);
            }
        }

        #endregion
    }
}