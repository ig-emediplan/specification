# Specification repository of IG eMediplan

For details regarding the organization, please refer to the website: https://emediplan.ch/

The aim of this repository is to be the home of specifications created
and maintained by the IG eMediplan. 
Some specifications have been published as PDF as well on the [website](https://emediplan.ch/downloads/).
These versions are superseded by the specifications in this repository.
For historical purposes, they are included in the [legacy](./legacy) folder.

## Versions

There are multiple versions of the specification as it is continually being improved.
Additionally, older versions may still be in use across the industry
even if a newer version is proposed to replace it.

The following table gives an overview over the different versions
and their uses.

It uses the following states a specification can be in:

- Proposal: Major changes are to be expected as the specification is actively being worked on.
  In this phase, feedback is most effective as it can still be changed easily
  without impacting system implementers.
  Adoption by system implementers is not advised except for providing early feedback.
- Ready for adoption: This version has been approved by the IG eMediplan board
  and is ready for adoption by system implementers.
  Some minor changes are still to be expected
  as the version has not had the chance to gather a lot of feedback in the field.
- Stable: This version has seen enough adoption in the industry
  and has not had big changes in a while to be considered stable.
  Changes are not expected and should always be backwards compatible.

| Version | State | Use |
| --- | --- | --- |
| [CHMED16A](./chmed16a) | Stable | System implementers should aim to be able to read this version as it is widely used. For generating new medication plans, CHMED23A should be used instead. |
| [CHMED16R](./chmed16r) | Ready for adoption | This is a newer version of CHMED16A which is only for prescription documents (Rx) to make sure these conform with the law. |
| [CHMED20AF](https://chmed20af.emediplan.ch/fhir/) | Stable | This a FHIR version which defines the mapping from CHMED16A to FHIR. It is a superset of [CH EMED](https://fhir.ch/ig/ch-emed/index.html) which enables interoperability with the electronic patient record. |
| [CHMED23A](./chmed23a) | Ready for adoption | System implementers should use this version for both reading and creating medication plans JSON documents (the QR code). Note that certain parts of the specification are not available yet (e.g. physical layout). |

## Backwards compatibility

The aim is to create newer versions which are backwards compatible
as not all system implementers will have the possibility
to adjust their systems quickly.
This may not always be possible/desirable though
but it is always an important point to consider.

To reduce the possiblity of systems breaking with newer versions,
implementers SHOULD:

- ignore additional, unknown fields present in a JSON object:
  This allows the addition of new (optional) fields to a version without breaking implementations.

## Client implementations

The following table lists the known client implementations.
Note that IG eMediplan does not maintain them unless it is explictly stated.
It is not guaranteed that client implementations are compliant with the standard
(the specification always superseeds any implementation).
It is the responsibility of the maintainer
to ensure the compliance with the specification.
If you notice any misbehavior in client implementations,
feel free to open a PR to fix it.

| Version | Environment / Language | Author | Maintainer | Notes |
| --- | --- | --- | --- | --- | 
| CHMED16A | .NET / C# | HCI Solutions AG | - | Only the DLL has been published, see https://emediplan.ch/downloads/ |
| CHMED23A | .NET / C# | HCI Solutions AG | - | [Source Code](./chmed23a/client-implementations/dotnet) |

If you have implemented a client library in a certain language
which you would be willing to share with others,
please open a PR in this repository,
adding it as a subdirectory to the corresponding version.

Client implementations should help at least in one of the following areas:

- Provide types to reduce accidental mistakes
  (e.g. ensuring the data type and cardinality of a certain field is correct)
- Provide enums/types and corresponding converters to make implementation easier
  by allowing to usage of non-abbreviated names
  (since many property names are shortened
  to reduce the output size and enums are represented as integers).
- Provide serialization/deserialization functionality
