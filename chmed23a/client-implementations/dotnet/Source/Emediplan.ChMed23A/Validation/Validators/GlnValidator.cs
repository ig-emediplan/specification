using System.Text.RegularExpressions;

namespace Emediplan.ChMed23A.Validation.Validators
{
    /// <summary>
    ///     Validates GLNs
    /// </summary>
    internal class GlnValidator
    {
        private const string GlnPattern = @"^\d{13}$";

        /// <summary>
        ///     Validate the GLN. Checks that the GLN consists of 13 digits and verifies that the check digit is valid.
        /// </summary>
        /// <param name="gln"></param>
        /// <returns><c>True</c> if the GLN is valid; otherwise <c>false</c>.</returns>
        public bool Validate(string gln)
        {
            if (gln == null || !Regex.IsMatch(gln, GlnPattern))
                return false;

            // The GLN has the expected format, make sure the check digit is correct
            var checkDigitString = gln.Substring(12, 1);
            var dataPortion = gln.Substring(0, 12);

            var calculatedCheckDigit = CalculateGlnCheckDigit(dataPortion);
            var actualCheckDigit = int.Parse(checkDigitString);
            return calculatedCheckDigit == actualCheckDigit;
        }

        private static int CalculateGlnCheckDigit(string dataPortion)
        {
            var sum = 0;

            for (var i = 0; i < dataPortion.Length; i++)
            {
                var digit = int.Parse(dataPortion[i].ToString());
                sum += (i % 2 == 0) ? digit : digit * 3;
            }

            var remainder = sum % 10;
            var checkDigit = (remainder == 0) ? 0 : 10 - remainder;

            return checkDigit;
        }
    }
}