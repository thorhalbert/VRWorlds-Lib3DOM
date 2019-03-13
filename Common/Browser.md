# Browser Architecture

## EntityLet - horrible name for the codebehind that exists for every world, avatar and entity

World Servers (all entities in a scene in the world), Avatar Servers (entities on/in the Avatar's person), and Entity Servers (Entities inside of other entities) provide the list of entity controllers (or EntityLets).

It also provides us with a sha256 checksum which has been signed by the appropriate kudo server.   Upon receipt this provides us with the hash and the filename for a zip file which will be downloaded and extracted into a directory in the browser cache with the hash as a directory name (after download the hash is checked).
This directory basically gets logically chroot'ed as the working directory for the EntityLet.   If a new version of the entitylet comes along, the new one is downloaded and a transition method is called and the old one is removed.   This allows the entity code to be updated continuously and on the fly.

This directory also has the startup javascript and webassembly that will be loaded for this entity.  These will be compiled in v8 and the "boot" procedure will be started.  If there is mesh data and textures these can then be loaded.    If the entity is no longer required for a scene (or the user changes scenes), then it will timeout and be removed.    Ultimately State is the entity servers responsibility.
