# Against the Dark
## A JRPG Roguelike
Insert description here.
##### To-Do/Notes:
next to do:
Setup game advancement
Setup the rest of the content

Make animations
"Animator" class for each unit prefab
"Animation" ScriptableObject to tie to each Skill
When Skill is used, tell the user's Animator to do a sequence of things, apply the effect, then run back
??? When a new battle starts, coroutines play from the BattleAnimator? idk

Make sound effects
Learn how sound effects work

Make remaining UI:
Start menu with dropdowns for starting class and name entry
Get a working StatusEffect display?

Make Upgrades
Upgrades require: The fucking god himself to help me--
Upgrades need:
An UpgradeBase ScriptableObject that has 1 function:
Application, which will be overridden on each unique type of upgrade
Each new type of upgrade (basically either Add Skill or Increase Stat) will have a specific time they want to be handled
Add Skill needs to be done on Application
Increase Stat needs to be done to the stat itself when gained, thus on Application
there can be room for triggerable upgrades later
After making UpgradeBase, StatUpgrade, and SkillUpgrade, I can make some test Upgrades and add them to a pool
Presumably I need an UpgradePool ScriptableObject
Then, I can make a UI that shows up on victory, randomly rolls for 1 character (who hasn't gotten an upgrade yet), and grants them a choice of 3 upgrades to gain.
On click, apply the upgrade and start the next fight.
Repeat until the game say you're done.
