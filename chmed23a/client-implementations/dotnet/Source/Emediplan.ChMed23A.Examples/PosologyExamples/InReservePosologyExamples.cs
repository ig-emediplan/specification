using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class InReservePosologyExamples : ExamplesBase
{
    #region Constructors

    public InReservePosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void InReserve_Every6h_Min1_Max4()
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
                                                   Id = "Med1",
                                                   IdType = MedicamentIdType.None,
                                                   IsAutoMedication = false,
                                                   Posologies = new List<Posology>
                                                                {
                                                                    new()
                                                                    {
                                                                        Unit = "TABL",
                                                                        InReserve = true,
                                                                        PosologyDetailObject = new Cyclic
                                                                                               {
                                                                                                   CycleDurationUnit = TimeUnit.Day,
                                                                                                   CycleDuration = 1,
                                                                                                   TimedDosage = new Interval
                                                                                                                 {
                                                                                                                     MinIntervalDurationUnit = TimeUnit.Hour,
                                                                                                                     MinIntervalDuration = 6,
                                                                                                                     Dosage = new DosageRange
                                                                                                                         {
                                                                                                                             MinAmount = 1,
                                                                                                                             MaxAmount = 4
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