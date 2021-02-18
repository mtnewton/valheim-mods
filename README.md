# Gravekeeper Plugin for Valheim

Work in progress mod for valheim

Features:  
🚧 Keep inventory on death  
❔ Option to only keep equipment on death
  

## Development

### Install BepInEx
Install BepInEx into the game directory following [these instructions](https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html?tabs=tabid-win)  

### Libs folder
Copy the following folders into the `libs` folder  
`<Game Direcotry>/unstripped_corlib/`  
`<Game Direcotry>/valheim_Data/Managed/`  
`<Game Direcotry>/BepInEx/core/`  

### Run
Build the dll via `Build -> Build Solution`  
Copy the Gravekeeper.dll file from `<Solution Directory>/bin/Debug/Gravekeeper.dll` into `<Game Direcotry>/BepInEx/plugins/Gravekeeper.dll`  