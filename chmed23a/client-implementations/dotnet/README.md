# Emediplan.ChMed23A

The solution includes the ChMed23A model (with validation and JSON serializer), unit tests and examples.

## ChMed23A
Contains everything required to convert between the ChMed23A model and JSON, validate the model and serialize it.

This is a `netstandard2.0` library, which can be used in any .NET application.

### Models
The root model is [Medication](./Source/Emediplan.ChMed23A/Models/Medication.cs).
All models contained in `XyzObjects` directory are 'dynamic' objects, which are being identified by their type, defined in [ObjectTypes](./Source/Emediplan.ChMed23A/Models/Enums/ObjectTypes/) enumerations.

### Exceptions
The [Exceptions](./Source/Emediplan.ChMed23A/Exceptions/) directory contains custom exceptions, used for validation and serialization.

### Validation
Model validations are implemented in the models themselves, implementing the [IValidatable](./Source/Emediplan.ChMed23A/Validation/IValidatable.cs) interface.
The [ValidationContext](./Source/Emediplan.ChMed23A/Validation/ValidationContext.cs) currently only contains the medication type (medication plan or prescription) allowing to differentiate validation between the two.

### Serialization
[Newtonsoft.Json](https://www.newtonsoft.com/json) is used for serialization and deserialization of the model.
[CustomCreationConverters](./Source/Emediplan.ChMed23A/Serialization/CustomCreationConverters) are configured to de-serialize the dynamic objects in the JSON to their respective c# models.

## Emediplan.ChMed23A.Tests.Unit
Contains a set of unit tests for validation and (de-)serialization of the ChMed23A model.

## Emediplan.ChMed23A.Examples
Contains a set of examples, demonstrating the usage of the ChMed23A model. The examples are implemented as tests.

## Build/test/release
[Cake](https://cakebuild.net/) is used for building, testing and releasing the solution. Find the available targets in the [build.cake](./Source/build.cake) file.
To execute a target, run a powershell command `.\build.ps1 --target=<target>` in the `Source` directory.

Examples:
To build the solution: `.\build.ps1 --target=build`
To run all tests: `.\build.ps1 --target=test`