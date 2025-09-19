using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;


namespace Lurking_in_the_Dark
{
    public partial class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += SaveLoaded;
            var harmony = new Harmony(ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(NPC), "resetForNewDay"),
                prefix: new HarmonyMethod(typeof(Patches), nameof(Patches.resetForNewDay_Prefix))
            );
        }
        public void SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            try
            {
                var Jem = Game1.getCharacterFromName("JeremyTSnail");
                if (Jem != null)
                    Jem.forceOneTileWide.Set(true);
            }
            catch
            {
            }
        }
    }
}
