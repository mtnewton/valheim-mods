# A collection of Valheim mods.

## Development

### Install BepInEx
Install BepInEx into the game directory following [these instructions](https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html?tabs=tabid-win)  

### Assembly Publicizer
Download https://github.com/MrPurple6411/Bepinex-Tools/releases/tag/1.0.0-Publicizer, extract and add the folder containing the dlls to `<Game Direcotry>/BepInEx/plugins/`  
Run the game once.  
`<Game Direcotry>/valheim_Data/Managed/` should now contain a folder called `publicized_assemblies`

### Libs folder
Copy the following folders into the `libs` folder  
`<Game Direcotry>/unstripped_corlib/`  
`<Game Direcotry>/valheim_Data/Managed/`  
`<Game Direcotry>/BepInEx/core/`  

### Run
Build the dll via `Build -> Build Solution`  
Copy the Gravekeeper.dll file from `<Solution Directory>/bin/Debug/<Project>.dll` into `<Game Direcotry>/BepInEx/plugins/`  