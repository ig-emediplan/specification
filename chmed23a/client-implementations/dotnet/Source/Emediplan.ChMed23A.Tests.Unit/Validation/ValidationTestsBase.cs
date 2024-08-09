using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emediplan.ChMed23A.Models;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.Constants;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.PosologyDetailObjects;
using Emediplan.ChMed23A.Models.SequenceObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Emediplan.ChMed23A.Validation;
using Xunit.Abstractions;
using DayOfWeek = Emediplan.ChMed23A.Models.Enums.DayOfWeek;
using Single = Emediplan.ChMed23A.Models.PosologyDetailObjects.Single;

namespace Emediplan.ChMed23A.Tests.Unit.Validation;

public abstract class ValidationTestsBase
{
    #region Protected Properties

    protected ITestOutputHelper TestOutputHelper { get; }

    #endregion

    #region Constructors

    protected ValidationTestsBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    #endregion

    #region Protected Methods

    protected void OutputErrors(ICollection<ValidationError> errors)
    {
        if (!errors.Any())
        {
            TestOutputHelper.WriteLine("No validation errors");
            return;
        }

        TestOutputHelper.WriteLine($"{errors.Count} validation errors occurred:");
        TestOutputHelper.WriteLine(string.Empty);

        var errorIndex = 0;

        foreach (var error in errors)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Error {errorIndex++}:");
            stringBuilder.AppendLine($"{nameof(ValidationError.PropertyPath)}: {error.PropertyPath}");
            stringBuilder.AppendLine($"{nameof(ValidationError.PropertyPathJson)}: {error.PropertyPathJson}");
            stringBuilder.AppendLine($"{nameof(ValidationError.Reason)}: {error.Reason}");

            if (error.Reference != null)
                stringBuilder.AppendLine($"{nameof(ValidationError.Reference)}: {error.Reference}");

            stringBuilder.AppendLine($"{nameof(ValidationError.Message)}: {error.Message}");

            TestOutputHelper.WriteLine(stringBuilder.ToString());
        }
    }

    #region Get minimal valid objects

    protected static Medication GetMinimalValidMedication(MedicationType medicationType) =>
        new()
        {
            Author = MedicationAuthor.HealthcarePerson,
            HealthcarePerson = GetMinimalValidHealthcarePerson(),
            HealthcareOrganization = GetMinimalValidHealthcareOrganization(),
            Patient = GetMinimalValidPatient(medicationType),
            CreationDate = DateTimeOffset.Now,
            Id = "MyId",
            MedType = MedicationType.MedicationPlan,
            Remark = "MyRemark",
            Medicaments = medicationType == MedicationType.MedicationPlan
                              ? null
                              : new List<Medicament>
                                {
                                    GetMinimalValidMedicament()
                                }
        };

    protected static Patient GetMinimalValidPatient(MedicationType medicationType) =>
        new()
        {
            FirstName = "Dora",
            LastName = "Graber",
            BirthDate = new DateTime(1950, 02, 05),
            Gender = Gender.Female,
            Street = "Untermattenweg 8",
            Zip = "3027",
            City = "Bern",
            Language = medicationType == MedicationType.MedicationPlan ? "DE" : null,
            Ids = new List<PatientId>
                  {
                      GetMinimalValidPatientId()
                  }
        };

    protected static PatientId GetMinimalValidPatientId() =>
        new()
        {
            Type = PatientIdType.LocalPid,
            SystemIdentifier = "Galenica-1.2.3.4.5",
            Value = "12345"
        };

    protected static Medicament GetMinimalValidMedicament() =>
        new()
        {
            Id = "MyMed1",
            IdType = MedicamentIdType.None,
            IsAutoMedication = false
        };

    protected static HealthcarePerson GetMinimalValidHealthcarePerson() =>
        new()
        {
            FirstName = "Lena",
            LastName = "De Santos",
            Gln = "7601001362383"
        };

    protected static HealthcareOrganization GetMinimalValidHealthcareOrganization() =>
        new()
        {
            Name = "HCI Solutions AG",
            Street = "Untermattweg 8",
            Zip = "3000",
            City = "Bern",
            Gln = "7601001362383"
        };

    protected static RiskCategory GetMinimalValidRiskCategory() =>
        new()
        {
            Id = RiskCategoryCodeConstants.Driver
        };

    protected static Extension GetMinimalValidExtension() =>
        new()
        {
            Name = "P1",
            Schema = "test"
        };

    protected static MedicalData GetMinimalValidMedicalData() => new();

    protected static Posology GetMinimalValidPosology(MedicationType medicationType) =>
        new()
        {
            Unit = medicationType == MedicationType.MedicationPlan ? "ml" : null,
            PosologyDetailObject = GetMinimalValidDaily()
        };

    #region Posology detail objects

    protected static Daily GetMinimalValidDaily() =>
        new()
        {
            Dosages = new[] {1.0, 0, 0, 0}
        };

    protected static FreeText GetMinimalValidFreeText() =>
        new()
        {
            Text = "Apply once in the morning"
        };

    protected static Single GetMinimalValidSingle() =>
        new()
        {
            TimedDosage = GetMinimalValidDosageOnly()
        };

    protected static Cyclic GetMinimalValidCyclic() =>
        new()
        {
            TimedDosage = GetMinimalValidDosageOnly(),
            CycleDuration = 3,
            CycleDurationUnit = TimeUnit.Hour
        };

    protected static Sequence GetMinimalValidSequence() =>
        new()
        {
            SequenceObjects = new List<SequenceObject>
                              {
                                  GetMinimalValidPosologySequence()
                              }
        };

    #endregion

    #region Timed dosage objects

    protected static DaySegments GetMinimalValidDaySegments() =>
        new()
        {
            Applications = new List<ApplicationInSegment>
                           {
                               GetMinimalValidApplicationInSegment()
                           }
        };

    protected static DaysOfMonth GetMinimalValidDaysOfMonth() =>
        new()
        {
            Days = new[] {1},
            TimedDosage = GetMinimalValidDosageOnly()
        };

    protected static DosageOnly GetMinimalValidDosageOnly() =>
        new()
        {
            Dosage = GetMinimalValidDosageSimple()
        };

    protected static Times GetMinimalValidTimes() =>
        new()
        {
            Applications = new List<ApplicationAtTime>
                           {
                               GetMinimalValidApplicationAtTime()
                           }
        };

    protected static WeekDays GetMinimalValidWeekDays() =>
        new()
        {
            DaysOfWeek = new[] {DayOfWeek.Tuesday},
            TimedDosage = GetMinimalValidDosageOnly()
        };

    protected static Interval GetMinimalValidInterval() =>
        new()
        {
            Dosage = GetMinimalValidDosageSimple(),
            MinIntervalDuration = 1,
            MinIntervalDurationUnit = TimeUnit.Hour
        };

    #endregion

    #region Dosage objects

    protected static DosageSimple GetMinimalValidDosageSimple() =>
        new()
        {
            Amount = 7
        };

    protected static DosageFromTo GetMinimalValidDosageFromTo() =>
        new()
        {
            AmountFrom = 1,
            AmountTo = 5,
            DurationUnit = TimeUnit.Hour,
            Duration = 1
        };

    protected static DosageRange GetMinimalValidDosageRange() =>
        new()
        {
            MinAmount = 1,
            MaxAmount = 3
        };

    #endregion

    #region Application objects

    protected static ApplicationAtTime GetMinimalValidApplicationAtTime() =>
        new()
        {
            Dosage = GetMinimalValidDosageSimple(),
            TimeOfDay = new TimeSpan(8, 0, 0)
        };

    protected static ApplicationInSegment GetMinimalValidApplicationInSegment() =>
        new()
        {
            Dosage = GetMinimalValidDosageSimple(),
            Segment = DaySegment.Evening
        };

    #endregion

    #region Sequence objects

    protected static PosologySequence GetMinimalValidPosologySequence() =>
        new()
        {
            Duration = 7,
            DurationUnit = TimeUnit.Day,
            PosologyDetailObject = GetMinimalValidSingle()
        };

    protected static Pause GetMinimalValidPause() =>
        new()
        {
            Duration = 7,
            DurationUnit = TimeUnit.Day
        };

    #endregion

    #endregion

    #endregion
}