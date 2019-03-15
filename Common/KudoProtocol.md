# Kudo Server

The name comes from a story I was writing, where a "Kudo" was basically a non-transferable type of Coin, which represents a reputation credit.

Bob's Flying School awards a Level 2 Flying Certificate for Avatar XDP323.   The intention is to make things like levels and experience points something that's portable between worlds.   If you accept kudo from 'Bob's Flying School' then you can honor this kudo anywhere.
You can also use it for other reputational systems (positive, or possibly negative) to downvote trolls or griefers.

Anyway the whole Kudo thing turned into a full-on Certificate Authority.

You could also do real-coin if you wanted to.   Kudo servers could share a blockchain so you can have mutually shared Kudos or Coin (this would be much more stable).   If the "Flight Cooperative" had 400 servers and they all shared the blockchains for the kudos then we'd get the benefits of blockchain.

Shared blockchains could also be used for networks of trust.

## Authentication stops at the Kudo Server

A Kudo server Self-Signs its Root Certificate (as any Root does).  However no other server or certificate is required to vouch for that server.   All I require is that the servers proves that it is who it is, by proving that it is who it was.
In other words.  The server only requires the ability to prove to me that it owns the public key I have for it by signing something.   There's no heirarchy of trust like in the SSL world (though we're going to use the same infrastructure).

Now, there can be blockchains of trust and such, but it's not required.   That's about trust, not authentication.   

Your avatar has a set of certificates that are managed by your avatar server, however these are signed by the Avatar server's Kudo server.

# Discovery

## Login

Your browser first logs you into your Avatar's Kudo server (likely via OAuth).

Once you have validated your Avatar for that browser, your Avatar will either be in a Vestibule (which is a micro-world that just exists in the browser), or a regular World.
You get access to a World by having someone give you a Pass (a type of Kudo--non-transferrable), or arranging entry via an old fashioned website, or by vising a public area of a World (some have them, most don't) and aquiring a Pass that way.

You then basically do an OAuth login via the Pass.  (Some Passes are Permanent).

You typically get your first Avatar this way - by logging into a web site and setting one up.  Now this, is a Possession, which is an even more specialized Kudo.  These typically are transferable under special circumstances (even your Avatar).

However you can't lose things that are Possesions (I'm not sure you can't leave them somewhere you can't get back to--not likely--not willing to have endless support problems caused by this).




# System is not built using Gaming or Movie VR tropes (which are intended to increase drama/tension)

* No, you can't die.  Your Avatar is yours.
* You can be in more than one world at a time (though we're not implementing that initially).   And you're only really awake in one world at time (like tabs in your web-browser--though maybe someday your Avatar can be run by an AI for you).
* You don't have a limit to what you can carry.
* Things that are yours can't be lost.

Now, some of these game-like things might exist inside a world.   Things you pick up inside a world are not necessarily yours and you can't always take them with you or they may have a carrying capacity.

You can have levels and skills and these are affected by the rules of a game world.   A world might kick you out or zero your level/health, but it can't kill you.  Likely it will kick you out (likely temporarily), or it will "respawn" you.




Looking very hard at: https://github.com/ory - Might use just for OAUTH2, or maybe even for a lot more of the Kudo Server
