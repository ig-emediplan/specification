# Specification ChTransmissionFormat

**Contact**

Geschäftsstelle IG eMediplan<br>
c/o Köhler, Stüdeli & Partner GmbH<br>
Amthausgasse 18, 3011 Bern<br>
Tel. +41 (0)31 560 00 24<br>
info@emediplan.ch

## Introduction

Medication plans often need to be printed on paper.
To easily bring these back into the digital world,
the CHMED standard (in all its versions) requires a specification
of how the whole information can be represented in a QR code
as a simple line of text.

The ChTransmissionFormat is this corresponding format
which defines the contents of the QR code on a printed medication plan
(which can be represented either physically on paper or digitally e.g. as a PDF).
This representation can be used anywhere
but is optimized for the usage in the QR code.

### Goals

- Define parsing rules based on prefix of the data
  (only looking at the start of the data needs to be sufficient
  to determine how the rest needs to be parsed),
  specifically the version part
  which allows to implement support for multiple versions.
- Define measures to reduce the size of data since the amount of data
  which can be included in a single QR code is limited.

## Format

A ChTransmissionFormat document must use UTF-8 encoding.

```
CHMED{ReleaseYear}{SubVersion}{AdditionalMetaData}{Data}
```

Representation as regex:

```regex
^CHMED([0-9]{2})([A-Z]+)(.*)$
```

Description and constraints:

- `CHMED` (Prefix): This is the fixed string `CHMED`.
  It is always the same value and should be used to determine if the given input string
  should be tried to parse as ChTransmissionFormat.
  During parsing, it should be treated as case-insensitive (e.g. `ChMed` should also be accepted)
  but during serialization all uppercase should be used.
- `ReleaseYear`: two digits (e.g. `16`)
- `SubVersion`: At least a single uppercase alphabetic character (e.g. `A`).
  Depending on the `ReleaseYear`, more than one character can be allowed.
  During parsing, it should be treated as case-insensitive (e.g. `a` should also be accepted)
  but during serialization all uppercase should be used.
- `AdditionalMetaData`: Based on the `ReleaseYear`, additional meta data may be required.
- `Data`: The actual data (JSON) which (after applying transformations based on the `ReleaseYear`)
  needs to conform with the corresponding specification defining the format of the JSON.

Since part of the format is dependent on the `ReleaseYear`,
each available `ReleaseYear` is covered independently.
Only years listed here are considered valid for use with ChTransmissionFormat.
All constraints listed above also apply to the specific parts defined for a `ReleaseYear`.

Note that the JSON format itself does not contain a version marker
as it would be duplicated data when transmitted using ChTransmissionFormat
and one of the goals is to reduce the data size.
Therefore, CHMED documents should always be transmitted using ChTransmissionFormat.

### ReleaseYear 16

```
CHMED16{SubVersion}{Compression}{Data}
```

Representation as regex:

```regex
^CHMED16([A-Z])([0-1])(.*)$
```

Description and constraints:

- `SubVersion`: A single uppercase alphabetic character.
- `Compression`: This is the only part of `AdditionalMetaData`.
  It is a single digit which either is `0` or `1`
  and determines if compression is used for the `Data`.
- `Data`:
  - if `Compression = 0`: Plain JSON document.
    Note that it is highly recommended to always compress the contents.
    Later versions (e.g. `ReleaseYear` 23) do no longer support documents without compression.
  - if `Compression = 1`: Base64 encoded and gzipped JSON document

**Example**

```
CHMED16A1H4sIAAAAAAAEAMVU3W7TMBR+lcq3S4SPHdtx7raVAaKFqutAAnoREreJ2jpT4gKj6ptxx4txnCwVSKQSu0GVqvPX7+fIpwdyuXcFSYiSFCjloGKtNQnI2GGRUZAh1SGwBUASyYTqC8oSSnHgVe4HZM7z1UqF6WcqwkhxjCKdhcJkWc650GIlcXZq8sXDvSEJtHGZpTtjXUOSj4cORyuIpWpRu0EekFnVDYzxmwbtZ+l13dTV7g9t5Ij1eZVicfYWMRab9byxmF1urMH8zpbezO3iNTkGj4RRJIHH7Bwj/J2RhUAHGa+2e/fF1PnPH9bu7XqAnHEhZHzGrqeGJ5EXVVbk9T7bfHr20tTfBwRAxCQHen7f/y7guqi2pnGmLm1j7MbUA/Qx1VLz8/aftvvf7L9Pm2ZQAQgBTEX/QwL+dJa6Et8/SQ7kqr0z0DGEFPxbDsh16R48lqktZjdv8FYwnabfyl2KhRfG5giasIBMTj3nWp4JPrmEjJ93J+fxx5PGTY3X1tbSzuHpGDthCPUu3eKM4O2Kujbr29C3QcSd9ex0udibYyKUWvbLZX3Au54EdupFfSD6QD4CCBEIBcsjjpJZUVnv6yJCxlEsYMRk+59z62pj/MLuLBreoe2vZj2KsfOhvMcyp0yhAzLfbbzj4y8tzloh3gQAAA==
```

### ReleaseYear 23

```
CHMED23{SubVersion}{ChunkMetaData}.{Data}
```

Representation as regex:

```regex
^CHMED23([A-Z]+)(\.[1-9][0-9]*/[1-9][0-9]*)?\.(.*)$
```

Description and constraints:

- `SubVersion`: One or more uppercase alphabetic characters which are delimited by a dot (`.`).
- `ChunkMetaData`: Optional, see below for further information.
- `Data`:
  - if `ChunkMetaData` is present: Chunk of base64 encoded and gzipped JSON document.
    If all chunks are concatenated in the correct order, it must result in a valid JSON document
    (after base64 decoding and gzip decompressing).
    Note that a single chunk is not necessarily proper base64 encoded nor properly gzip compressed.
    This property only holds for the dechunked data.
  - if `ChunkMetaData` is **not** present: Base64 encoded and gzipped JSON document

**Example**

```
CHMED23A.H4sIAAAAAAAACq2OOw4CMQxE7zIt2ZUTAmzcLZsGiU+KUCEKYKlokIACRbk7jkLBAWisZz/NyAmb6/gAHxJWI7hsGgqhnsIOnBDBRmF4+9cebCuBtUL0Xy38g73MnIu+DxX/1nRUkCRiv1zLl9tzOF1uIloqxj9FGTKmId1oHcnxtGM7a+28c9YtJqSZCPkD+iD8fPQAAAA=
```

#### ChunkMetaData

```
{Index}/{Total}
```

Optionally, the contained `Data` can be chunked and delivered in separate chunks.
This allows to transmit bigger documents in environments
where the size of a single document is limited,
e.g. multiple QR codes could be used if a single one is not able to represent all the data.
Note that the format does not include an ID or something similar
which allows a system receiving a stream of ChTransmissionFormat (23) documents
to correlate chunks.
E.g. if four documents are received,
two with index 1 and total 2 and two with index 2 and total 2,
there is no way to know which two are a pair
or even if any of these are related at all.
The system used for transfer is responsible
to provide a mechanism to correlate the entries itself.

Description and constraints:

- `Index`: Positive integer which is the 1-based index of the chunks.
  The `Index` must be lower or equal to the `Total`
  and must be unique within the list of ChTransmissionFormat (23) documents
  which represent the full document.
- `Total`: Positive integer which defines the total number of chunks the full document has been split into.
  Must be greater than 1 as `ChunkMetaData` must not be used if no actual chunking is applied.

To recreate the full document,
`Total` number of ChTransmissionFormat (23) documents need to be received
and ordered by `Index`.
The `Data` needs to be concatenated in this order
which yields a string
which corresponds to the `Data` of a single ChTransmissionFormat (23) document
without `ChunkMetaData`.

**Example**

```
Chunk 1: CHMED23A.1/4.H4sIAAAAAAAACq2OOw4CMQxE7zIt2ZUTAmzcLZsGiU+KUCEKYKlokIACRbk7jk
Chunk 2: CHMED23A.2/4.LBAWisZz/NyAmb6/gAHxJWI7hsGgqhnsIOnBDBRmF4+9cebCuBtUL0Xy38g73MnI
Chunk 3: CHMED23A.3/4.u+DxX/1nRUkCRiv1zLl9tzOF1uIloqxj9FGTKmId1oHcnxtGM7a+28c9YtJqSZCPkD+
Chunk 4: CHMED23A.4/4.iD8fPQAAAA=
```

is equivalent to

```
CHMED23A.H4sIAAAAAAAACq2OOw4CMQxE7zIt2ZUTAmzcLZsGiU+KUCEKYKlokIACRbk7jkLBAWisZz/NyAmb6/gAHxJWI7hsGgqhnsIOnBDBRmF4+9cebCuBtUL0Xy38g73MnIu+DxX/1nRUkCRiv1zLl9tzOF1uIloqxj9FGTKmId1oHcnxtGM7a+28c9YtJqSZCPkD+iD8fPQAAAA=
```

## Parsing and Serialization

Parsing a ChTransmissionFormat document only relies on the `ReleaseYear`
and potentially the additional meta data it defines
(Note that this is not true for the JSON document itself which can differ between `SubVersion`).
Therefore, the first step is to determine if it is a ChTransmissionFormat document
by checking for the prefix `CHMED` (case-insensitive)
and then parse the two following characters.
Based on their contents, the subsequent parsing can be done.

Serialization is simpler
as the data just needs to be gzipped and base64 encoded
and then prepended by the prefix
(including the used `ReleaseYear`, `SubVersion` etc.).

Both parsing and serialization can be implemented in basically any programming language.
To keep the specification somewhat language agnostic,
the provided examples use basic command line utilities available on Unix-like systems
(on Windows via Windows Subsystem for Linux).
This also demonstrates that the format itself is pretty simple.

### ReleaseYear 16

After the prefix `CHMED16` follows the `SubVersion`
which is not relevant for the parsing of ChTransmissionFormat
but should be stored for further processing of the extracted JSON data.
The following digit `Compression` determines,
if the data needs to be base64 decoded and decompressed
or if the JSON is available directly.
Since it is recommended to always compress the data,
no examples are provided with `Compression = 0`.

**Parsing**

```shell
echo 'SOME_CHMED16_CHTRANSMISSIONFORMAT_DOCUMENT' | sed -r 's/^CHMED16[A-Z]1(.*)$/\1/I' | base64 --decode | zcat
```

**Serialization**

```shell
echo 'SOME_CHMED16_JSON' | gzip -n | base64 | sed -r 's/(.*)/CHMED16A1\1/'
```

Note that a correct implementation should be able to roundtrip,
as do the presented commands:

```shell
echo '{"x":"This is not a valid CHMED16 JSON but is sufficient to show that the commands roundtrip"}' | gzip -n | base64 | sed -r 's/(.*)/CHMED16A1\1/' | sed -r 's/^CHMED16[A-Z]1(.*)$/\1/I' | base64 --decode | zcat
```

### ReleaseYear 23

After the prefix `CHMED23` follows the `SubVersion`
which is not relevant for the parsing of ChTransmissionFormat
but should be stored for further processing of the extracted JSON data.
After the `SubVersion` follows a dot (`.`) to delimit the header from the content.
Optionally, `ChunkMetaData` can follow, which is delimited by a dot again.

Assuming that all chunks have been concatenated (if applicable),
the rest of the data needs to be base64 decoded and gzip decompressed.

**Parsing**

```shell
echo 'SOME_CHMED23_CHTRANSMISSIONFORMAT_DOCUMENT' | sed -r 's/^CHMED23[A-Z]+\.(.*)$/\1/I' | base64 --decode | zcat
```

**Serialization**

```shell
echo 'SOME_CHMED23_JSON' | gzip -n | base64 | sed -r 's/(.*)/CHMED23A.\1/'
```

Note that a correct implementation should be able to roundtrip,
as do the presented commands:

```shell
echo '{"x":"This is not a valid CHMED23 JSON but is sufficient to show that the commands roundtrip"}' | gzip -n | base64 | sed -r 's/(.*)/CHMED23A.\1/' | sed -r 's/^CHMED23[A-Z]+\.(.*)$/\1/I' | base64 --decode | zcat
```

