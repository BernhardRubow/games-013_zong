# Zong
A pong clone for android for 2 players.

# SOLID-Principles

## Single Responsibility Principle
Every Script on the player should have exactly one function, e.g. nvp_PlayerMove_scr is only responsible
for moving the player

## Open Closed Principle
With switching to the injection of an IPlayerInput-Implementation we open the class to an extension but close it
to change. This means, we don't have to change the code inside nvp_PlayerMove_scr, if we want to extend the
script to use an ai as input.

## Dependency Injection
nvp_PlayerMove_scr uses Dependency Injection via Awake Methode to inject the IPlayerInput-Implementation.
To move the player the script has find a way to get the input of the touches. The former dependency on the Input class is removed by injecting an Implementation of an IPlayerInput-Interface. The PlayerMoveScript
now no longer cares about the 'how' of getting the player input. It's only using the value presented to him.
This opens the class for changes because now its very easy to inject another IPlayerInput-Implementation maybe
for playing against a computer ai.

## Observer Pattern
For Example, the nvp_UiManager_scr subscribes to the 'onPlayerScored' to be informed
when the ball goes out of bounds. This event is invoked by the nvp_GameManager_scr and 
some information about which player has scored and what score he currently has is attached
in the event args

## Message Bus
The Message Bus is a 'GameObjects' integration pattern. A game normal consists of
numerousness GameObjects that are not aware of each other. The message bus patter leads us a
way for centralization of communication between these game objects.

Singleton Pattern
The message bus (nvp_EventManager_scr) is set up as a singleton. So it is reachable quit easy form every game object in the game. Every Script now can subscribe to particular events that the script is interested to get notified if this event occurs. And every Script can easily invoke such event from any location. Every Event is decorated with parameters. A reference to the sender of the event and specially event args which may contain important information to
the subscribers of the event.



## Manual
Place mobile between player. Steer paddle with the finger. Avoid missing ball for high score.

## Credits and Licences


### Music

#### bensound [https://www.bensound.com](https://www.bensound.com)

- bensound-buddy: 
- bensound-clearday: 
- bensound-cute: 
- bensound-dance: 
- bensound-energy: 
- bensound-funnysong: 
- bensound-ukulele: 
- bensound-littleidea: 

### SFX

#### noise for fun [http://www.noiseforfun.com/](http://www.noiseforfun.com/)

- NFF-bounce
- NFF-bounce-02
- NFF-bump
- NFF-disappear
- NFF-jump
- NFF-metal-hit
- NFF-robo-hit
- NFF-slip-02