using BepInEx.Configuration;

namespace Gravekeeper
{
    class Settings
    { 
        public static void Init(ConfigFile config)
        {
            KeepInventory.Init(config);
            Grave.Init(config);
        }

        public class KeepInventory
        {
            public static ConfigEntry<bool> Enabled { get; private set; }
            public static ConfigEntry<bool> KeepAll { get; private set; }
            public static ConfigEntry<bool> KeepHotbar { get; private set; }
            public static ConfigEntry<bool> KeepEquipped { get; private set; }
            public static ConfigEntry<bool> KeepAmmo { get; private set; }
            public static ConfigEntry<bool> KeepConsumables { get; private set; }

            public static void Init(ConfigFile config)
            {
                string name = "KeepInventory";

                Enabled = config.Bind(name, "Enabled", false,
                    "Should Gravekeeper modify what is kept on death?\n" +
                    "Turn on the below options to change what is kept on death."
                );

                KeepAll = config.Bind(name, "KeepAll", true,
                    "Keep all items on death.\n" +
                    "Grave will dissapear when empty unless [Grave] KeepGrave is true"
                );

                KeepHotbar = config.Bind(name, "KeepHotbar", false,
                    "Items on the hotbar are kept.\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepEquipped = config.Bind(name, "KeepEquipped", false,
                    "Equipped items are kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepAmmo = config.Bind(name, "KeepAmmo", false,
                    "Ammo is kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepConsumables = config.Bind(name, "KeepConsumables", false,
                    "Consumables are kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );
            }
        }

        public class Grave
        {
            public static ConfigEntry<bool> Enabled { get; private set; }
            public static ConfigEntry<bool> ExtraGraves { get; private set; }
            public static ConfigEntry<string> ExtraGravesSuffix { get; private set; }

            public static void Init(ConfigFile config)
            {
                string name = "Grave";

                Enabled = config.Bind(name, "Enabled", true,
                    "Should Gravekeeper modify how graves are created?\n" +
                    "Reccomended to keep true"
                );

                ExtraGraves = config.Bind(name, "ExtraGraves", true,
                    "If the players inventory (visible or not) is larger than the normal grave inventory\n" +
                    "should more tombstones be created to hold those items?\n" +
                    "Reccomended to keep true, otherwise items past the noraml 4 rows could be lost."
                );

                ExtraGravesSuffix = config.Bind(name, "ExtraGravesSuffix", "'s Extras",
                    "If extra graves are created, what should be added to the name?"
                );
            }
        }
    }
}
