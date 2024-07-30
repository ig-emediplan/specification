using System;
using Emediplan.ChMed23A.Serialization;
using Emediplan.ChMed23A.Validation;
using KellermanSoftware.CompareNetObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization;

public abstract class SerializationDeserializationTestsBase
{
    #region Private Fields

    private readonly IChMed23ASerializer _serializer;
    private readonly CompareLogic _compareLogic;

    #endregion

    #region Protected Properties

    protected ITestOutputHelper TestOutputHelper { get; }

    #endregion

    #region Constructors

    protected SerializationDeserializationTestsBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;

        _serializer = new ChMed23ASerializer();

        _compareLogic = new CompareLogic
                        {
                            Config = new ComparisonConfig
                                     {
                                         // IgnoreObjectTypes has to be enabled because generic lists/collections
                                         // are being used in the model and there is no guarantee the instantiated
                                         // types will be the same before and after serialization
                                         IgnoreObjectTypes = true,
                                         DateTimeKindToUseWhenUnspecified = DateTimeKind.Unspecified
                                     }
                        };
    }

    #endregion

    #region Protected Methods

    protected void TestSerialization<T>(T obj, MedicationType medicationType)
        where T : class, IValidatable
    {
        // Act
        var medicationJson = _serializer.Serialize(obj, true, true, medicationType);

        // Output serialization result
        TestOutputHelper.WriteLine(medicationJson);

        // De-serialize
        var objDeserialized = _serializer.Deserialize<T>(medicationJson);

        // Assert original and serialized/deserialized medications are equal
        var compareResult = _compareLogic.Compare(obj, objDeserialized);
        Assert.True(compareResult.AreEqual, compareResult.DifferencesString);
    }

    #endregion
}