using System;

namespace Emediplan.ChMed23A.Exceptions
{
    /// <summary>
    ///     Exception being thrown when an error occurs during a serialization or deserialization operation.
    /// </summary>
    public class ChMed23ASerializerException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChMed23ASerializerException" /> class.
        /// </summary>
        internal ChMed23ASerializerException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChMed23ASerializerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        internal ChMed23ASerializerException(string message)
            : base(message) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChMed23ASerializerException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        internal ChMed23ASerializerException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}