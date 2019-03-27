# Primary Use-Cases


# Principles


## Browser Is only Provisionally Trusted

Private certs are not kept on the browser, even if you store them in the cert/secret stores in windows.  The TOTP scheme gives hour-long grants to such things.  If the browser or the computer it's on becomes later compromised, by then the TOTP keys will have expired (though these might just always be kept in memory--we'll have to see).


## Security Handled by TOTP Tickets for their Certs/Kudos

My for security is that you never actually have the private key on the browser side in any form.  It will hold the public key and the certificate.   But, it has a TOTP (Time-Based One-Time Password) ticket that is valid for some time (maybe an hour).  The idea is that if the browser has a valid TOTP it can renew it and get a new one before the time interval expires.  But if it manages to expire then you have to go through some login process to get another one.

Initially we'll just use a conventional OAUTH2 type login.  Which means we'll need to get keyboards working.  If you added two-factor the user would have to take their goggles off until we get a working VR physical-phone mirror.   If you had an in-world phone you'd have to already be logged into to use it since it belongs to the Avatar.

We might keep more than one TOTP key - for 24 hours, 5 days, 30 days, to lessen the requirements for presentation of credentials to renew the short keys.


## No Stupid Game or Movie VR Tropes



*   Your Avatar can't be killed.   You might have points/health/fuzzywhatzits subtracted from a Kudo until it reaches zero and deathlike things might occur in a particular world which might implement a game, but your Avatar can't die or be taken from you.   Values attached to kudo's are their own thing and mean whatever they mean.
*   Your possessions (entities) are yours.  They can't be taken, stollen, hidden, or lost.  You have a special kudo called a Deed which means it's yours.  You only own your instance of the entity, you have no rights over the entity prototype or other instances.   Items you pick up may or may not become yours or give you any rights to take them out of the world.   This is up to the entity and the world.


## That What Must be Named...



*   These TOTP tokens need a better name
*   EntityLet's need to be named properly.  These are the suite of browser-side code-behind elements that represent Avatars, Entities and Worlds.
*   Might want a better name than 'browser'
*   Also the whole damn project needs a better name.  VR-Worlds isn't too bad, but it isn't good either.


# Use-Cases:


## Avatar Logs into Browser

Or rather, Browser Logs Into Avatar...

This is a tricky one.  I was thinking that avatar creation and such for now is handled via conventional account signup with linkage to a verification source like email or ssms or—even better—a two-factor arrangement like google or such.  So, for now the enrollment is not part of the browser and may never be.  I can imagine a world with a Boutique where you buy and outfit Avatars, though you have to have an Avatar to do that.  I'd rather not have a default Avatar.  I'd like to have some standard for the worlds.  It you didn't have choose, a larger percentage wouldn't.

Contact Avatar server and get basic startup information, including where the authentication needs occur (some server in the Avatar server's fleet, or it's Kudo server).   At this point, the browser caused the server to be vouched.  To make sure it's the same server.

Loophole here, in that I'm not sure you can verify that a server is who it is the very first time you contact it unless you already have it's public key.   There needs to be a way to pre-populate kudo CA info.

There's also no real way to include an ssh ingress to the path the first time.  A lot of times this will be there to allow some to access their own servers inside a cable TV home LAN or something like that.   Though it's a great way to keep private servers behind a corporate firewall.  So negotiating these ingresses might use some other protocol.  The ingress should be trapped and separately firewalled anyway, maybe even dynamic (opening and closing routes to adjust to requirements from the server).

To get your TOTP, an OAUTH2 transaction occurs against the authentication server.  We'll need a keyboard.  

All communications with the servers will require the TOTP key, possibly in the URL (which keeps things from being cached—or replayed).


## Marshall Avatar

Get a manifest for the Avatar and everything it's carrying (current active personal inventory)

Start loading the 'EntityLets' for the Avatar and the Entities (unless they're cached).

These get loaded into the **_Avatar _**and the**_ Current Inventory_** V8 instances.

Marshalling process is generic — it needs its own document.


## Marshal Entities With Avatar

There may be a need for an Entity level of security that I'm not really looking at beyond the Deed.


## Materialize in Vestibule

Avatar preferences can specify a private world where you can initially materialize or you can do that in the generic environments which are available in the browser.


## Avatar Enters World

Avatar presents its **_pass _**to the World's Kudo server.  If it doesn't have one, it petitions for one (some worlds have public areas, to these might get handed out without requiring any credentials).   If it has an expired one, it can petition for renewal (which may or may not be granted), or it might return something temporary (you might materialize in a Limbo location so you might perform more permanent arrangements).

Note that this conversation occurs between the Avatar server and the World servers on behalf of the browser.

If a valid pass is present or can be acquired, the World will then attempt to marshall the Scene.


## Marshall Scene

A Scene is a finite chunk of the world.    The Avatar will materialize in a particular location.  The World server will present the browser with the Manifest of the Scene, sorted in LOD (level of detail order) — usually sorted by distance and size — which represent a priority order.


## Materialize Avatar in World

These are the hardest parts of this, keeping cooperatively the position and movement vectors of all objects within the scene, including the Avatars.   You have to trust the physics engine in some of the browsers, but you can't trust individual ones.  Otherwise they could be spoofed, but at this point in the game we don't want to have to write an full collision and physics engine for the world.  Also, it's worthwhile to crowdsource these calculations to all of the browsers participating in the scene, potentially a great deal of computing power.


## Marshall World Entities in Scene


## Switch Scenes


## Exit World


## Switch between Aspects
