# Browser Emissary Process

Going by the Chrome model, and how deucedly hard it is to attach real systems code to Unity directly, we're going to put the V8 engine and the emissary processors
in separate processes like Chrome tabs.   And, like Chrome, we're going to use named-pipes to communicate between the browser and the process (and perhaps between processes), though
we're going to use protobufs.
