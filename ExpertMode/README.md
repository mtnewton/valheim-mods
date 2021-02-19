# ExpertMode Plugin for Valheim
Enemies spawn at the specified level.

Modifying the GlobalLevel will change what level all enemies spawn at.
<enemyname>_override, if set above 0, will be used instead of the GlobalLevel for that enemy.

Enemy overrides are added dynamically to the config when they are encountered in game. 
Overrides can be added manually if the enemy name is known.

A 0 star enemy is level 1. So:
Level 1 = 0 Stars
Level 2 = 1 Stars
Level 3 = 2 Stars
and so on.

The visual indicator of stars and enemey looks revert back to the original after exceeding level 3.


## Configuration
```
[ExpertMode.Global]

## What level should enemies be? Stars = Level - 1
# Setting type: UInt32
# Default value: 3
global_level = 3


[ExpertMode.EnemyOverrides]

## Set above 0 to use this value instead of GlobalLevel for this enemy.  Stars = Level - 1
# Setting type: UInt32
# Default value: 0
boar_override = 0

## Set above 0 to use this value instead of GlobalLevel for this enemy.  Stars = Level - 1
# Setting type: UInt32
# Default value: 0
greyling_override = 0

## Set above 0 to use this value instead of GlobalLevel for this enemy.  Stars = Level - 1
# Setting type: UInt32
# Default value: 0
deer_override = 0

...
```
