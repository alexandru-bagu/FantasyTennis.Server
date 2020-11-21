# Fantasy Tennis - Server
Welcome to Fantasy Tennis Server. This is a server emulator for the Fantasy Tennis game written in C# targetting .NET 5.0.

## Status
I am working on it in my spare time, after my full-time job so progress will be quick. The main target of this project is to sharpen my RE skills, and so while some may consider it a waste of time, I will try to document as much as possible from the client-side.

## Demo
We are not there yet. At the moment the server knows only how to say hello, nothing more. At some later time I will make available simple scripts to build and run the server.

## Progress
* Project architecture is complete (or atleast the skeleton)
* Included support for SQLite and MySQL. Currently defaults to SQLite.
* All cryptography methods have been implemented (Blowfish, Xor, Plain)

## Reverse engineering
I use [Ghidra](https://ghidra-sre.org/) to reverse engineer the client. Additionally for debugging I use [x64dbg](https://x64dbg.com/) with [ret-sync](https://github.com/bootleg/ret-sync) to synchronize Ghidra with the debugger. I will update my github repository [FantasyServer.Ghidra](https://github.com/alexandru-bagu/FantasyServer.Ghidra) with all the symbols I figure out. If anyone wants to pick up from where I leave off, they are welcome to it.

## Pull-requests
Public involvement is welcomed but the code must be kept to the same standard I have implemented. The server relies heavily on Dependency Injection by choice.

## Inspiration
I started working on this because I love the game and because of the work done at https://github.com/sstokic-tgm/JFTSE and https://github.com/AnCoFT/AnCoFT. I did not want to work on JFTSE because it's java. I did not continue AnCoFT because it seems it is no longer developed and the architecture leaves a lot to be desired.