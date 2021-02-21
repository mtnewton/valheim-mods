# BiggerPickupRadius Plugin for Valheim
Increase your pickup radius (3x by default)

## Installation
1. Download and install [BepInEx Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download this mod and move the `BiggerPickupRadius.dll` into `<GameLocation>\BepInEx\plugins`
3. Launching the game will generate a config file at `<GameLocation>\BepInEx\config`

## Configuration
`<GameLoacation>/BepInEx/config/net.mtnewton.biggerpickupradius.cfg`
```
[BiggerPickupRadius]

## Player pickup radius. Game default is 2
# Setting type: UInt32
# Default value: 6
Radius = 6
```

## Changelog
- v1.1.3
  - update readme - config path
- v1.1.2
  - updated changelog
- v1.1.1
  - fix readme dll name and typos
  - update mod version in files... but forgot to add changelog for version
- v1.1.0
  - pickup radius is now set to a configurable value instead of being multiplied
  - refactor code for simplification
- v1.0.0
  - initial mod creation
  - radius is multiplied by a configurable value (default 3x)

## Source Code
https://github.com/mtnewton/valheim-mods/tree/master/BiggerPickupRadius
