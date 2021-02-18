# ItemStacks Plugin for Valheim
Increases item stack size (10x by default) and item weight (x0.1 by default).

Loading up a world wihout the mod installed will cause any items in a stack above the normal stack size to be lost.

Configs are generated dynamicly based on the items in the ObjectDB instance. 

You must join a world for it to create the configs.

You may choose to enable/disable the stack size or weight modification by setting the respective enabled flag.

## Configuration
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
