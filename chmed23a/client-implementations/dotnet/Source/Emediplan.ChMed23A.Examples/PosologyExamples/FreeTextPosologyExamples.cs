using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.Enums;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Xunit.Abstractions;

// ReSharper disable StringLiteralTypo

namespace Emediplan.ChMed23A.Examples.PosologyExamples;

public class FreeTextPosologyExamples : ExamplesBase
{
    #region Constructors

    public FreeTextPosologyExamples(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper) { }

    #endregion

    #region Public Tests

    [Fact]
    public void FreeText()
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
                                                                        PosologyDetailObject = new FreeText
                                                                                               {
                                                                                                   Text = @"Angabe der Dosierung für 1x täglich abends:
Ramipril – xyz-Pharma 2,5 mg 20 Tbl. N1 PZN01234567 ≫0-0-1≪
Angabe, dass eine schriftliche Dosierungsanweisung oder ein Medikationsplan vorliegt:
Ramipril – xyz-Pharma 2,5 mg 20 Tbl. N1 PZN01234567 ≫Dj≪"
                                                                                               }
                                                                    }
                                                                }
                                               }
                                           }
                         };

        PrintSerializedJson(medication, medication.MedType.Value);
    }

    [Fact]
    public void FreeText_SpecialCharacters()
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
                                                                        PosologyDetailObject = new FreeText
                                                                                               {
                                                                                                   Text = $"\"'@\\{Environment.NewLine}"
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