# Marshall Emissary

This is the server for the emissary modules which must be downloaded from Avatar, Entity, and World servers.   They all need it so it's a common service.

This basically allows a properly authorized and authenticated client to download the emissary zip files and their code-signatures.  The signature will be a small json file with the sha256 of the package zip file and the manifest of it's files, signed by the proper signing cert.

There will be a corresponding API in the common library for calling this, checking it's signature, and extracting it into a cache directory.
