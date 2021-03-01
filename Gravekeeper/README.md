# Gravekeeper Plugin for Valheim  

Configure to choose what is keep on death. Creates graves for larger inventories.  

Configs to change what is kept on death:  
- KeepAll
- KeepHotbar
- KeepEquipped
- KeepConsumables
- KeepAmmo

Spawns extra graves if the first will not hold everything in your inventory. Configurable, but reccomended to keep on.

Both sections can be disabled independently.

## Configuration  
`<GameLoacation>/BepInEx/config/net.mtnewton.gravekeeper.cfg`
```
[KeepInventory]

## Should Gravekeeper modify what is kept on death?
## Turn on the below options to change what is kept on death.
# Setting type: Boolean
# Default value: false
Enabled = false

## Keep all items on death.
## Grave will dissapear when empty unless [Grave] KeepGrave is true
# Setting type: Boolean
# Default value: true
KeepAll = true

## Items on the hotbar are kept.
## Only needed if [KeepInventory] KeepAll is false
# Setting type: Boolean
# Default value: false
KeepHotbar = false

## Equipped items are kept
## Only needed if [KeepInventory] KeepAll is false
# Setting type: Boolean
# Default value: false
KeepEquipped = false

## Ammo is kept
## Only needed if [KeepInventory] KeepAll is false
# Setting type: Boolean
# Default value: false
KeepAmmo = false

## Consumables are kept
## Only needed if [KeepInventory] KeepAll is false
# Setting type: Boolean
# Default value: false
KeepConsumables = false


[Grave]

## Should Gravekeeper modify how graves are created?
## Reccomended to keep true
# Setting type: Boolean
# Default value: true
Enabled = true

## If the players inventory (visible or not) is larger than the normal grave inventory
## should more tombstones be created to hold those items?
## Reccomended to keep true, otherwise items past the noraml 4 rows could be lost.
# Setting type: Boolean
# Default value: true
ExtraGraves = true

## If extra graves are created, what should be added to the name?
# Setting type: String
# Default value: \'s Extras
ExtraGravesSuffix = \'s Extras

```

## Installation  
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `Gravekeeper.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLocation>\BepInEx\config`

## Changelog  
- v2.0.0
    - refoctored code
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
