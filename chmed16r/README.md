# eMedication Plan CHMED16R

**Contact**

Geschäftsstelle IG eMediplan<br>
c/o Köhler, Stüdeli & Partner GmbH<br>
Amthausgasse 18, 3011 Bern<br>
Tel. +41 (0)31 560 00 24<br>
info@emediplan.ch

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Introduction](#introduction)
- [CHMED16R eMedication object](#chmed16r-emedication-object)
    - [Overview of the object model](#chmed16r-emedication-object)
    - [Using JSON as the object model format](#using-json-as-the-object-model-format)
    - [Object model](#object-model)
    - [Encapsulation of the JSON medication object](#encapsulation-of-the-json-medication-object)

## Introduction

Medication plans are a central pillar of any eHealth solution. To enable interoperability between eHealth systems in Switzerland, the organisation "[IG eMediplan](https://emediplan.ch/)" was founded in 2016. Its aim is to support and provide public, open source, medication plan formats used by a broad group of stakeholders from the public and private sectors. 

This paper describes the currently proposed specification and reference implementation of the object model for an electronic prescription, the so-called CHMED16R.

> [!IMPORTANT]
> CHMED16R is based on CHMED16A and only adds the new entities HealthcarePerson and HealthcareOrganization.
> This is required for the prescription document (Rx) to conform with the law.
> Therefore, CHMED16R is only relevant when working with Rx documents
> (therefore also the version "R" for Rx).
> Due to this, the parts of the CHMED16A specification
> which are not impacting the Rx document directly (e.g. the paper-based layout)
> have been removed.

This document is the content and layout specification for the electronic document, a string/text file containing a header such as "CHMED16R1" and the (compressed, encoded) medication plan as a JSON object in UTF-8 (see [ChTransmissionFormat](../chtransmissionformat/README.md)).

This allows IT systems to store and transmit electronic medication plans as simple strings or text files in UTF-8. It also makes it possible to transmit the medication in a print-based form by using 2D barcodes. Therefore, the medication plan is readable by users and systems alike. This is necessary to guarantee a simple handling. 

## CHMED16R eMedication object

### Overview of the object model

The hierarchy of the object model is quite simple. Each medication object includes:

```
1 Medication (the current medication plan)
  1 Patient
    n Identifiers (a number of identifiers/numbers/codes to connect the patient to IT systems)
  n Medicaments (all currently used medicaments)
    n Posology (the dosage information)
  1 HealthcarePerson
  1 HealthcareOrganization
```

### Using JSON as the object model format


The object model format is [JSON](https://en.wikipedia.org/wiki/JSON), which was chosen for its openness, flexibility, simplicity and language independence. JSON can be implemented for most programming languages and platforms. The JSON format uses human-readable text, so patient data is clearly identifiable, making the JSON format easy to use for documentation, development and integration. 

*As the number of primary types supported by JSON is very small, we make intensive use of strings in combination with a specific format, e.g. dates or posology. During serialization, the cost of tags is quite high; therefore we have chosen rather short field names. We recommend to omit optional fields when they are null or empty.* 

### Object model

```mermaid
classDiagram
    class Patient {
        -FName[1]: string
        -LName[1]: string
        -BDt[1]: string
        -Gender[0..1]: int
        -Street[0..1]: string
        -Zip[0..1]: string
        -City[0..1]: string
        -Phone[0..1]: string
        -Email[0..1]: string
        -Rcv[0..1]: string
        -Ids[0..*]: PatientId
        -PFields[0..*]: PrivateField
    }

    class PatientId {
        -Type[1]: int
        -Val[1]: string
    }

    class PrivateField {
        -Nm: string
        -Val: string
        -PFields[0..*]: PrivateField
    }

    class Medication {
        -Patient[1]: Patient
        -Medicaments[0..*]: Medicaments
        -PFields[0..*]: PrivateField
        -PSchema[0..1]: string
        -MedType[1]: int
        -Id[1]: string
        -Auth[1]: string
        -Zsr[0..1]: string
        -Dt[1]: string
        -Rmk[0..1]: string
        -HcPerson[1]: HealthcarePerson
        -HcOrg[1]: HealthcareOrganization
    }

    class Medicament {
        -Id[1]: string
        -IdType[1]: int
        -Pos[0..1]: Posology
        -Unit[0..1]: string
        -AppInstr[0..1]: string
        -Rep[0..1]: int
        -Subs[0..1]: int
        -NbPack[0..1]: double
        -PFields[0..*]: PrivateField
    }

    class Posology {
        -DtTo[0..1]: string
        -D[0..*]: double
    }

    class HealthcarePerson {
        -Gln[1]: string
        -FName[1]: string
        -LName[1]: string
        -Zsr[0..1]: string
    }

    class HealthcareOrganization {
        -Name[1]: string
        -NameAffix[0..1]: string
        -Street[1]: string
        -Zip[1]: string
        -City[1]: string
        -Country[0..1]: string
        -Zsr[0..1]: string
        -Phone[0..1]: string
        -Email[0..1]: string
    }

    Patient "1" -- "0..*" PatientId
    Patient "1" -- "0..*" PrivateField
    Medication "1" -- "1" Patient
    Medication "1" -- "1" HealthcarePerson
    Medication "1" -- "1" HealthcareOrganization
    Medication "1" -- "0..*" Medicament
    Medication "1" -- "0..*" PrivateField
    Medicament "1" -- "0..*" Posology
    Medicament "1" -- "0..*" PrivateField
```

#### Medication (Med)

The *Med* object is the main one; it contains exactly one *Patient* object and at least one *Medicament* object.  

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Patient</td>
  <td><i>Patient</i></td>
  <td>

  R[^1]

  </td>
  <td>

  The patient. Please refer to [Patient](#patient)

  </td>
</tr>
<tr>
  <td>Medicaments</td>
  <td>

  list of [Medicament](#medicament)

  </td>
  <td>0-N</td>
  <td>

  The list of medicaments. Please refer to [Medicament](#medicament)

  </td>
</tr>
<tr>
  <td>PFields</td>
  <td>

  list of [Private Field](#private-field)

  </td>
  <td>0-N</td>
  <td>

  The list of private fields. Please refer to [Private Field](#private-field)

  </td>
</tr>
<tr>
  <td>PSchema</td>
  <td>string</td>
  <td>

  O[^2]

  </td>
  <td>The schema of the private fields. When empty or not specified, all private fields must be ignored.</td>
</tr>
<tr>
  <td>MedType</td>
  <td>number</td>
  <td>R</td>
  <td>
    <p>The type of the <i>Medication</i> object Possible values: </p>
    <p>3: Prescription (Rx) </p>
  </td>
</tr>
<tr>
  <td>Id</td>
  <td>string</td>
  <td>R</td>
  <td>The ID of the <i>Medication</i> object</td>
</tr>
<tr>
  <td>Auth</td>
  <td>string</td>
  <td>R</td>
  <td>

  Author (GLN[^3] if available, otherwise name)

  eMediplan: GLN of a person or organisation

  ePrescription: GLN of a person

  The patient can also be the author of the eMediplan. In this case, the minimum requirement is that the term
  "patient" is used to designate the author. Optionally, the patient's first name, last name and date of birth can also be specified additionally.

  Note that the GLN provided in this field should also be provided
  in [HealthcarePerson](#healthcareperson) or [HealthcareOrganization](#healthcareorganization) respectively.
  This is a duplication of data which allows systems only supporting CHMED16A to support CHMED16R at least to some degree.

  </td>
</tr>
<tr>
  <td>Zsr</td>
  <td>string</td>
  <td>O</td>
  <td>

  ZSR ([Zahlstellenregister](https://www.sasis.ch/de/Angebot/Produkt/ProductDetail?topMenuId=447)) number of the author or their organisation

  </td>
</tr>
<tr>
  <td>Dt</td>
  <td>string</td>
  <td>R</td>
  <td>

  Date of creation, Format: yyyy-mm-ddThh:mm:ss+02:00 ([ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) Combined date and time in UTC) (e.g. 2016-01-16T16:26:15+02:00)

  </td>
</tr>
<tr>
  <td>Rmk</td>
  <td>string</td>
  <td>O</td>
  <td>
    Remark (any information/advice the author would like to share independently of a specific medicament)
  </td>
</tr>
<tr>
  <td>HcPerson</td>
  <td><i>HealthcarePerson</i></td>
  <td>R</td>
  <td>

  The healthcare person (the author of the document)

  Please refer to [HealthcarePerson](#healthcareperson).

  </td>
</tr>
<tr>
  <td>HcOrg</td>
  <td><i>HealthcareOrganization</i></td>
  <td>R</td>
  <td>

  The healthcare organization in which the <i>HealthcarePerson</i> works.

  Please refer to [HealthcareOrganization](#healthcareorganization).

  </td>
</tr>
</table>

#### Patient

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>FName</td>
  <td>string</td>
  <td>R</td>
  <td>First name</td>
</tr>
<tr>
  <td>LName</td>
  <td>string</td>
  <td>R</td>
  <td>Last name</td>
</tr>
<tr>
  <td>BDt</td>
  <td>string</td>
  <td>R</td>
  <td>

  Date of birth, Format: yyyy-mm-dd ([ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) Date)

  </td>
</tr>
<tr>
  <td>Gender</td>
  <td>number</td>
  <td>O</td>
  <td>

  Gender of patient

  Possible values

  1: Male

  2: Female

  </td>
</tr>
<tr>
  <td>Street</td>
  <td>string</td>
  <td>O</td>
  <td>Street</td>
</tr>
<tr>
  <td>Zip</td>
  <td>string</td>
  <td>O</td>
  <td>Zip code</td>
</tr>
<tr>
  <td>City</td>
  <td>string</td>
  <td>O</td>
  <td>City</td>
</tr>
<tr>
  <td>Phone</td>
  <td>string</td>
  <td>O</td>
  <td>Phone number</td>
</tr>
<tr>
  <td>Email</td>
  <td>string</td>
  <td>O</td>
  <td>E-mail address</td>
</tr>
<tr>
  <td>Rcv</td>
  <td>string</td>
  <td>O</td>
  <td>Receiver (GLN) of the electronic prescription. To be used if the electronic prescription is to be transmitted
    electronically to a healthcare professional.
  </td>
</tr>
<tr>
  <td>Ids</td>
  <td>
 
  list of [PatientId](#patientid)
 
  </td>
  <td>0-N</td>
  <td>
 
  The list of patient identifiers. Please refer to [PatientId](#patientid)
 
  </td>
</tr>
<tr>
  <td>PFields</td>
  <td>
 
  list of [Private Fields](#private-field)
 
  </td>
  <td>0-N</td>
  <td>
 
  The list of private fields. Please refer to [Private Fields](#private-field)
 
  </td>
</tr>
</table>

#### PatientId


<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Type</td>
  <td>number</td>
  <td>R</td>
  <td>

  Type of the ID

  Possible values

  1: Insurance card number

  </td>
</tr>
<tr>
  <td>Val</td>
  <td>string</td>
  <td>R</td>
  <td>The ID value</td>
</tr>
</table>

#### Medicament

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Id</td>
  <td>string</td>
  <td>R</td>
  <td>

  The ID defined in the _IdType_ below. If `IdType = None` then free text description.

  </td>
</tr>
<tr>
  <td>IdType</td>
  <td>number</td>
  <td>R</td>
  <td><p>The type of the <i>Id</i>. Possible values:</p><p>1: None </p>
    <p>2: GTIN </p>
    <p>3: Pharmacode </p>
    <p>4: Product number (not for Rx) </p></td>
</tr>
<tr>
  <td>Pos</td>
  <td>

  list of [Posology](#posology)

  </td>
  <td>0-1</td>
  <td>

  The list of posologies. Please refer to [Posology](#posology)

  </td>
</tr>
<tr>
  <td>Unit</td>
  <td>string</td>
  <td>O</td>
  <td>

  The quantity unit. Mandatory if _Pos_ is defined.
  (The unit must be based on the standardized substance in the INDEX database.)

  Possible values: [CDTYP 9](https://index.hcisolutions.ch/index/current/get.aspx?schema=CODE&keytype=CDTYP&key=9) in INDEX database/CODE schema

  </td>
</tr>
<tr>
  <td>AppInstr</td>
  <td>string</td>
  <td>O</td>
  <td>Application instructions (further information on how to apply the medication, e.g. take before meals)</td>
</tr>
<tr>
  <td>Rep</td>
  <td>number</td>
  <td>O</td>
  <td>Integer which defines the number of repetitions in months, e.g. permanent prescription for 6 months</td>
</tr>
<tr>
  <td>Subs</td>
  <td>number</td>
  <td>O</td>
  <td>1 if medicament should not be substituted, 0 otherwise. Default: 0</td>
</tr>
<tr>
  <td>NbPack</td>
  <td>number</td>
  <td>O</td>
  <td>Number of packages to be delivered. Default: 1</td>
</tr>
<tr>
  <td>PFields</td>
  <td>

  list of [Private Fields](#private-field)

  </td>
  <td>0-N</td>
  <td>

  The list of private fields. Please refer to [Private Fields](#private-field)

  </td>
</tr>
</table>

#### Posology

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>DtTo</td>
  <td>string</td>
  <td>O</td>
  <td>

  To date (end date of medication treatment),
  Format: yyyy-mm-dd ([ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) Date).
  The _DtTo_ must be considered as inclusive. For example DtTo: 2015-05-01, the patient must apply the medicament also on 2015-05-01.

  </td>
</tr>
<tr>
  <td>D</td>
  <td>list of number</td>
  <td>0-4</td>
  <td>

  Simplified version of taking times.
  Describes the doses to be applied in the morning, at noon, in the evening and at night.

  </td>
</tr>
</table>

#### Private Field

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Nm</td>
  <td>string</td>
  <td>R</td>
  <td>The name of the field</td>
</tr>
<tr>
  <td>Val</td>
  <td>string</td>
  <td>O</td>
  <td>The value of the field</td>
</tr>
<tr>
  <td>PFields</td>
  <td>

  list of [Private Fields](#private-field)

  </td>
  <td>0-N</td>
  <td>The list of private fields</td>
</tr>
</table>

#### HealthcarePerson

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Gln</td>
  <td>string</td>
  <td>R</td>
  <td>The GLN</td>
</tr>
<tr>
  <td>FName</td>
  <td>string</td>
  <td>R</td>
  <td>First name</td>
</tr>
<tr>
  <td>LName</td>
  <td>string</td>
  <td>R</td>
  <td>Last name</td>
</tr>
<tr>
  <td>Zsr</td>
  <td>string</td>
  <td>O</td>
  <td><p>ZSR number</p>
    <p>The ZSR number may only be set once, either in object <i>HealthcarePerson</i> or in object <i>HealthcareOrganization</i>.
    </p></td>
</tr>
</table>

#### HealthcareOrganization

<table>
<tr>
  <th rowspan="2"><b>Name</b></th>
  <th rowspan="2"><b>Type</b></th>
  <th><b>Usage</b></th>
  <th rowspan="2"><b>Description</b></th>
</tr>
<tr>
  <td>Rx</td>
</tr>
<tr>
  <td>Name</td>
  <td>string</td>
  <td>R</td>
  <td>Name</td>
</tr>
<tr>
  <td>NameAffix</td>
  <td>string</td>
  <td>O</td>
  <td>Additional name, e.g. department within the organization</td>
</tr>
<tr>
  <td>Street</td>
  <td>string</td>
  <td>R</td>
  <td>Street</td>
</tr>
<tr>
  <td>Zip</td>
  <td>string</td>
  <td>R</td>
  <td>Postcode</td>
</tr>
<tr>
  <td>City</td>
  <td>string</td>
  <td>R</td>
  <td>City</td>
</tr>
<tr>
  <td>Country</td>
  <td>string</td>
  <td>O</td>
  <td>

  Country

  If the address is in Switzerland, this property does not need to be set, as it is assumed by default that the address is in Switzerland.

  Format: Alpha-2 code ([ISO 3166](https://www.iso.org/iso-3166-country-codes.html) Country Codes)

  (e.g. FR for France)

  </td>
</tr>
<tr>
  <td>Zsr</td>
  <td>string</td>
  <td>O</td>
  <td><p>ZSR number</p>
    <p>The ZSR number may only be set once, either in object <i>HealthcarePerson</i> or in object <i>HealthcareOrganization</i>.
    </p></td>
</tr>
<tr>
  <td>Phone</td>
  <td>string</td>
  <td>O</td>
  <td>Phone number</td>
</tr>
<tr>
  <td>Email</td>
  <td>string</td>
  <td>O</td>
  <td>E-mail address</td>
</tr>
</table>

### Encapsulation of the JSON medication object

To encapsulate and transmit the JSON object, [ChTransmissionFormat](../chtransmissionformat/README.md) should be used.
Note that CHMED16R (the JSON described in this specification) does not contain a version marker
which is only included in ChTransmissionFormat
as one of the main goals is to keep the content as compact as possible.
Therefore, if CHMED16R is transmitted without being wrapped in ChTransmissionFormat,
the version needs to be transferred in another way.

[^1]: R: Required
[^2]: O: Optional
[^3]: Global Location Number (GLN): [https://www.refdata.ch/de/partner/abfrage](https://www.refdata.ch/de/partner/abfrage)
