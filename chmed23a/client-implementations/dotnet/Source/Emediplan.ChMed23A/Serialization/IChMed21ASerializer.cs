using Emediplan.ChMed23A.Exceptions;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Validation;

namespace Emediplan.ChMed23A.Serialization
{
    /// <summary>
    ///     Serializer for ChMed23A models.
    /// </summary>
    public interface IChMed23ASerializer
    {
        /// <summary>
        ///     Serializes the object as JSON string. The object can optionally be validated before being serialized.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize. It must extend <see cref="IValidatable" />.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="prettyPrint">
        ///     <c>true</c> if the JSON should be pretty printed; otherwise, if it should be written as a
        ///     single line, <c>false</c>.
        /// </param>
        /// <param name="validate"><c>true</c> if the object should be validated before being serialized; otherwise <c>false</c>.</param>
        /// <param name="medicationType">
        ///     The medication type for which to validate the object. Only required if validate ==
        ///     <c>true</c>.
        /// </param>
        /// <exception cref="ChMed23ASerializerException">Thrown if validation has been enabled and a validation error occurs.</exception>
        /// <returns>The object, serialized as JSON string.</returns>
        string Serialize<T>(T obj, bool prettyPrint = false, bool validate = false, MedicationType? medicationType = null) where T : class, IValidatable;

        /// <summary>
        ///     Deserializes the JSON string. The object can optionally be validated before being serialized.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize. It must extend <see cref="IValidatable" />.</typeparam>
        /// <param name="jsonString">The JSON string to deserialize.</param>
        /// <param name="validate"><c>true</c> if the object should be validated before being serialized; otherwise <c>false</c>.</param>
        /// <param name="medicationType">
        ///     The medication type for which to validate the object. Only required if validate ==
        ///     <c>true</c>.
        /// </param>
        /// <exception cref="ChMed23ASerializerException">Thrown if validation has been enabled and a validation error occurs.</exception>
        /// <returns>The object, serialized as JSON string.</returns>
        T Deserialize<T>(string jsonString, bool validate = false, MedicationType? medicationType = null) where T : class, IValidatable;
    }
}