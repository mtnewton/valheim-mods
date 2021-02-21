# ItemStacks Plugin for Valheim
Increases item stack size (10x by default) and item weight (x0.1 by default).

Loading up a world wihout the mod installed will cause any items in a stack above the normal stack size to be lost.

Configs are generated dynamicly based on the items in the ObjectDB instance. 

You must join a world for it to create the configs.

You may choose to enable/disable the stack size or weight modification by setting the respective enabled flag.

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `ItemStacks.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game and joining a world will generate a config file at `<GameLocation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.itemstacks.cfg`
```
[ItemStacks.ItemStackSize]

# Setting type: Boolean
# Default value: true
enabled = true

# Setting type: Int32
# Default value: 200
amber_stack_size = 200

# Setting type: Int32
# Default value: 500
amberpearl_stack_size = 500

...

[ItemStacks.ItemWeight]

# Setting type: Boolean
# Default value: true
enabled = true

# Setting type: Single
# Default value: 0.01
amber_weight = 0.01

# Setting type: Single
# Default value: 0.01
amberpearl_weight = 0.01

...
```

## Changelog
- v1.1.2
  - update readme, config path
- v1.1.1
  - fix readme typos
- v1.1.0
  - readme changes - installation steps and changelog
  - refactor code for simplification
- v1.0.0
  - initial mod creation
  - configurable item stack size and item weight (defaults to 10x base item stack size and 0.1x item weight)

## Source Code
https://github.com/mtnewton/valheim-mods/tree/master/ItemStacks
