using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

// ReSharper disable CommentTypo

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class TopcarePosologyExamples : ExamplesBase
{
    #region Constructors

    public TopcarePosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    /// <summary>
    ///     Example for: Augensalbe 5 Applikationen alle 3 Stunden von 08:00 bis 16:00 Uhr
    /// </summary>
    [Fact]
    public void Every3h_8h_16h_OK()
    {
        var medication = new Medication
                         {
                             Id = "MyId",
                             MedType = MedicationType.MedicationPlan,
                             CreationDate = DateTimeOffset.Now,
                             Patient = new Patient
                                       {
                                           FirstName = "Dora",
                                           LastName = "Graber",
                                           BirthDate = new DateTime(1954, 12, 1),
                                           Gender = Gender.Female,
                                           Language = "de",
                                           Ids = new List<PatientId>
                                                 {
                                                     new()
                                                     {
                                                         Type = PatientIdType.InsuranceCardNumber,
                                                         Value = "756.1234.5678.97"
                                                     }
                                                 }
                                       },
                             Author = MedicationAuthor.Patient,
                             Medicaments = new List<Medicament>
                                           {
                                               new()
                                               {
                                                   Id = "Augensalbe",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "Applikation",
                                                                        PosologyDetailObject = new Sequence
                                                                                               {
                                                                                                   SequenceObjects = new List<SequenceObject>
                                                                                                                     {
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Hour,
                                                                                                                             Duration = 8
                                                                                                                         },
                                                                                                                         new PosologySequence
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Hour,
                                                                                                                             Duration = 8,
                                                                                                                             PosologyDetailObject = new Cyclic
                                                                                                                                 {
                                                                                                                                     CycleDurationUnit = TimeUnit.Hour,
                                                                                                                                     CycleDuration = 3,
                                                                                                                                     TimedDosage = new DosageOnly
                                                                                                                                         {
                                                                                                                                             Dosage = new DosageSimple {Amount = 5}
                                                                                                                                         }
                                                                                                                                 }
                                                                                                                         },
                                                                                                                         new Pause
                                                                                                                         {
                                                                                                                             DurationUnit = TimeUnit.Hour,
                                                                                                                             Duration = 8
                                                                                                                         }
                                                                                                                     }
                                                                                               }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    #endregion
}