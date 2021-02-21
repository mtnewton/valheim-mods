# ExpertMode Plugin for Valheim
Enemies spawn at the specified level (3 by default). And give loot as if at the specified level (1 by default).

Modifying the `global_level` will change what level all enemies spawn at.
`<enemyname>_level_override`, if set above 0, will be used instead of the `gloabl_level` for that enemy.

Modifying the `global_loot_level` will change what level all enemies will drop loot as.
`<enemyname>_loot_level_override`, if set above 0, will be used instead of the `gloabl_loot_level` for that enemy.

Enemy overrides are added dynamically to the config when they are encountered in game. 
Overrides can be added manually if the enemy name is known.

A 0 star enemy is level 1. So:
Level 1 = 0 Stars
Level 2 = 1 Stars
Level 3 = 2 Stars
and so on.

The visual indicator of stars and enemy looks revert back to the original after exceeding level 3.

While the defaults result in a more difficult game, it is possible to modify the configs to result in an EasyMode.
Set the gloabl_level to 1 so all enemies spawn at 0 stars.
Set the global_loot_level to 3 so enemies drop loot as if they were 2 stars.

Be careful of high loot levels, items spawn individually instead of in stacks. Try to stay below 5.

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `ExpertMode.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLoation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.expertmode.cfg`
```
[ExpertMode.Global]

## What level should enemies be? Stars = Level - 1
# Setting type: Int32
# Default value: 3
global_level = 3

## When enemies drop loot, they should act as if they are this level. Game default would be same as global_level
# Setting type: Int32
# Default value: 1
global_loot_level = 1


[ExpertMode.EnemyLevelOverrides]

greydwarfbrute_level_override = 0

greydwarf_level_override = 0

...


[ExpertMode.EnemyLootLevelOverrides]

greydwarfbrute_loot_level_override = 0

greydwarf_loot_level_override = 0

...
```

## Known Issues
- The visual indicator of stars and enemy looks revert back to the original after exceeding level 3.
- High loot level cause lag before items combine. Try to stay below 5.
- 
## Changelog
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
