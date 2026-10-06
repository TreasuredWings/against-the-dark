# Against the Dark
## A JRPG Roguelike
Insert description here.
##### To-Do/Notes:
When the player clicks on a button during the Command state:
- If it is a command, enter the targeting state
- If it is a menu, open the respective menu and wait for a command

During the Targeting state, the player should be able to highlight potential targets and pick one
- After picking a target, execute the command against the target

Command execution:
- Attack:
	- Find reference to target, call the Attack function against it, return to Wait state and call ProcessTurns
- Defend:
	- Find reference to target, toggle Guard on the unit, Wait and ProcessTurns.
- Class Ability:
	- Find reference to target, call the unit's class's Use function, Wait and ProcessTurns.
- Skills:
	- Find reference to target, call the specific skill's Use function, Wait and ProcessTurns.
- Items:
	- Find reference to target, call the item's Use function, Wait and ProcessTurns.

During ProcessTurns, if an Enemy needs to take their turn:
- Have the enemy roll on a table to decide it's action, then have it act on which party member generated the most threat? (also can be random if a cop-out is needed)
- When the enemy acts:
	- Find reference to target, call the chosen action against it, then do nothing because we're in ProcessTurns already

Upgrade structure:
- Upgrades can be stored as a list, storing each instance of an object that was derived from Upgrade
- In theory, we could have Skill, Passive, and Weapon be children of Upgrade
- For Item, it can be a distinct type stored on the game manager? or some other independent area that can be referenced
