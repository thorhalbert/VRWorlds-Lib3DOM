# VRWorlds-Lib3DOM

For now, this .net solution hosts all of the code that is not in the browser (and some libraries that will be used by the browser).

## Browser

The browser is designed to act somewhat like the multi-process nature of Chrome.   There's a main visualization component of the browser itself (running Unity, possibly connected to a VR HMD).  

Then there are a fleet of processes, handling mostly the emissaries from the various world components (the world itself, entities, and the control of the avatar).   It also will make more of these if the user is in more than one world-scene at a time (like multiple tabs in chrome).

There are a handfull of potential side components are emissary servers, mesh engine, and image and mesh caches.  The code for these is directly in this solution as .net core command line projects.  They communicate in various ways, via gRPC, but also with mutexes and there is consideration for the caches to operate with shared memory or memory-mapped files.   Eventually it might even possible to have have the browser run on a cluster of computers.  

## Emissaries

Emissary is a name I chose for well structured, digitally signed, webassembly sandboxed components from the worlds, entities and avatars (and perhaps kudo servers), which run in the browser.   This is similar to running javascript or webassembly sandboxed in the web browser.

## Kudo Server

The idea for Kudos originally was for awards given in games, like the player's level, or certifications of skill.  In a way it is a type of cryptocurrency that is not intended to be traded, but that a group sharing a blockchain could agree that a given user owned a given kudo.   In any case the Kudo server has grown into a generalized secure bookkeeping system which includes cryptography certifications, authentication and authorization.   

## World Server

The world server provides the state machine and status for all entities in the world.   Generally worlds are broken internally into Scenes.   The world server mantains the postion (or current state vector) of objects in the world.   It also has to glean consensus from the physics engines rendering the scene (which the world server can't quite fully trust), to determine the proper physics which is occuring. 

The idea is that, unlike many existing gaming systems, objects in the world have true persistence and true state (though entities keep their state in the entity server, not the world).  If something gets put into a position, it stays there forever until moved.  If you want things to reset their positions, code needs to be added to do this.

## Entity Server

Entity servers contain the objects in a world.  Entities have two parts, the prototype or template, and instances.  You can have a million bricks, but you really only have to have one prototype to describe what a brick looks like.  Emissary code tends to simply operate around the prototype, instances will all then run the same code.

## Avatar Server

The Avatar server controls the first person controller that is the user in your browser.  Note that there are only ever one of these (unless you are materialized in more than one world).   

Also, if you are in second person, you are in a scene with someone else, on their browser your avatar acts as an entity, not an avatar.   So, the Avatar server has to be able to provide both types of interfaces.   


