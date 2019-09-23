# Orb

Allright, so 2 days for such a project is a pretty tough timeframe. Still pretty fun project to deal with.

So, I decided to not use any third party lib/code, only unity stuff, so the most confusing part may be the DI, because I wrote some simple di container (SimpleDi class, IResolver, IBinder interfaces) and the bindings/bootstrapping is in DiContainer.cs.

The game is divided into assemblies: Core and Physics contains the game mechanics, Di, Di.Simple and DiContext are all part of DI implementation, View is for game mechanics entities that exist in scenes and are dependent on Core, Ui is all ui-stuff, Meta contains parts of game that wraps core gameplay into metagame with main menu/restarts/loading scenes/preparign DI etc, and Save which is pretty straitforward.

Basically the main part of the game is in Game class, which got a little messy by the end, you may start looking from there. There are 2 weapons, single shot and multi shot, assigned randomly at start. Planets can be manipulated from scene, they have mass and hp. Human and ai players are assigned randomly as well. Ai is stupid, it just shoots randomly, but in this game it's still pretty effective strategy.

All bullets and planets (and star if you let it) obey gravity, which is calculated by newton intergation in Solver.cs. Newton behaves very funny at small distances/large forces, so I'd like to use rk4, but that's really out of scope. You may try putting another star near the existing one to see everything go crazy. The idea of Solver was to implement a kind of ecs approach, which it isn't, but still showing nice performance considering the amount of bodies in simulation. As it is put out in different assembly it would be pretty straitforward to move it more towards unity ecs, or doing the pattern manually.

Saves are not saving everything, but it is there to show the point, how it would have been done with this architecture. No game part, except for Orb.Save assembly knows anything about serialization/jsons/whatever. This is not how I'd have done saves in a larger project, because it has no versioning, conversion between versions, error handling, but it's good enough to show the intent here.

Weapons are added by one per planet, which is easy to change, and are created by factory which scopes all appdomain. This is pretty dirty, and the ugly refection code should be put away somewhere else. Still this way you are not limited by the damage/acceleration/reload constrains and can create any kind of weapon without messing with already existing code. Pretty much the same goes for planets and bullets, hence the factories.
