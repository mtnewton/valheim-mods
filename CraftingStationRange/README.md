# CraftingStationRange Plugin for Valheim
Unlimited range for crafting stations. Or configure a custom range.

As a reference, the game default for the stations is 20.

0 = unlimited

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `CraftingStationRange.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLocation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.craftingstationrange.cfg`
```
[CraftingStationRange]

## Station range is set to this amount.
# Setting type: Single
# Default value: 0
$piece_workbench = 0

## Station range is set to this amount.
# Setting type: Single
# Default value: 0
$piece_forge = 0

## Station range is set to this amount.
# Setting type: Single
# Default value: 0
$piece_stonecutter = 0

## Station range is set to this amount.
# Setting type: Single
# Default value: 0
$piece_artisanstation = 0
```
## Known Issues
- When set to unlimited range, a crafting station must still be loaded by the game. Walking far away from a station will casue it to unload.

## Changelog
- v1.0.3
  - update readme - config path
- v1.0.2
  - fix readme dll name and typos
- v1.0.1
  - readme changes - installation steps, known issues, and changelog
- v1.0.0
  - initial mod creation
  - each crafting station can be configured to have a specific range (default is unlimited)

## Source Code
https://github.com/mtnewton/valheim-mods/tree/master/CraftingStationRange
