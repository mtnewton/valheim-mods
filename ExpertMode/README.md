# ExpertMode Plugin for Valheim
Enemies spawn at the specified level (3 by default). And give loot as if at the specified level (1 by default).

Setting `global_level` greater than 0 will change what level all enemies spawn at.  
`<enemyname>_level_override`, if set above 0, will be used instead of the `gloabl_level` for that enemy.  
If both `gloabl_level` and `<enemyname>_level_override` are set to 0, then ExpertMode will not mess with the enemy level, allowing for the normal spawning of 0-2star enemies.  

Setting `global_loot_level` greater than 0 will change what level all enemies will drop loot as.
`<enemyname>_loot_level_override`, if set above 0, will be used instead of the `gloabl_loot_level` for that enemy.  
If both `global_loot_level` and `<enemyname>_loot_level_override` are set to 0, then ExpertMode will not mess with the enemy drops, allowing for the normal loot level equal to enemy level.  

Enemy overrides are added dynamically to the config when they are encountered in game.  
Overrides can be added manually if the enemy name is known.

A 0 star enemy is level 1. So:  
Level 1 = 0 Stars  
Level 2 = 1 Stars  
Level 3 = 2 Stars  
and so on.  

The visual indicator of stars and enemy looks revert back to the original after exceeding level 3.

While the defaults result in a more difficult game, it is possible to modify the configs to result in an EasyMode.  
1. Set the global_level to 1 so all enemies spawn at 0 stars.  
2. Set the global_loot_level to 3 so enemies drop loot as if they were 2 stars.  

Another possibility is to ony modify select enemies. For example boars for easier leather scraps.  
1. Set the global_level to 0 so that all enemies will follow noraml levels of 0-2 stars unless overwritten.  
2. Set the global_loot_level to 0 so all enemies drop loot following the normal rules unless overwritten.  
3. Now there are two options on how to modify the specific enemy drops
    - Set boar_level_override to 3 and leave boar_loot_level_override at 0. This will make all boars 2 stars and make them drop loot equal to their level.
    - Leave boar_level_override at 0 and set boar_loot_level_override to 3. This will make boars spawn at the normal 0-2 star range, but all will drop loot as if 2 stars.

Be careful of high loot levels, items spawn individually instead of in stacks. Try to stay below 5.

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `ExpertMode.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLoation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.expertmode.cfg`
```
[ExpertMode.Global]

## What level should enemies be? 
## 0 prevents changing levels unless the specific enemy has a level override above 0. 
## Stars = Level - 1
# Setting type: Int32
# Default value: 3
# Acceptable value range: From 0 to 2147483647
global_level = 3

## When enemies drop loot, they should act as if they are this level. 
## 0 prevents changing loot levels unless the specific enemy has an loot level override above 0. 
## Be careful with higher values. Suggest staying below 5.
## Game default would be same as global_level
# Setting type: Int32
# Default value: 1
# Acceptable value range: From 0 to 2147483647
global_loot_level = 1


[ExpertMode.EnemyLevelOverrides]

## What level should neck(s) spawn at? 
## Set above 0 to override the global_level for neck(s) only.  
# Setting type: Int32
# Default value: 0
# Acceptable value range: From 0 to 2147483647
neck_level_override = 0

## What level should boar(s) spawn at? 
## Set above 0 to override the global_level for boar(s) only. 
# Setting type: Int32
# Default value: 0
# Acceptable value range: From 0 to 2147483647
boar_level_override = 0

...


[ExpertMode.EnemyLootLevelOverrides]

## When neck(s) are killed, loot should drop as if they were this level. 
## Set above 0 to override the global_loot_level value for neck(s) only. 
## Be careful with higher values. Suggest staying below 5.
# Setting type: Int32
# Default value: 0
# Acceptable value range: From 0 to 2147483647
neck_loot_level_override = 0

## When boar(s) are killed, loot should drop as if they were this level. 
## Set above 0 to override the global_loot_level value for boar(s) only. 
## Be careful with higher values. Suggest staying below 5.
# Setting type: Int32
# Default value: 0
# Acceptable value range: From 0 to 2147483647
boar_loot_level_override = 0

...
```

## Known Issues
- The visual indicator of stars and enemy looks revert back to the original after exceeding level 3.
- High loot level cause lag before items combine. Try to stay below 5.
- 
## Changelog
- v1.2.0
    - allow for global_level and global_loot_level to be set to 0
        - if the desired level for an enemy is 0 then ExpertMode will not mess with the enemy level, meaning normal 0-2 star spawning.  
        - if the desired loot_level for an enemy is 0 then Expert mode will not mess with the enemy drops, meaning normal loot mutlipliers equal to enemy level.  
- v1.1.0
    - added config for enemy loot level, default to 1 to give same loot as if they were 0 stars
- v1.0.3
    - update readme - config path - typos
- v1.0.2
    - fix readme dll name and typos
- v1.0.1
    - readme changes - installation steps, known issues, and changelog
    - const for mod name, fix logging wrong mod name on load
- v1.0.0
    - initial mod creation
    - all enemies spawn at 2 stars by default, configurable.

## Source Code
https://github.com/mtnewton/valheim-mods/tree/master/ExpertMode
