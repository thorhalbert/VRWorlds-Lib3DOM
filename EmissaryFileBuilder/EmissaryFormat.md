Emissary file is a tar file (which can be stored compressed--it will be decompressed to run it)


Manifest.yaml - the manifest for all files, including digital signatures for all files
Cert.CA - the base signing signatures for above
Deploy.yaml - initial deployment instructions
_content - contains fixed models, images -- lego cad, stl, eventually a generic mesh model
_payload - any files which will be available to the code
_code - directory containing the code
   loader.yaml - loading/library linking instructions
   startup.wasm - initial startup - calls main via standard api
   ...  - any other files (typically wasm) which are included - including possible linked libraries
 




First deploy will be from fixed files.
Later, this will be dynamic content.

