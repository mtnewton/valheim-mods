# ItemStacks Plugin for Valheim
Increases item stack size (10x by default) and item weight (x0.1 by default).

Loading up a world without the mod installed or reducing item stack size will cause any items in a stack above the new stack size to be lost.

Configuration entries are generated based on the items in the ObjectDB instance.  
You must join a world for it to create the individual item config entries.

You may choose to enable/disable the stack size or weight modification by setting the respective enabled flag.

Item weights and stack sizes can be individually set, setting these values above 0 will have it ignore the multiplier and use the given value instead.

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `ItemStacks.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game and joining a world will generate a config file at `<GameLocation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.itemstacks.cfg`
```
[ItemStacks.ItemMultipliers]

## Multiply the original item stack size by this value
## Minimum resulting stack size is 1
## Overwritten by individual item _stack_size values.
# Setting type: Single
# Default value: 10
stack_size_multiplier = 10

## Multiply the original item weight by this value
## Overwritten by individual item _weight values.
# Setting type: Single
# Default value: 0.1
weight_multiplier = 0.1

[ItemStacks.ItemStackSize]

## Should item stack size be modified?
# Setting type: Boolean
# Default value: true
enabled = true

## Set above 0 to use this value instead of the stack size multiplier
## Game default: 20
# Setting type: Int32
# Default value: 0
amber_stack_size = 0

## Set above 0 to use this value instead of the stack size multiplier
## Game default: 50
# Setting type: Int32
# Default value: 0
amberpearl_stack_size = 0

...

[ItemStacks.ItemWeight]

## Should item weight be modified?
# Setting type: Boolean
# Default value: true
enabled = true

## Set above 0 to use this value instead of the weight multiplier
## Game default: 0.1
# Setting type: Single
# Default value: 0
amber_weight = 0

## Set above 0 to use this value instead of the weight multiplier
## Game default: 0.1
# Setting type: Single
# Default value: 0
amberpearl_weight = 0

...
```

## Changelog
- v1.2.0
    - added config option for stack and weight multiplier
    - specific item configs now default to 0
        - setting these above 0 will cause the weight or stack size to be set to this value instead of being multiplied.
        - previous versions set item specific values, these will need to be deleted or set to 0 for a custom multiplier different than the previously default 10x/0.1x to take effect
- v1.1.3
    - item weight toggle was using wrong config value
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
