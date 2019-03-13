# VRWorlds-Lib3DOM
BrowserSide and Serverside Libraries for the 3DOM (3D Object Model) for the VR Browser.   These are intended to be there interface between the sandboxed entity code and the browser itself.  It is somewhat like a browser DOM, but there's one for each entity.   Also, There's a World and Avatar version of the 3DOM which is extended to handle the needs of designing and operating these pieces.


So, there's several entities involved in this transaction.

## Browser

Runs the various V8 instances to run the entity programs.   Presents the 3DOM API, which mostly wraps unity's graphical object framework and makes these available for the code running in the Entity.   It also provides communication to the backend server.

## Entity

A program which runs in the V8 instance and represents a graphical entity.  Entities include a prototype and instances.   Instances can be very light, or very complex.  You can have a thousand bricks, where most of the representation is in the prototype.  Or you can have very complex instances which layer all the content onto a simpler prototype.   And there are singleton-like entities like the Avatar.   Ultimately these are intended to be webassembly objects.  
Initially I'm going to start with the mono-wasm (also used by the Blazor experiment) to try to build my entities.

## Server

This is connected to entities via a bidirectional web-socket rpc type arrangment.  Not entirely sure how I'm going to do that yet.   Ultimately the converstion between the server and the entities is a private affair, but we do want to make sure we've provided a robust framework to build on there.


# Polymorphism

## Base Libraries

## Entities

### Prototypes
### Instances

## Avatars extend Entities

## Worlds  - may be their own creatures, though they do act a bit like Entities in their communication with the baekend server, but these much be much more robust.  Also, the world must interact more vigorously with the browser since the browser will assist with physics and other such things for the world.
