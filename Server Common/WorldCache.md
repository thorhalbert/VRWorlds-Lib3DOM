* The world cache contains the state of everything in the world

The initial concept is to store it in redis (persisted redis).   

Base unit is Meters.  I have spoken.  Its meters.  You want something else--convert.  Easy enough.

** I thought of a geocoding scheme:

World.Status.{worldUuid}.{sceneUuid}:{part}.AB:abcdefghijkl/mn

WorldUuid 
SceneUuid

{Part} (if set) allows you to have separate but accessible coordinate worlds inside the same scene.  For pocket universes, or interdimensional space.  Somebody's got to figure out how this gets rendered though.

A is number of bits (in meters) in world (A=1).  T would be 2^20 meters.
B is bits of submeter resolution (A is 1, B is 2, etc), leaving it out is zero

Implied limit for now of 2^26 meters, which is about 47,000 miles.
If we add lower case and numbers bringing it up to 2^62 it's somewhere in the order of 488 light years.

For a given scene/partition these numbers can't change (or you'd need a refactor tool which would rewrite all the inventory)

I'd put different planets into different scenes :)

So, the number of digits you expect on the left and right side of the / (which is optional if no fraction) is set by the prefix letters.

abcefg is as a follows:
  * each bit of the 3 coordinates are split up into binary as bits XYZ (0 to 7), so what we end up with is N octal digits which represent that section of space. I may rearrange XYZ to YXZ, I don't see
  a good reason to make up the most significant digit.  I have an intiution it might make things easer to debug.

You can do a redis wildcard lookup and so something like ...AB:abcdefgh* to find everything in that area of space.   

The contents of this is simply a redis set, which contains the uuids (or map ids) that live in that sector.

You can have a sub-meter fraction if you want to.  

Entity.Status.{entityUuid-orMap}

There will also have to be special consideration given to things which are in motion.   These might generate events in the event system to update a vector.   We are not going to be the physics engine 
for thousands of browers.  However, we will likely coopt the physics engines and gpus of those thousand (or a fraction of that) to do calculations for us.  So, we might be getting vector/collission 
estimates from browsers and updating items for us (update events will come from emissaries in the cloud).  Our physics engine will probably be of that nature.  Structuring the output of the physics 
operating in the browser and likely refereeing it.  We'll probably getting a telemetry feed from moving objects from the emissary.  We can't truly trust the emissary code to not be lying to us. 
If only one emissary is witness to some piece of physics, we can't really double 
check it from another browser's POV.  This is one of those classic MMPORPG problems.  It will likely take a lot of work.  First versions will probably just have to trust the browser closest to the action.

## We also need some scheme to reduce these uuids down to size -- they'll make hash calculation slower.  Likely one server fleet will only be serving one world (or just a few), so there's only ever going to be one WorldUUID.
And probably no more than thousands of scenes.   Likely to be lots of entities though.

I think we can make atomic counters in redis.  One of the cool things you can do besides locks.

So maybe:

Map.Counter = 0

World.Map.{uuid} = {x}
World.Unmap.{x} = {uuid}

Scene.Map...

Entity.Map...

I think I'd rather the counter be global so there's never any ambiguity between the different types.   If there's a Scene 6, then there'd never be an Entity 6.   
Counter should probably be 64 bits.  Though if we ever had Oasis level worlds running on an 10,000 node sharded redis cluster.   This will be pretty crazy anyway.  Just thinking ahead.

Note I never mention avatars here.   As far as the world is concerned we mostly treat them as Entities.  They'll also probably have special fields (isWorld, isAvatar).

---------------

So, thinking about the above.  It's inadequate.  It's fine to be able to use the wildcard, but only interesting if you want bigger than 1m cubes.
I'm starting to think that this 3d-space needs to be thought of as a filesystem, with each tri-bit being a directory node.  And so, everything that this implies.  Fsck type operations, etc.

Also, when things move or change, then events need to be created on the eventbus--everything must revolve around events.  
When nodes become empty, this needs to ripple up the namespace, culling empty sectors.
Obviously creating a "file" requires all of the intervening "node" levels to automatically be created and their directories to be set up (mkdir -p).



