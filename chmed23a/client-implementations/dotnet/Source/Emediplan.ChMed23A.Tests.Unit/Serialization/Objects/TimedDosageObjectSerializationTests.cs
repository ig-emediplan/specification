using System;
using System.Collections.Generic;
using Emediplan.ChMed23A.Models.ApplicationObjects;
using Emediplan.ChMed23A.Models.DosageObjects;
using Emediplan.ChMed23A.Models.TimedDosageObjects;
using Xunit.Abstractions;
using DayOfWeek = Emediplan.ChMed23A.Models.Enums.DayOfWeek;

namespace Emediplan.ChMed23A.Tests.Unit.Serialization.Objects;

public class TimedDosageObjectSerializationTests : SerializationDeserializationTestsBase
{
    public TimedDosageObjectSerializationTests(ITestOutputHelper output)
        : base(output) { }

    [Fact]
    public void WhenDeserializingSerializedDosageOnly_ReturnsEqualObject()
    {
        // Arrange
        var dosageOnly = new DosageOnly
                         {
                             Dosage = new DosageSimple
                                      {
                                          Amount = 1
                                      }
                         };

        // Act and Assert
        TestSerialization(dosageOnly, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedTimes_ReturnsEqualObject()
    {
        // Arrange
        var times = new Times
                    {
                        Applications = new List<ApplicationAtTime>
                                       {
                                           new()
                                           {
                                               TimeOfDay = new TimeSpan(8, 0, 0),
                                               Dosage = new DosageSimple
                                                        {
                                                            Amount = 1
                                                        }
                                           }
                                       }
                    };

        // Act and Assert
        TestSerialization(times, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedDaySegments_ReturnsEqualObject()
    {
        // Arrange
        var daySegments = new DaySegments
                          {
                              Applications = new List<ApplicationInSegment>
                                             {
                                                 new()
                                                 {
                                                     Segment = DaySegment.Evening,
                                                     Dosage = new DosageSimple
                                                              {
                                                                  Amount = 1
                                                              }
                                                 }
                                             }
                          };

        // Act and Assert
        TestSerialization(daySegments, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedWeekDays_ReturnsEqualObject()
    {
        // Arrange
        var weekDays = new WeekDays
                       {
                           DaysOfWeek = new List<DayOfWeek>
                                        {
                                            DayOfWeek.Monday,
                                            DayOfWeek.Wednesday,
                                            DayOfWeek.Friday
                                        },
                           TimedDosage = new DosageOnly
                                         {
                                             Dosage = new DosageSimple
                                                      {
                                                          Amount = 1
                                                      }
                                         }
                       };

        // Act and Assert
        TestSerialization(weekDays, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedDaysOfMonth_ReturnsEqualObject()
    {
        // Arrange
        var daysOfMonth = new DaysOfMonth
                          {
                              Days = new[] {1, 15},
                              TimedDosage = new DosageOnly
                                            {
                                                Dosage = new DosageSimple
                                                         {
                                                             Amount = 1
                                                         }
                                            }
                          };

        // Act and Assert
        TestSerialization(daysOfMonth, MedicationType.MedicationPlan);
    }

    [Fact]
    public void WhenDeserializingSerializedInterval_ReturnsEqualObject()
    {
        // Arrange
        var timedDosageObject = new Interval
                                {
                                    MinIntervalDuration = 6,
                                    MinIntervalDurationUnit = TimeUnit.Hour,
                                    Dosage = new DosageSimple
                                             {
                                                 Amount = 1
                                             }
                                };

        // Act and Assert
        TestSerialization(timedDosageObject, MedicationType.MedicationPlan);
    }
}