# Against the Dark
## A JRPG Roguelike
Insert description here.
##### To-Do/Notes:
- Make a PartyData class that stores data on all 3 party members (maybe in a PartyMemberData class), then later the game manager can initialize a battle and have the battle manager pull data from PartyData to initialize battle units
- Each gameplay Class is gonna be a Prefab, initialized by the BattleManager and modified upon initialization based on PartyData to set up the battle
- Each Enemy is gonna be a prefab as well, functioning like party members, but each enemy pulls from the appropriate ScriptableObject to set everything for the fight
- Consider adding components to the player prefabs during runtime to assign classes when initializing
