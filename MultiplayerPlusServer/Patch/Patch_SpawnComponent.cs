using HarmonyLib;
using MultiplayerPlusServer.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.Warmup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using Debug = TaleWorlds.Library.Debug;

namespace MultiplayerPlusServer.Patch
{
    public class Patch_SpawnComponent
    {
        private static readonly Harmony Harmony = new Harmony(SubModule.ModuleId + nameof(Patch_SpawnComponent));

        private static bool _patched;
        public static bool Patch()
        {
            Debug.Print("** Patch_SpawnComponent Patch Begin ! **", 0, Debug.DebugColor.Yellow);
            try
            {
                var orgMethod = typeof(SpawnComponent).GetMethod("SetWarmupSpawningBehavior",
                        BindingFlags.Static | BindingFlags.Public);

                var newMethod = new HarmonyMethod(typeof(Patch_SpawnComponent).GetMethod(
                        nameof(PrefixSetWarmupSpawningBehavior), BindingFlags.Static | BindingFlags.Public));

                if (_patched)
                    return false;
                _patched = true;
                Harmony.Patch(orgMethod, prefix: newMethod);
            }
            catch (Exception e)
            {
                Debug.Print("** Patch_SpawnComponent Patch Failed ! **", 0, Debug.DebugColor.DarkYellow);
                Debug.Print(e.Message, 0, Debug.DebugColor.Red);
                return false;
            }

            Debug.Print("** Patch_SpawnComponent Patch Ended ! **", 0, Debug.DebugColor.Yellow);
            return true;
        }

        public static bool PrefixSetWarmupSpawningBehavior()
        {
            Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new FFASpawnFrameBehavior());
            Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new MPPWarmupSpawningBehavior());

            return false;
        }
    }
}
