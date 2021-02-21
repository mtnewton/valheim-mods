# ExpertMode Plugin for Valheim
Enemies spawn at the specified level.

Modifying the `global_level` will change what level all enemies spawn at.
`<enemyname>_override`, if set above 0, will be used instead of the `gloabl_level` for that enemy.

Enemy overrides are added dynamically to the config when they are encountered in game. 
Overrides can be added manually if the enemy name is known.

A 0 star enemy is level 1. So:
Level 1 = 0 Stars
Level 2 = 1 Stars
Level 3 = 2 Stars
and so on.

The visual indicator of stars and enemey looks revert back to the original after exceeding level 3.

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/])
2. Download this mod and move the `ExpertMode.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLoation>\BepInEx\plugins`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.expertmode.cfg`
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

## Known Issues
- The visual indicator of stars and enemey looks revert back to the original after exceeding level 3.

## Changelog
- v1.0.1
  - readme changes - installation steps, known issues, and changelog
  - const for mod name, fix logging wrong mod name on load
- v1.0.0
  - initial mod creation
  - all enemies spawn at 2 stars by default, configurable.

## Source Code
https://github.com/mtnewton/valheim-mods/tree/master/ExpertMode
