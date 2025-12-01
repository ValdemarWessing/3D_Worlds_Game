Overview of game:

This game is about a player navigating a small world, talking with other inhabitants trying to figure out what happened to Bob, who aparently left the island but nobody knows exactly why.
Through the story the player will engage in dialogue with NPCs, sail a boat, fly a spaceship, and use a gun.


----------------------------------------------------------------

Mouse and keyboard:

E – interact

F – leave vehicle and activate or deactivate gun

Space – progress conversation

Q / E – roll spaceship

WASD – walk

Shift – sprint

--------------------------------------------------------------

Controller:

West button – interact

East button – leave vehicle and activate or deactivate gun

South button – progress conversation

RB/LB – roll spaceship

Right stick – walk

RT – sprint


----------------------------------------------------------------

Most important scripts:

DialogueSystem – Manages NPC conversations

Interactor – Handles player interaction input and invokes IInteractable behavior on objects.

IInteractable – Interface defining interactable objects (pickups, tents, text triggers).

FirstPersonController – Player walking controller extended with audio and additional input integration (customized beyond stock Unity controller).

PlayerSpaceShip – Flight/ship controller for flying movement and controls.

BoatControls – Sailing controller for boat movement.

Gunscript – Shooting mechanics.

AiTarget – NPC movement

----------------------------------------------------------------

The game was build in UNITY 6.000.2.6f2
