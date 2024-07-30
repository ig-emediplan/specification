using Newtonsoft.Json.Converters;

namespace Emediplan.ChMed23A.Serialization.Converters
{
    /// <summary>
    ///     Extends <see cref="IsoDateTimeConverter" /> and allows to specify the date time format to use by specifying it in
    ///     the attribute.
    /// </summary>
    /// <seealso cref="IsoDateTimeConverter" />
    internal class DateTimeJsonConverter : IsoDateTimeConverter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DateTimeJsonConverter" /> class.
        /// </summary>
        /// <param name="dateTimeFormat">The date time format to use during serialization.</param>
        public DateTimeJsonConverter(string dateTimeFormat)
        {
            DateTimeFormat = dateTimeFormat;
        }
    }
}