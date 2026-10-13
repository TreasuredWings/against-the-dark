# Against the Dark
## A JRPG Roguelike
### Changes From Design Document
While developing this game, I came across a lot of challenges, mainly about trying to store a large amount of data in an organized way that can also change functionality. What I ended up using was Unit's ScriptableObjects feature, which let me make a sort-of "database" inside my project, allowing me to make a large amount of data, change it quickly for testing and balancing, and attach static functionality to each new effect, skill, behavior, and so on.

Because I ran out of time, I could only implement mouse controls, but the entire game is still playable from start to finish, including 9 rounds of fights with a randomly rolled upgrade between each one, and a boss fight at the end against a 1000 HP dragon. Because I ran out of time to fully balance the game, I'm hoping that it'll at least have a decent difficulty curve and prove a challenge near the end. I also managed to create a decent variety of upgrades to choose from and a few enemies with unique behaviors, making the game interesting to replay.

Once again, because of time constraints, I could not implement a Turn Order view or a Status Effect Display, but the status effects do in fact work, and turns are decided in a round-by-round basis. I also deviated by adding a Stun meter to every enemy, allowing the player to strike enemy weaknesses and attempt to score huge damage on an enemy. I hope it's at least somewhat fun to replay.
