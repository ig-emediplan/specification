# eMedication Plan ChMed23A Examples

**Contact**

Geschäftsstelle IG eMediplan<br>
c/o Köhler, Stüdeli & Partner GmbH<br>
Amthausgasse 18, 3011 Bern<br>
Tel. +41 (0)31 560 00 24<br>
info@emediplan.ch

## Introduction

This document shows examples of the ChMed23A based on some use cases. The use cases are fictitious and the choice of medication is random and does not constitute advertising or therapy recommendations.

## Examples

### Example 1

**Document:** Medication plan

**Created (date and time):** 09.01.2024 09:14:36

**Patient:**

- First name: Alice
- Last name: Louloui
- Date of birth: 19.01.1945
- Gender: Female
- Address: Bernstrasse 1, 3000 Bern, Switzerland
- Language: German
- Insurance card number: 87588895877545621
- Phone numbers: 011 111 11 11 and 079 999 99 99
- E-mail address: alicelouloui1945@fake-e-mail.ch

**Medical Data:**

- Weight: 70 kg
- Height: 165 cm
- Risks:
    - Mild renal insufficiency (risk category 1) with a Clcr within the range of ≥60–89 ml/min (Niereninsuffizienz, leichte (Clcr ≥60–89 ml/min), risk id 577)
    - Liver insufficiency (risk category 2): Unknown
    - Reproduction (risk category 3): No
    - Competitive athlete (risk category 4): No
    - Operating vehicles/machines (risk category 5): No
    - Allergies (risk category 6): Analgesic allergy (Analgetika-Allergie, code 503)
    - Diabetes type 2 (risk category 7): (Diabetes mellitus Typ 2, code 780)

**HealthcarePerson = author of the document:**

- GLN: 123123123123
- First name: Hans
- Last name: Muster

**Healthcare Organization:**

- Name: Medical practice Dr. med. Hans Muster
- Address: Bernstrasse 1, 3000 Bern, Switzerland

**Medication:**

- METFORMIN Mepha Filmtabl 1000 mg (Id 1246564, IdType: 4)
    - Reason for the medication treatment: Pancreas
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: After
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Daily dosage of 1 pill in the morning and 1 pill in the evening
- ATORVASTATIN axapharm Filmtabl 10 mg (Id 5292958, IdType: 3)
    - Reason for the medication treatment: Cholesterol-lowering drug
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Posology:
        - Start date of the medication treatment: 25.05.2012
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: Oral use (Zum Einnehmen, code 20053000)
        - Method of administration: Swallowing (Schlucken, code 19)
        - Posology details:
            - Daily dosage of 1 pill in the evening
- VITAMIN D3 Streuli 4000 IE/ml Prophylax (Id 7680334810013, IdType: 2)
    - Reason for the medication treatment: Vitamins/minerals
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Posology:
        - Start date of the medication treatment: 20.09.2023
        - End date of the medication treatment: 30.04.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Milliliter (Milliliter, code ml)
        - Application instructions: Dose using the dosing pipette, place on a spoon and then take undiluted. The pipette must not come into contact with the mouth, saliva or food.
        - Route of administration: Oral use (Zum Einnehmen, code 20053000)
        - Method of administration: Not specified (keine Angaben, code 20)
        - Posology details:
            - Once a week 1.4 milliliter
- BURGERSTEIN Zinkvital Tabl 14 mg (Id 1512856, IdType: 4)
    - Reason for the medication treatment: Vitamins/minerals
    - Automedication/self-medication: Yes
    - Prescribed by: -
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Daily 1 pill at 09:00 a.m.
- VOLTAREN Emulgel 1 % (Id 7680473440263, IdType: 2)
    - Reason for the medication treatment: Rheumatism
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Application (2 gramm Diclofenac natrium) (Applikation, code Appl)
        - Application instructions: -
        - Route of administration: Use on the skin (Anwendung auf der Haut, code 20003000)
        - Method of administration: Application (Applikation, code 5)
        - Posology details:
            - Three times a day

```json5
{
  "patient": {
    "fName": "Alice",
    "lName": "Louloui",
    "bdt": "1945-01-19",
    "gender": 2,
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH",
    "lng": "DE",
    "phones": [
      "011 111 11 11",
      "079 999 99 99"
    ],
    "emails": [
      "alicelouloui1945@fake-e-mail.ch"
    ],
    "ids": [
      {
        "type": 1,
        "val": "87588895877545621"
      }
    ],
    "mData": {
      "rcs": [
        {
          "id": 1,
          "rIds": [
            577
          ]
        },
        {
          "id": 3
        },
        {
          "id": 4
        },
        {
          "id": 5
        },
        {
          "id": 6,
          "rIds": [
            503
          ]
        },
        {
          "id": 7,
          "rIds": [
            780
          ]
        }
      ],
      "w": 70.0,
      "h": 165.0
    }
  },
  "meds": [
    {
      "id": "1246564",
      "idType": 4,
      "pos": [
        {
          "po": {
            "t": 1,
            "ds": [
              1,
              0,
              1,
              0
            ]
          },
          "relMeal": 3,
          "inRes": false,
          "unit": "Stk"
        }
      ],
      "rsn": "Pancreas",
      "autoMed": false,
      "prscbBy": "123123123123",
      "nbPack": 1.0
    },
    {
      "id": "5292958",
      "idType": 3,
      "pos": [
        {
          "dtFrom": "2012-05-25",
          "po": {
            "t": 1,
            "ds": [
              0,
              0,
              1,
              0
            ]
          },
          "inRes": false,
          "roa": "20053000",
          "moa": "19",
          "unit": "Stk"
        }
      ],
      "rsn": "Cholesterol-lowering drug",
      "autoMed": false,
      "prscbBy": "123123123123",
      "nbPack": 1.0
    },
    {
      "id": "7680334810013",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2023-09-20",
          "dtTo": "2024-04-30",
          "po": {
            "t": 4,
            "cyDuU": 5,
            "cyDu": 1,
            "tdo": {
              "t": 1,
              "do": {
                "t": 1,
                "a": 1.4
              }
            },
            "tdpc": 1
          },
          "inRes": false,
          "roa": "20053000",
          "moa": "20",
          "unit": "ml",
          "appInstr": "Dose using the dosing pipette, place on a spoon and then take undiluted. The pipette must not come into contact with the mouth, saliva or food."
        }
      ],
      "rsn": "Vitamins/minerals",
      "autoMed": false,
      "prscbBy": "123123123123",
      "nbPack": 1.0
    },
    {
      "id": "1512856",
      "idType": 4,
      "pos": [
        {
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 2,
              "ts": [
                {
                  "dt": "09:00:00",
                  "do": {
                    "t": 1,
                    "a": 1.0
                  }
                }
              ]
            },
            "tdpc": 1
          },
          "inRes": false,
          "unit": "Stk"
        }
      ],
      "rsn": "Vitamins/minerals",
      "autoMed": true,
      "nbPack": 1.0
    },
    {
      "id": "7680473440263",
      "idType": 2,
      "pos": [
        {
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 1,
              "do": {
                "t": 1,
                "a": 1.0
              }
            },
            "tdpc": 3
          },
          "inRes": false,
          "roa": "20003000",
          "moa": "5",
          "unit": "Appl"
        }
      ],
      "rsn": "Rheumatism",
      "autoMed": false,
      "prscbBy": "123123123123",
      "nbPack": 1.0
    }
  ],
  "medType": 1,
  "auth": 1,
  "dt": "2024-01-09T09:14:36.0000000+01:00",
  "hcPerson": {
    "gln": "123123123123",
    "fName": "Hans",
    "lName": "Muster"
  },
  "hcOrg": {
    "name": "Medical practice Dr. med. Hans Muster",
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH"
  }
}
```

### Example 2

**Document:** Medication plan

**Created (date and time):** 10.01.2024 15:54:06

**Patient:**

- First name: Urs
- Last name: Mustermann
- Date of birth: 15.05.1968
- Gender: Male
- Address: Bernstrasse 1, 3000 Bern, Switzerland
- Insurance card number: 87588895877545621
- Language: German
- Phone numbers: 079 999 99 99

**Medical Data:**

- Weight: -
- Height: -
- Risks:
    - Renal insufficiency (risk category 1): Unknown
    - Liver insufficiency (risk category 2): Unknown
    - Competitive athlete (risk category 4): No
    - Operating vehicles/machines (risk category 5): Exposure to potentially dangerous situations, such as driving vehicles, operating machinery, or working at great heights (Potenziell gefährlichen Situationen ausgesetzt, wie beispielsweise dem Führen von Fahrzeugen, dem Bedienen von Maschinen oder dem Arbeiten in grossen Höhen, risk id 615)
    - Allergies (risk category 6): No
    - Diabetes (risk category 7): No

**Healthcare Person = author of the document:**

- First name: Hans
- Last name: Beispiel

**Healthcare Organization:**

- GLN: 55255255255
- Name: Arztpraxis Sonnenschein
- Address: Bernstrasse 1, 3000 Bern, Switzerland

**Medication:**

- EUTHYROX 75 Tabl 75 mcg (Id 7680549490031, IdType: 2)
    - Reason for the medication treatment: Thyroid gland
    - Automedication/self-medication: No
    - Prescribed by: 45745874584
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: Before
        - Unit: Piece (Stück, code Stk)
        - Application instructions: Take on an empty stomach, at least 30 minutes before breakfast.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 pill at 07:30 a.m. on Tuesday and Thursday and 1 pill at 09.00 a.m. on Saturday
- SINTROM 4 Tabl 4 mg (Id 7680216930020, IdType: 2)
    - Reason for the medication treatment: Blood thinner
    - Automedication/self-medication: No
    - Prescribed by: 55255255255
    - Posology:
        - Start date of the medication treatment: 10.01.2024
        - End date of the medication treatment: 29.02.2024
        - Reserve medication: No
        - Relative to meal: Before
        - Unit: Piece (Stück, code Stk)
        - Application instructions: Next doctor's appointment on 29.02.2024
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Starting from 10.01.2024 2 days ½ pill in the evening, then 1 day pause (0 pills), then 3 days ¾ pill in the evening, then 1 day pause (0 pills) and repeat all steps all over again until the next doctor's appointment on 29.02.2024
- REPATHA Inj Lös 140 mg/ml Pen (Id 7680656220026, IdType: 2)
    - Reason for the medication treatment: Cholesterol-lowering drug
    - Automedication/self-medication: No
    - Prescribed by: 45745874584
    - Posology:
        - Start date of the medication treatment: 12.11.2023
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: See leaflet.
        - Route of administration: Subcutaneous application (Subkutane Anwendung, code 20066000)
        - Method of administration: Injection, (Injektion, code 11)
        - Posology details:
            - 1 injection every 2 weeks on Monday at 08:00 p.m.
- AMOXICILLIN Sandoz Disp Tabl 1000 mg (Id 7680562030092, IdType: 2)
    - Reason for the medication treatment: Infection
    - Automedication/self-medication: No
    - Prescribed by: 45745874584
    - Posology 1:
        - Start date of the medication treatment: 02.02.2024
        - End date of the medication treatment: 02.02.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: Endocarditis prophylaxis. Take 1 pill 2 hours (09:00 a.m.) before the dentist's appointment (02.02.2024 11.00 a.m.).
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 pill at 09:00 a.m. on 02.02.2024
    - Posology 2:
        - Start date of the medication treatment: 06.03.2024
        - End date of the medication treatment: 11.03.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 pill at 08:00 a.m. and 1 pill at 08:00 p.m.
- VITAMIN B12 Amino Inj Lös 1000 mcg (Id 7680302190581, IdType: 2)
    - Reason for the medication treatment: -
    - Automedication/self-medication: No
    - Prescribed by: 45745874584
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: 1 application once a month, next appointment on 10.02.2024
        - Route of administration: Intramuscular application (Intramuskuläre Anwendung, code 20035000)
        - Method of administration: Injection (Injektion, code 11)
        - Posology details:
            - Once a month
- BILOL Filmtabl 5 mg (Id 7680540300100, IdType: 2)
    - Reason for the medication treatment: -
    - Automedication/self-medication: No
    - Prescribed by: 45745874584
    - Posology:
        - Start date of the medication treatment: 06.07.2022
        - End date of the medication treatment: Unknown
        - Reserve medication: Yes
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - If palpitations occur, take ½ pill and wait 30 minutes. If palpitations persist, take another ½ pill and wait another 30 minutes. If it does not get better, contact a doctor. (Free text)
- Imagikin (Id Imagikin, IdType: 1)
    - Reason for the medication treatment: -
    - Automedication/self-medication: Yes
    - Prescribed by: -
    - Posology 1:
        - Start date of the medication treatment: 10.11.2023
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Drops (Tropfen, code gtt)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Every 10th day of the month 10 – 20 drops
    - Posology 2:
        - Start date of the medication treatment: 02.02.2024
        - End date of the medication treatment: 18.02.2024
        - Reserve medication: Yes
        - Relative to meal: -
        - Unit: Drops (Tropfen, code gtt)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 2 drops every Monday at 06:00 p.m.

**Remark:** Please measure your blood pressure daily.

```json5
{
  "patient": {
    "fName": "Urs",
    "lName": "Mustermann",
    "bdt": "1968-05-15",
    "gender": 1,
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH",
    "lng": "DE",
    "phones": [
      "079 999 99 99"
    ],
    "ids": [
      {
        "type": 1,
        "val": "87588895877545621"
      }
    ],
    "mData": {
      "rcs": [
        {
          "id": 4
        },
        {
          "id": 5,
          "rIds": [
            615
          ]
        },
        {
          "id": 6
        },
        {
          "id": 7
        }
      ]
    }
  },
  "meds": [
    {
      "id": "7680549490031",
      "idType": 2,
      "pos": [
        {
          "po": {
            "t": 4,
            "cyDuU": 5,
            "cyDu": 1,
            "tdo": {
              "t": 4,
              "wds": [
                2,
                4
              ],
              "tdo": {
                "t": 2,
                "ts": [
                  {
                    "dt": "07:30:00",
                    "do": {
                      "t": 1,
                      "a": 1.0
                    }
                  }
                ]
              }
            },
            "tdpc": 1
          },
          "relMeal": 1,
          "unit": "Stk",
          "appInstr": "Take on an empty stomach, at least 30 minutes before breakfast."
        },
        {
          "po": {
            "t": 4,
            "cyDuU": 5,
            "cyDu": 1,
            "tdo": {
              "t": 4,
              "wds": [
                6
              ],
              "tdo": {
                "t": 2,
                "ts": [
                  {
                    "dt": "09:00:00",
                    "do": {
                      "t": 1,
                      "a": 1.0
                    }
                  }
                ]
              }
            },
            "tdpc": 1
          },
          "relMeal": 1,
          "unit": "Stk",
          "appInstr": "Take on an empty stomach, at least 30 minutes before breakfast."
        }
      ],
      "rsn": "Thyroid gland",
      "autoMed": false,
      "prscbBy": "45745874584",
      "nbPack": 1.0
    },
    {
      "id": "7680216930020",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-01-10",
          "dtTo": "2024-02-29",
          "po": {
            "t": 5,
            "sos": [
              {
                "t": 1,
                "po": {
                  "t": 1,
                  "ds": [
                    0,
                    0,
                    0.5,
                    0
                  ]
                },
                "duU": 4,
                "du": 2
              },
              {
                "t": 2,
                "duU": 4,
                "du": 1
              },
              {
                "t": 1,
                "po": {
                  "t": 1,
                  "ds": [
                    0,
                    0,
                    0.75,
                    0
                  ]
                },
                "duU": 4,
                "du": 3
              },
              {
                "t": 2,
                "duU": 4,
                "du": 1
              }
            ]
          },
          "relMeal": 1,
          "inRes": false,
          "unit": "Stk",
          "appInstr": "Next doctor's appointment on 29.02.2024"
        }
      ],
      "rsn": "Blood thinner",
      "autoMed": false,
      "prscbBy": "55255255255",
      "nbPack": 1.0
    },
    {
      "id": "7680656220026",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2023-11-12",
          "po": {
            "t": 4,
            "cyDuU": 5,
            "cyDu": 2,
            "tdo": {
              "t": 4,
              "wds": [
                1
              ],
              "tdo": {
                "t": 2,
                "ts": [
                  {
                    "dt": "20:00:00",
                    "do": {
                      "t": 1,
                      "a": 1.0
                    }
                  }
                ]
              }
            },
            "tdpc": 1
          },
          "inRes": false,
          "roa": "20066000",
          "moa": "11",
          "unit": "Stk",
          "appInstr": "See leaflet"
        }
      ],
      "autoMed": false,
      "prscbBy": "45745874584",
      "nbPack": 1.0
    },
    {
      "id": "7680562030092",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-02-02",
          "dtTo": "2024-02-02",
          "po": {
            "t": 3,
            "tdo": {
              "t": 2,
              "ts": [
                {
                  "dt": "09:00:00",
                  "do": {
                    "t": 1,
                    "a": 1.0
                  }
                }
              ]
            }
          },
          "inRes": false,
          "unit": "Stk",
          "appInstr": "Endocarditis prophylaxis. Take 1 pill 2 hours (09:00 a.m.) before the dentist's appointment (02.02.2024 11.00 a.m.)."
        },
        {
          "dtFrom": "2024-03-06",
          "dtTo": "2024-03-11",
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 2,
              "ts": [
                {
                  "dt": "08:00:00",
                  "do": {
                    "t": 1,
                    "a": 1.0
                  }
                },
                {
                  "dt": "20:00:00",
                  "do": {
                    "t": 1,
                    "a": 1.0
                  }
                }
              ]
            },
            "tdpc": 1
          },
          "inRes": false,
          "unit": "Stk"
        }
      ],
      "rsn": "Infection",
      "autoMed": false,
      "prscbBy": "45745874584",
      "nbPack": 1.0
    },
    {
      "id": "7680302190581",
      "idType": 2,
      "pos": [
        {
          "po": {
            "t": 4,
            "cyDuU": 6,
            "cyDu": 1,
            "tdo": {
              "t": 1,
              "do": {
                "t": 1,
                "a": 1.0
              }
            },
            "tdpc": 1
          },
          "inRes": false,
          "roa": "20035000",
          "moa": "11",
          "unit": "Stk",
          "appInstr": "1 application once a month, next appointment on 10.02.2024"
        }
      ],
      "autoMed": false,
      "prscbBy": "45745874584",
      "nbPack": 1.0
    },
    {
      "id": "7680540300100",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2022-07-06",
          "po": {
            "t": 2,
            "text": "If palpitations occur, take ½ pill and wait 30 minutes. If palpitations persist, take another ½ pill and wait another 30 minutes. If it does not get better, contact a doctor."
          },
          "inRes": true,
          "unit": "Stk"
        }
      ],
      "autoMed": false,
      "prscbBy": "45745874584",
      "nbPack": 1.0
    },
    {
      "id": "Imagikin",
      "idType": 1,
      "pos": [
        {
          "dtFrom": "2023-11-10",
          "po": {
            "t": 4,
            "cyDuU": 6,
            "cyDu": 1,
            "tdo": {
              "t": 5,
              "doms": [
                10
              ],
              "tdo": {
                "t": 1,
                "do": {
                  "t": 3,
                  "aMin": 10.0,
                  "aMax": 20.0
                }
              }
            },
            "tdpc": 1
          },
          "inRes": false,
          "unit": "gtt"
        },
        {
          "dtFrom": "2024-02-02",
          "dtTo": "2024-02-18",
          "po": {
            "t": 4,
            "cyDuU": 5,
            "cyDu": 1,
            "tdo": {
              "t": 4,
              "wds": [
                1
              ],
              "tdo": {
                "t": 2,
                "ts": [
                  {
                    "dt": "18:00:00",
                    "do": {
                      "t": 1,
                      "a": 2.0
                    }
                  }
                ]
              }
            },
            "tdpc": 1
          },
          "inRes": true,
          "unit": "gtt"
        }
      ],
      "autoMed": true,
      "nbPack": 1.0
    }
  ],
  "medType": 1,
  "auth": 1,
  "dt": "2024-01-10T15:54:06.0000000+01:00",
  "rmk": "Please measure your blood pressure daily.",
  "hcPerson": {
    "fName": "Hans",
    "lName": "Beispiel"
  },
  "hcOrg": {
    "gln": "7601001362383",
    "name": "Arztpraxis Sonnenschein",
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH"
  }
}
```

### Example 3

**Document:** Medication plan

**Created (date and time):** 05.02.2024 21:33:46

**Patient = author of the document:**

- First name: Dana
- Last name: Banana
- Date of birth: 17.07.1997
- Gender: Female
- Address: Bodenseeweg 3, 78462 Konstanz, Deutschland
- Language: German
- Patient Identifier: System 9.99.999.9.99, value: 57484
- Phone numbers: 079 999 99 99
- E-mail address: ana@email.de

**Medical Data:**

- Weight: 56 kg
- Height: 165 cm
- Risks:
    - Renal insufficiency (risk category 1): No
    - Liver insufficiency (risk category 2): No
    - Reproduction (risk category 3): Woman of childbearing age (Frauen im gebärfähigen Alter, risk id 612)
    - Competitive athlete (risk category 4): Competitive athlete (Leistungssportler, risk id 580)
    - Operating vehicles/machines (risk category 5): Exposure to potentially dangerous situations, such as driving vehicles, operating machinery, or working at great heights (Potenziell gefährlichen Situationen ausgesetzt, wie beispielsweise dem Führen von Fahrzeugen, dem Bedienen von Maschinen oder dem Arbeiten in grossen Höhen, risk id 615)
    - Allergies (risk category 6): No
    - Diabetes (risk category 7): No

**Medication:**

- ELTROXIN LF Tabl 0.1 mg (Id 7680298120012, IdType: 2)
    - Reason for the medication treatment: Thyroid gland
    - Automedication/self-medication: No
    - Prescribed by: Dr. Hans Vogel, Konstanz
    - Posology:
        - Start date of the medication treatment: Unknown
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: Before
        - Unit: Piece (Stück, code Stk)
        - Application instructions: 30 minutes before breakfast.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Alternating 1 pill one day and 2 pills the other day
- MEBUCAÏNE N Lutschtabl neue Formel (Id 7680662560024, IdType: 2)
    - Reason for the medication treatment: Sore thorat
    - Automedication/self-medication: No
    - Prescribed by: Apotheke Blumenwiese, Romanshorn
    - Posology:
        - Start date of the medication treatment: 18.01.2024
        - End date of the medication treatment: 20.01.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: Do not take the lozenge immediately before eating or drinking. Take maximum 6 per day.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 lozenge every 4 hours
- DAFALGAN Filmtabl 1 g (Id 7680563180079, IdType: 2)
    - Reason for the medication treatment: Fever/pain
    - Automedication/self-medication: No
    - Prescribed by: Apotheke Blumenwiese, Romanshorn
    - Posology:
        - Start date of the medication treatment: 18.01.2024
        - End date of the medication treatment: Unknown
        - Reserve medication: Yes
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: Up to 4 pills daily with at least 6 hours between each dose.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Up to 4 pills daily with at least 6 hours between each dose.
- LADONNA Filmtabl (Id 7680594920033, IdType: 2)
    - Reason for the medication treatment: Contraception
    - Automedication/self-medication: No
    - Prescribed by: Dr. Hannelore Beispielhaft, Romanshorn
    - Posology:
        - Start date of the medication treatment: 14.06.2016
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (Stück, code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 21 days one pill at 09:00 p.m. and then 7 days pause (0 pills) and then repeat all over again

```json5
{
  "patient": {
    "fName": "Dana",
    "lName": "Banana",
    "bdt": "1997-07-17",
    "gender": 2,
    "street": "Bodenseeweg 3",
    "zip": "78462",
    "city": "Konstanz",
    "country": "DE",
    "lng": "DE",
    "phones": [
      "079 999 99 99"
    ],
    "emails": [
      "ana@email.de"
    ],
    "ids": [
      {
        "type": 2,
        "sId": "9.99.999.9.99",
        "val": "57484"
      }
    ],
    "mData": {
      "rcs": [
        {
          "id": 1
        },
        {
          "id": 2
        },
        {
          "id": 3,
          "rIds": [
            612
          ]
        },
        {
          "id": 4,
          "rIds": [
            580
          ]
        },
        {
          "id": 5,
          "rIds": [
            615
          ]
        },
        {
          "id": 6
        },
        {
          "id": 7
        }
      ],
      "w": 56.0,
      "h": 165.0
    }
  },
  "meds": [
    {
      "id": "7680298120012",
      "idType": 2,
      "pos": [
        {
          "po": {
            "t": 5,
            "sos": [
              {
                "t": 1,
                "po": {
                  "t": 3,
                  "tdo": {
                    "t": 1,
                    "do": {
                      "t": 1,
                      "a": 1.0
                    }
                  }
                },
                "duU": 4,
                "du": 1
              },
              {
                "t": 1,
                "po": {
                  "t": 3,
                  "tdo": {
                    "t": 1,
                    "do": {
                      "t": 1,
                      "a": 2.0
                    }
                  }
                },
                "duU": 4,
                "du": 1
              }
            ]
          },
          "relMeal": 1,
          "inRes": false,
          "unit": "Stk",
          "appInstr": "30 minutes before breakfast."
        }
      ],
      "rsn": "Thyroid gland",
      "autoMed": false,
      "prscbBy": "Dr. Hans Vogel, Konstanz",
      "nbPack": 1.0
    },
    {
      "id": "7680662560024",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-01-18",
          "dtTo": "2024-01-20",
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 6,
              "do": {
                "t": 1,
                "a": 1.0
              },
              "miDuU": 3,
              "miDu": 4
            },
            "tdpc": 6
          },
          "inRes": false,
          "unit": "Stk",
          "appInstr": "Do not take the lozenge immediately before eating or drinking. Take maximum 6 per day."
        }
      ],
      "rsn": "Sore thorat",
      "autoMed": false,
      "prscbBy": "Apotheke Blumenwiese, Romanshorn",
      "nbPack": 1.0
    },
    {
      "id": "7680563180079",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-01-18",
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 6,
              "do": {
                "t": 1,
                "a": 1.0
              },
              "miDuU": 3,
              "miDu": 6
            },
            "tdpc": 4
          },
          "inRes": true,
          "unit": "Stk",
          "appInstr": "Up to 4 pills daily with at least 6 hours between each dose."
        }
      ],
      "rsn": "Fever/pain",
      "autoMed": false,
      "prscbBy": "Apotheke Blumenwiese, Romanshorn",
      "nbPack": 1.0
    },
    {
      "id": "7680594920033",
      "idType": 2,
      "pos": [
        {
          "po": {
            "t": 5,
            "sos": [
              {
                "t": 1,
                "po": {
                  "t": 4,
                  "cyDuU": 4,
                  "cyDu": 1,
                  "tdo": {
                    "t": 2,
                    "ts": [
                      {
                        "dt": "09:00:00",
                        "do": {
                          "t": 1,
                          "a": 1.0
                        }
                      }
                    ]
                  },
                  "tdpc": 1
                },
                "duU": 4,
                "du": 21
              },
              {
                "t": 2,
                "duU": 4,
                "du": 7
              }
            ]
          },
          "unit": "Stk"
        }
      ],
      "rsn": "Contraception",
      "autoMed": false,
      "prscbBy": "Dr. Hannelore Beispielhaft, Romanshorn",
      "nbPack": 1.0
    }
  ],
  "medType": 1,
  "auth": 2,
  "dt": "2024-02-05T21:33:46.0000000+01:00"
}
```

### Example 4

**Document:** Prescription

**Created (date and time):** 08.02.2024 10:01:23

**Patient:**

- First name: Sebastian
- Last name: Example
- Date of birth: 15.05.1972
- Gender: Male
- Address: Bernstrasse 1, 3000 Bern, Switzerland
- Patient Identifier: System 9.99.999.9.99, value: 87588895877545621
- Language: German
- Phone number: 079 999 99 99

**Medical Data:**

- Weight: 80
- Height: 179
- Risks:
    - Renal insufficiency (risk category 1): No
    - Liver insufficiency (risk category 2): No
    - Competitive athlete (risk category 4): No
    - Operating vehicles/machines (risk category 5): Exposure to potentially dangerous situations, such as driving vehicles, operating machinery, or working at great heights (Potenziell gefährlichen Situationen ausgesetzt, wie beispielsweise dem Führen von Fahrzeugen, dem Bedienen von Maschinen oder dem Arbeiten in grossen Höhen, risk id 615)
    - Allergies (risk category 6): No
    - Diabetes (risk category 7): No

**HealthcarePerson = author of the document:**

- GLN: 123123123123
- First name: Hans
- Last name: Muster

**Healthcare Organization:**

- GLN: 55255255255
- Name: Arztpraxis Sonnenschein
- Address: Bernstrasse 1, 3000 Bern, Switzerland
- ZSR: XX.1254

**Medication:**

- SPIRICORT Filmtabl 5 mg (Id 7680388400376, IdType: 2)
    - Reason for the medication treatment: -
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Repetition: 6 months
    - Should not be substituted: No
    - Number of packages: 1
    - Posology 1:
        - Start date of the medication treatment: 08.02.2024
        - End date of the medication treatment: 10.02.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - ¼ pill every morning and evening
    - Posology 2:
        - Start date of the medication treatment: 11.02.2024
        - End date of the medication treatment: 13.02.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - ½ pill every morning and evening
    - Posology 3:
        - Start date of the medication treatment: 14.02.2024
        - End date of the medication treatment: 16.02.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - ¾ pill every morning and evening
    - Posology 4:
        - Start date of the medication treatment: 17.02.2024
        - End date of the medication treatment: Unknown
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 pill every morning and evening
- DAFALGAN Filmtabl 1 g (Id 7680563180079, IdType: 2)
    - Reason for the medication treatment: Pain
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Repetition: not defined
    - Should not be substituted: Yes
    - Number of packages: 2
    - Posology:
        - Start date of the medication treatment: 08.02.2024
        - End date of the medication treatment: Unknown
        - Reserve medication: Yes
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: Maximum 4 pills a day.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - 1 – 2 pills every morning and evening
- MAKATUSSIN Tropfen (Id 7680552740055, IdType: 2)
    - Reason for the medication treatment: Cough
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Repetition: Not repeatable
    - Should not be substituted: No
    - Number of packages: 1
    - Posology:
        - Start date of the medication treatment: 08.02.2024
        - End date of the medication treatment: 28.02.2024
        - Reserve medication: No
        - Relative to meal: -
        - Unit: Drops (Tropfen, code gtt)
        - Application instructions: -
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - Daily 20 drops at 09:00 p.m.
- TEMESTA Tabl 1 mg (Id 7680362030131, IdType: 2)
    - Reason for the medication treatment: -
    - Automedication/self-medication: No
    - Prescribed by: 123123123123
    - Repetition: 2
    - Should not be substituted: No
    - Number of packages: 1
    - Posology:
        - Start date of the medication treatment: 08.02.2024
        - End date of the medication treatment: Unknown
        - Reserve medication: Yes
        - Relative to meal: -
        - Unit: Piece (code Stk)
        - Application instructions: Take half an hour before bedtime.
        - Route of administration: -
        - Method of administration: -
        - Posology details:
            - ½ pill at night

```json5
{
  "patient": {
    "fName": "Sebastian",
    "lName": "Example",
    "bdt": "1971-05-15",
    "gender": 1,
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH",
    "lng": "DE",
    "phones": [
      "079 999 99 99"
    ],
    "ids": [
      {
        "type": 2,
        "sId": "9.99.999.9.99",
        "val": "87588895877545621"
      }
    ],
    "mData": {
      "rcs": [
        {
          "id": 1
        },
        {
          "id": 2
        },
        {
          "id": 4
        },
        {
          "id": 5,
          "rIds": [
            615
          ]
        },
        {
          "id": 6
        },
        {
          "id": 7
        }
      ],
      "w": 80.0,
      "h": 179.0
    }
  },
  "meds": [
    {
      "id": "7680388400376",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-02-08",
          "dtTo": "2024-02-10",
          "po": {
            "t": 1,
            "ds": [
              0.25,
              0,
              0.25,
              0
            ]
          },
          "inRes": false,
          "unit": "Stk"
        },
        {
          "dtFrom": "2024-02-11",
          "dtTo": "2024-02-13",
          "po": {
            "t": 1,
            "ds": [
              0.5,
              0,
              0.5,
              0
            ]
          },
          "inRes": false,
          "unit": "Stk"
        },
        {
          "dtFrom": "2024-02-14",
          "dtTo": "2024-02-16",
          "po": {
            "t": 1,
            "ds": [
              0.75,
              0,
              0.75,
              0
            ]
          },
          "inRes": false,
          "unit": "Stk"
        },
        {
          "dtFrom": "2024-02-17",
          "po": {
            "t": 1,
            "ds": [
              1,
              0,
              1,
              0
            ]
          },
          "inRes": false,
          "unit": "Stk"
        }
      ],
      "autoMed": false,
      "prscbBy": "123123123123",
      "reps": {
        "t": 2,
        "u": 6,
        "d": 6
      },
      "isNotSub": false,
      "nbPack": 1.0
    },
    {
      "id": "7680563180079",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-02-08",
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 3,
              "ss": [
                {
                  "s": 1,
                  "do": {
                    "t": 3,
                    "aMin": 1.0,
                    "aMax": 2.0
                  }
                },
                {
                  "s": 3,
                  "do": {
                    "t": 3,
                    "aMin": 1.0,
                    "aMax": 2.0
                  }
                }
              ]
            },
            "tdpc": 1
          },
          "inRes": true,
          "unit": "Stk",
          "appInstr": "Maximum 4 pills a day"
        }
      ],
      "rsn": "Pain",
      "autoMed": false,
      "prscbBy": "123123123123",
      "isNotSub": true,
      "nbPack": 2.0
    },
    {
      "id": "7680552740055",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-02-08",
          "dtTo": "2024-02-28",
          "po": {
            "t": 4,
            "cyDuU": 4,
            "cyDu": 1,
            "tdo": {
              "t": 2,
              "ts": [
                {
                  "dt": "21:00:00",
                  "do": {
                    "t": 1,
                    "a": 20.0
                  }
                }
              ]
            },
            "tdpc": 1
          },
          "inRes": false,
          "unit": "gtt"
        }
      ],
      "rsn": "Cough",
      "prscbBy": "123123123123",
      "reps": {
        "t": 1,
        "v": 0
      },
      "isNotSub": false,
      "nbPack": 1.0
    },
    {
      "id": "7680362030131",
      "idType": 2,
      "pos": [
        {
          "dtFrom": "2024-02-08",
          "po": {
            "t": 1,
            "ds": [
              0,
              0,
              0,
              0.5
            ]
          },
          "inRes": true,
          "unit": "Stk",
          "appInstr": "Take half an hour before bedtime."
        }
      ],
      "autoMed": false,
      "prscbBy": "123123123123",
      "reps": {
        "t": 1,
        "v": 2
      },
      "isNotSub": false,
      "nbPack": 1.0
    }
  ],
  "medType": 3,
  "auth": 1,
  "dt": "2024-02-08T10:01:23.0000000+01:00",
  "hcPerson": {
    "gln": "123123123123",
    "fName": "Hans",
    "lName": "Muster"
  },
  "hcOrg": {
    "gln": "55255255255",
    "name": "Arztpraxis Sonnenschein",
    "street": "Bernstrasse 1",
    "zip": "3000",
    "city": "Bern",
    "country": "CH",
    "zsr": "XX.1254"
  }
}
```
