# VR-Worlds Marshalling Process

Emissary is code that runs, sandboxed, on the browser on behalf of the server object.

When the browser needs to materialize the Emissary Packages for an Avatar, Entity, or the World itself, if loads various signed bits from the requisite server and loads it into the appropriate V8 instance in the browser.   

As a minor fig-leaf to security the V8 servers are somewhat segregated by function, and there is an attempt to hide the various parts from each other.   We're going to have to do a real hack-fest to try to make sure that we can keep things from seeing each other as much as possible, but I think the basic assumption going in is that emissaries might be able to see each other.   
There will be some V8 partitioning.  I originally wanted to have a separate V8 server for each manufacturer, but modern computers probably don't have enough memory for this (Once I wanted a separate V8 for each Emissary, but could easily run a rather beefy machine out of memory with a few hundred empty V8 instances--I can see thousands of emissaries running in a complex scene).

The Emissary code is written in Javascript or compiled into WASM these files along with any needed materials, meshes, images, or other assets are assembled into a zip file.  Along with the zip file a signature file which is signed by the proper Code Signer Intermediate Certificate from the Kudo Server's CA.

There are:

*   1-N V8 instances for the Avatar 
*   1-N V8 Instances for the Active Inventory (Entities owned or held by the Avatar)
*   4-N V8 Instances for the World 
*   1-N V8 Instances for the Entities
*   1-N V8 Instances for the Entity aspect of other Avatars/Worlds.  This is how other people's Avatars besides your own present themselves to the world.   Worlds can also present an Entity - like that little blue box which happens to have a Tardis world in it (might even permit more than one, like the Door on Howl's Moving Castle).

There will be an emissary object cache, which will contain the zip files and the signatures to they can be reloaded more quickly without needing to redownload them.  Actually, the cache should save the extracted files.  Just point the entity at the directory.

# Emissary Protocol

There are two and possibily three parts to this.

*  A Zip file which contains the emissary payload
*  A Manifest, though this will might be in the Zip file, it must have the signer key, and basics as to what emissary is and the version control information.   We may simply send this info to the signer and it builds the file and appends it to the zip file and then signs it.
*  The code signing cert.  I think this will simply be a signed sha256 of the final zip file.

Files:

*  Manifest.json
*  CERT.CA - cert which signed the manifest
*  Manifest.pem - signature of manifest?  Not sure if this it's final form
*  _code/  (directory)
** startup.js  - start script (maybe optionally this can be startup.wasm)
*  _payload/  (directory)

Any other javascript and wasm will be put in the _code directory.

The **_payload** directory will essentially be mounted/chroot'ed to the root of the filesystem that is visable to the Emissary (note it can't see its own code).  At this time I see no valid use-case that requires writable access to the disk.   State for the Emissary should always exist on the server side.   The only thing I can think of that would legitmately be there would be for caches.  
And so, I think I'd rather have a more formal caching API to controls that stuff.

So, reading about APK and JAR files internals today and I think I should do something similar.   The manifest file would contain the hashes of all the files and this would get signed.  The signature and the CERT it was signed with (public key) would get added to the zip file.  The zip file itself isn't signed.   
I think this would give more control (which is why the Android and Java people do it this way--I was wondering if they did something special to sign the file).


Researching WASM compilers, and the pickings are pretty thin.  I really wanted to start with C# (and I may try to get mono-wasm to work), but may end up with Go or Rust Emissaries.
