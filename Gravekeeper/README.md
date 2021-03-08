# Gravekeeper Plugin for Valheim  

Configure to choose what is keep on death. Creates graves for larger inventories.  

[KeepInventory]  
Configure to change what is kept on death.  
Can keep all items on death, equipped items, hotbar, and specific item categories.  
Take a look at the configuration section below to see what is available. These are the default values the config file will generate with.

[Grave]  
Spawns extra graves if the first will not hold everything in your inventory. Configurable, but recommended to keep on.  
Option to spawn a grave with one stone in it if no graves would have spawned on death.  
Option to delete all items on death. Items KeepInventory would keep are still kept.

Both sections can be enabled/disabled independently.

## Configuration  
`<GameLoacation>/BepInEx/config/net.mtnewton.gravekeeper.cfg`
```
[KeepInventory]

## Should Gravekeeper modify what is kept on death?
## Turn on the below options to change what is kept on death.
# Setting type: Boolean
# Default value: true
Enabled = true

## Keep all items on death.
## If set to false, the below options can be set to true to allow for only keeping specific item types.
# Setting type: Boolean
# Default value: true
KeepAll = true

KeepHotbar = false

KeepEquipped = false

KeepEquipment = false

KeepOneHandedWeapons = false

KeepTwoHandedWeapons = false

KeepShields = false

KeepBows = false

KeepHelmets = false

KeepChests = false

KeepLegs = false

KeepHands = false

KeepShoulders = false

KeepUtility = false

KeepTorchs = false

KeepAmmo = false

KeepMaterials = false

KeepConsumables = false

KeepTrophies = false

KeepMisc = false

KeepTools = false


[Grave]

## Should Gravekeeper modify how graves are created?
# Setting type: Boolean
# Default value: true
Enabled = true

## Whatever is not kept by KeepInventory is deleted before grave creation.
# Setting type: Boolean
# Default value: false
DeleteItems = false

## If no graves are to be created, create one with a stone in it.
# Setting type: Boolean
# Default value: false
KeepGrave = false
```

## Installation  
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `Gravekeeper.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLocation>\BepInEx\config`

## Changelog  
- v2.1.1
    - added extra options for KeepInventory
        - Equipment - checks  `ItemDrop.ItemData.IsEquipable()`
        - Most `ItemDrop.ItemData.ItemTypes`
    - KeepInventory will keep quest items so they aren't accidentally deleted if the Grave DeleteItems option is true
- v2.1.0
    - added back option to keep grave on death
    - option to delete items on death - item that match the KeepInventory flags are still kept
    - KeepInventory Enabled flag was accidentally set to false by default
- v2.0.0
    - refactored code
    - new config values - make sure to set them
    - added ability to keep hotbar, equipment, consumables, and ammo. still has keep all option
    - added handling for grave creation
        - creates an extra grave if the original would not contain everything in the players inventory
- v1.2.0  
    - new icon - no one told me that was the back of the gravestone XD  
    - under the hood - now prevents items from moving to grave at all. used to MoveAll items from grave inventory back to players inventory  
    - now has a config  
        - KeepGravestone(false) - option to keep gravestone in world (1 stone added grave inventory)  
        - KeepItemsEquipped(true) - option to unequip items when player dies  
- v1.1.2  
    - updated readme, mod does not generate a config file  
- v1.1.1  
    - fix readme dll name and typos  
- v1.1.0  
    - readme changes - installation steps and changelog  
    - refactor code for simplification  
- v1.0.0  
    - initial mod creation  
    - gives your items back to you after you die  

## Source Code  
https://github.com/mtnewton/valheim-mods/tree/master/Gravekeeper
