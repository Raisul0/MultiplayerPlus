using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Multiplayer.ViewModelCollection;
using TaleWorlds.ObjectSystem;
using Debug = TaleWorlds.Library.Debug;

namespace MultiplayerPlusClient.Patch
{
    public class Patch_MultiplayerEndOfBattleVM
    {
        private static readonly Harmony Harmony = new Harmony(SubModule.ModuleId + nameof(Patch_MultiplayerEndOfBattleVM));

        private static bool _patched;
        public static bool Patch()
        {
            Debug.Print("** Patch_MultiplayerEndOfBattleVM Patch Begin ! **", 0, Debug.DebugColor.Yellow);
            try
            {
                var orgMethod = typeof(MultiplayerEndOfBattleVM).GetMethod("OnEnabled",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                var newMethod = new HarmonyMethod(typeof(Patch_MultiplayerEndOfBattleVM).GetMethod(
                        nameof(PostfixOnEnabled), BindingFlags.Static | BindingFlags.Public));

                if (_patched)
                    return false;
                _patched = true;
                Harmony.Patch(orgMethod, postfix: newMethod);
            }
            catch (Exception e)
            {
                Debug.Print("** Patch_MultiplayerEndOfBattleVM Patch Failed ! **", 0, Debug.DebugColor.DarkYellow);
                Debug.Print(e.Message, 0, Debug.DebugColor.Red);
                return false;
            }

            Debug.Print("** Patch_MultiplayerEndOfBattleVM Patch Ended ! **", 0, Debug.DebugColor.Yellow);
            return true;
        }

        public static void PostfixOnEnabled(MultiplayerEndOfBattleVM __instance)
        {
            var equipmentElement = new EquipmentElement(MBObjectManager.Instance.GetObject<ItemObject>("mp_pauldron_cape_b"));
            __instance.FirstPlacePlayer.Preview.HeroVisual.SetEquipment(EquipmentIndex.Cape, equipmentElement);
            __instance.FirstPlacePlayer.Preview.HeroVisual.ExecuteStartCustomAnimation("act_taunt_california_gurls");

        }
    }
}
