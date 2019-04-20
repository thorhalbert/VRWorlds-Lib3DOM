# Emissary File Creation

CreateSignedEmissary directory

Ensure that proper files are in whichever directories.  Only acceptable directories are _code and _payload directories.   Files in root are strictly controlled.
Outside of this, the manifest is constructed and the proper key files are included (the program may have to prompt for these, and for other manifest fields).

All files are checksummed (sha256) and placed in the manifest.

Final manifest file is checksummed and signed.   Signature file is added.

Tar file is constructed from all above.  Tar file is gzip compressed.

## Version 1 of this will be a tool which will have direct access to the signing certificate

## version 2 will be the unsecured part of this, which will submit a partially assembled tar file to a kudo microservice with developer authentication, which will generate and sign the final form and place it into the emissary cache on the required server.


Manifest file is JSON

 * Emissary-Manifest
 ** Version:  0.0
 ** APIVersion: 0.0
 ** Architecture: V8
 ** Manufacturer-ID:
 ** Signer-CERT-ID:
 ** Developer-ID:
 ** Primary-Role:  Avatar, Entity, World
 ** Indirect-Role:  Avatar, World -- both would be Entity if not in their primary aspect
 ** Marhsalling
 *** Requires
 *** Exports
 *** Imports
 *** Startup
 ** Revision
 *** Package-ID:
 *** Name:
 *** Version:
 *** Repository:
 ** Contents:  [] - everything except this file and it's signature
 *** Path
 *** Length
 *** SHA256
