using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Serialization;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Examples;

public abstract class ExamplesBase
{
    #region Private Fields

    private readonly IChMed23ASerializer _serializer;

    #endregion

    #region Protected Properties

    protected ITestOutputHelper TestOutputHelper { get; }

    #endregion

    #region Constructors

    protected ExamplesBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;

        _serializer = new ChMed23ASerializer();
    }

    #endregion

    #region Protected Methods

    protected void PrintSerializedJson<T>(T obj, MedicationType medicationType, bool validate = true)
        where T : class, IValidatable
    {
        // Act
        var medicationJson = _serializer.Serialize(obj, validate, true, medicationType);

        // Output serialization result
        TestOutputHelper.WriteLine(medicationJson);
    }

    #endregion
}