# eMedication Plan CHMED23A Conventions

**Contact**

Geschäftsstelle IG eMediplan<br>
c/o Köhler, Stüdeli & Partner GmbH<br>
Amthausgasse 18, 3011 Bern<br>
Tel. +41 (0)31 560 00 24<br>
info@emediplan.ch

## Introduction

This document explains certain conventions which are used across multiple documents.

## Conventions

### Objects

In the context of this and the referncing document, properties named 'Object' can hold different types of data. Every object contains a type as well as properties defined by the type itself.

E.g. for dosage objects, a simple dosage only contains an amount:

```json5
{
  "t": 1, // Simple dosage
  "a": 1 // Amount of 1
}
```

Whereas a dosage range specifies a minimum and a maximum amount:

```json5
{
  "t": 3, // Dosage range
  "aMin": 1.0, // Minimum amount of 1
  "aMax": 3.0 // Maximum amount of 3
}
```

Use the appropriate object type to represent the desired posology.

Objects must be deserialised according to the specified type.

### Naming

To minimise the size of the JSON files being generated, property names have been abbreviated using the following rules:

- Property names always start with a lowercase character.
- Properties holding an array of elements have the suffix 's', which represents the plural.
- Properties holding variable object types contain an 'o'. E.g. *PosologyDetail* object -> po, *Dosage* object -> do
- If the abbreviation of a word consists of a single character, keep it lowercase; use CamelCase otherwise. E.g. MeasurementType -> mt, ApplicationInstructions -> appInstr

### Value types

The following types are used for the properties in the model.

|**Property type**|**Format**|**Examples**|**Description**|
| :- | :- | :- | :- |
|boolean|true / false|<p>true</p><p>false</p>|The value is either true or false or can be null if not required.|
|integer|whole number|<p>1</p><p>700</p>|A number without a decimal separator. In case it contains a decimal separator, the number will be rounded to the closest whole number.|
|decimal|decimal number|<p>1\.5</p><p>7</p><p>30\.005</p>|A number which is either a whole number or a number containing a decimal, the separator is a dot.|
|string|text|"any text"|A text contained in quotes.|
|list of …|a list of items|<p>[1, 7]</p><p>["item1"]</p>|An array containing elements of the specified type.|
|object|complex object|{ }|Can contain any type of complex object. Supported type(s) will be described.|

### Usage

The usage specifies if a property must be provided. The following values can be set.

|**Usage**|**Description**|
| :- | :- |
|R|The value is required and must be set.|
|R if …|The value must be provided if the specified condition is met (usually if another property has a certain value).|
|O|The value is optional. It will be used by certain use cases if it has been set.|
|-|The value can be set, but won't be used.|
|x-N|A list of values can be provided; the minimum amount that must be included is specified by x.|
