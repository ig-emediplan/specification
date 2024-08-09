using Emediplan.ChMed23A.Models.Enums.ObjectTypes;

namespace Emediplan.ChMed23A.Models.SequenceObjects
{
    /// <summary>
    ///     A sequence object describing a pause during which the medication doesn't have to be taken.
    /// </summary>
    /// <seealso cref="SequenceObject" />
    public class Pause : SequenceObject
    {
        #region Public Properties

        public override SequenceObjectType Type => SequenceObjectType.Pause;

        #endregion
    }
}