# Gravekeeper Plugin for Valheim  
Keep your items when you die.  
Config to keep the gravestone from crumbling (1 stone added to its inventory).  
Config to unequip items on death.  

## Configuration  
`<GameLoacation>/BepInEx/config/net.mtnewton.gravekeeper.cfg`
```
[Gravekeeper]

## Should the gravestone stay in the world? (you still keep your items) One stone is added to the graves inventory to keep it standing.
# Setting type: Boolean
# Default value: false
KeepGravestone = false

## Should items stay equipped when you respawn?
# Setting type: Boolean
# Default value: true
KeepItemsEquipped = true
```

## Installation  
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `Gravekeeper.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLocation>\BepInEx\config`

## Changelog  
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
