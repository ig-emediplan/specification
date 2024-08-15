# Specification ChTransmissionFormat

**Contact**

Geschäftsstelle IG eMediplan<br>
c/o Köhler, Stüdeli & Partner GmbH<br>
Amthausgasse 18, 3011 Bern<br>
Tel. +41 (0)31 560 00 24<br>
info@emediplan.ch

## Terminology

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL
NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED",  "MAY", and
"OPTIONAL" in this document are to be interpreted as described in
RFC 2119.

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

A ChTransmissionFormat document MUST use UTF-8 encoding.

```
CHMED{ReleaseYear}{SubVersion}{AdditionalMetaData}{Data}
```

Representation as regex:

```regex
^CHMED([0-9]{2})([A-Z]+)(.*)$
```

Description and constraints:

- `CHMED` (Prefix): This is the fixed string `CHMED`.
  This is a fixed value and is used to determine if the given input string
  can be parsed as ChTransmissionFormat.
  During parsing, it SHOULD be treated as case-insensitive (e.g. `ChMed` is also accepted)
  but during serialization all uppercase MUST be used.
- `ReleaseYear`: two digits (e.g. `16`)
- `SubVersion`: At least a single uppercase alphabetic character (e.g. `A`).
  Depending on the `ReleaseYear`, more than one character may be allowed.
  During parsing, it SHOULD be treated as case-insensitive (e.g. `a` is also accepted)
  but during serialization all uppercase MUST be used.
- `AdditionalMetaData`: Based on the `ReleaseYear`, additional meta data may be REQUIRED.
- `Data`: The actual data (JSON) which (after applying transformations based on the `ReleaseYear`)
  needs to conform with the corresponding specification defining the format of the JSON.

Since part of the format is dependent on the `ReleaseYear`,
each available `ReleaseYear` is covered independently.
Years not listed here MUST NOT be accepted when parsing using ChTransmissionFormat.
All constraints listed above also apply to the specific parts defined for a `ReleaseYear`.

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
    Note that it is RECOMMENDED to compress the contents.
    Later versions (e.g. `ReleaseYear` 23) do no longer support documents without compression.
  - if `Compression = 1`: Base64 encoded and gzipped JSON document

**Example**

With compression enabled:

```
CHMED16A1H4sIAAAAAAAEAMVU3W7TMBR+lcq3S4SPHdtx7raVAaKFqutAAnoREreJ2jpT4gKj6ptxx4txnCwVSKQSu0GVqvPX7+fIpwdyuXcFSYiSFCjloGKtNQnI2GGRUZAh1SGwBUASyYTqC8oSSnHgVe4HZM7z1UqF6WcqwkhxjCKdhcJkWc650GIlcXZq8sXDvSEJtHGZpTtjXUOSj4cORyuIpWpRu0EekFnVDYzxmwbtZ+l13dTV7g9t5Ij1eZVicfYWMRab9byxmF1urMH8zpbezO3iNTkGj4RRJIHH7Bwj/J2RhUAHGa+2e/fF1PnPH9bu7XqAnHEhZHzGrqeGJ5EXVVbk9T7bfHr20tTfBwRAxCQHen7f/y7guqi2pnGmLm1j7MbUA/Qx1VLz8/aftvvf7L9Pm2ZQAQgBTEX/QwL+dJa6Et8/SQ7kqr0z0DGEFPxbDsh16R48lqktZjdv8FYwnabfyl2KhRfG5giasIBMTj3nWp4JPrmEjJ93J+fxx5PGTY3X1tbSzuHpGDthCPUu3eKM4O2Kujbr29C3QcSd9ex0udibYyKUWvbLZX3Au54EdupFfSD6QD4CCBEIBcsjjpJZUVnv6yJCxlEsYMRk+59z62pj/MLuLBreoe2vZj2KsfOhvMcyp0yhAzLfbbzj4y8tzloh3gQAAA==
```

With compression disabled:

```
CHMED16A0{"Auth":"7601003178999","Dt":"2016-09-12T11:46:09+02:00","Id":"26d3dff7-ab05-4737-a49c-5eccd33595f6","MedType":1,"Medicaments":[{"Id":"971867","IdType":3,"Pos":[{"D":[0,0,0,0],"DtFrom":"2016-09-12"}],"Roa":"PO","TkgRsn":"Akne","Unit":"STK"},{"Id":"4461382","IdType":3,"Pos":[{"D":[0,1,0,0],"DtFrom":"2016-02-10"}],"Roa":"PO","TkgRsn":"Blutverdünnung","Unit":"STK"},{"Id":"2355687","IdType":3,"Pos":[{"D":[1,0,1,0],"DtFrom":"2016-02-10"}],"Roa":"PO","TkgRsn":"Bluthochdruck\/Herz","Unit":"STK"},{"Id":"1426310","IdType":3,"Pos":[{"D":[0,0,1,0],"DtFrom":"2016-02-10"}],"Roa":"PO","TkgRsn":"Cholesterinsenker","Unit":"STK"},{"Id":"809693","IdType":3,"Pos":[{"D":[1,0,0,0],"DtFrom":"2016-02-10"}],"Roa":"PO","TkgRsn":"Bluthochdruck\/Wasser","Unit":"STK"},{"Id":"1551274","IdType":3,"Pos":[{"D":[1,0,0,0],"DtFrom":"2016-02-10"}],"Roa":"PO","TkgRsn":"Bluthochdruck\/Wasser","Unit":"STK"}],"Patient":{"BDt":"1981-01-12","City":"Bern","FName":"Maxima","Gender":2,"LName":"Matter","Lng":"DE","Med":{"DLstMen":"","Meas":[{"Type":1,"Unit":2,"Val":"53"},{"Type":2,"Unit":1,"Val":"158"}],"Rc":[{"Id":1,"R":[577]},{"Id":2},{"Id":3,"R":[612]},{"Id":4},{"Id":5},{"Id":6,"R":[555,571]}]},"Phone":"+4158 851 2600","Street":"Untermattweg 8","Zip":"3027"},"Rmk":""}
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
    If all chunks are concatenated in the correct order, it MUST result in a valid JSON document
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
  The `Index` MUST be lower or equal to the `Total`
  and be unique within the list of ChTransmissionFormat (23) documents
  which represent the full document.
- `Total`: Positive integer which defines the total number of chunks the full document has been split into.
  MUST be greater than 1 as `ChunkMetaData` and not be used if no actual chunking is applied.

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
but is relevant for further processing of the extracted JSON data.
The following digit `Compression` determines,
if the data needs to be base64 decoded and decompressed
or if the JSON is available directly.

**Parsing**

With compression enabled:

```shell
echo 'SOME_CHMED16_CHTRANSMISSIONFORMAT_DOCUMENT' | sed -r 's/^CHMED16[A-Z]1(.*)$/\1/I' | base64 --decode | zcat
```

With compression disabled:

```shell
echo 'SOME_CHMED16_CHTRANSMISSIONFORMAT_DOCUMENT' | sed -r 's/^CHMED16[A-Z]0(.*)$/\1/I'
```

**Serialization**

With compression enabled
(serialization SHOULD NOT be done without compression,
so no example is provided here):

```shell
echo 'SOME_CHMED16_JSON' | gzip -n | base64 | sed -r 's/(.*)/CHMED16A1\1/'
```

Note that a correct implementation MUST be able to roundtrip,
as do the presented commands:

```shell
echo '{"x":"This is not a valid CHMED16 JSON but is sufficient to show that the commands roundtrip"}' | gzip -n | base64 | sed -r 's/(.*)/CHMED16A1\1/' | sed -r 's/^CHMED16[A-Z]1(.*)$/\1/I' | base64 --decode | zcat
```

### ReleaseYear 23

After the prefix `CHMED23` follows the `SubVersion`
which is not relevant for the parsing of ChTransmissionFormat
but is relevant for further processing of the extracted JSON data.
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

Note that a correct implementation MUST be able to roundtrip,
as do the presented commands:

```shell
echo '{"x":"This is not a valid CHMED23 JSON but is sufficient to show that the commands roundtrip"}' | gzip -n | base64 | sed -r 's/(.*)/CHMED23A.\1/' | sed -r 's/^CHMED23[A-Z]+\.(.*)$/\1/I' | base64 --decode | zcat
```

