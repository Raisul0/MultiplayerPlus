using HarmonyLib;
using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.Battle;
using MultiplayerPlusServer.GameModes.Skirmish;
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
    public class Patch_MultiplayerWarmupComponent
    {
        private static readonly Harmony Harmony = new Harmony(SubModule.ModuleId + nameof(Patch_MultiplayerWarmupComponent));

        private static bool _patched;
        public static bool Patch()
        {
            Debug.Print("** Patch_MultiplayerWarmupComponent Patch Begin ! **", 0, Debug.DebugColor.Yellow);
            try
            {
                var orgMethod = typeof(MultiplayerWarmupComponent).GetMethod("EndWarmup",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                var newMethod = new HarmonyMethod(typeof(Patch_MultiplayerWarmupComponent).GetMethod(
                        nameof(PrefixEndWarmup), BindingFlags.Static | BindingFlags.Public));

                if (_patched)
                    return false;
                _patched = true;
                Harmony.Patch(orgMethod, prefix : newMethod);
            }
            catch (Exception e)
            {
                Debug.Print("** Patch_MultiplayerWarmupComponent Patch Failed ! **", 0, Debug.DebugColor.DarkYellow);
                Debug.Print(e.Message,0,Debug.DebugColor.Red);
                return false;
            }

            Debug.Print("** Patch_MultiplayerWarmupComponent Patch Ended ! **", 0, Debug.DebugColor.Yellow);
            return true;
        }

        public static bool PrefixEndWarmup(MultiplayerWarmupComponent __instance)
        {
            var warmupStateProp = AccessTools.Property(typeof(MultiplayerWarmupComponent), "WarmupState");
            warmupStateProp.SetValue(__instance, MultiplayerWarmupComponent.WarmupStates.Ended);

            var timerField = AccessTools.Field(typeof(MultiplayerWarmupComponent), "_timerComponent");
            var gameModeField = AccessTools.Field(typeof(MultiplayerWarmupComponent), "_gameMode");
            var lobbyField = AccessTools.Field(typeof(MultiplayerWarmupComponent), "_lobbyComponent");

            var timerComponent = (MultiplayerTimerComponent)timerField.GetValue(__instance);
            var gameMode = (MissionMultiplayerGameModeBase)gameModeField.GetValue(__instance);
            var lobbyComponent = (MissionLobbyComponent)lobbyField.GetValue(__instance);

            timerComponent?.StartTimerAsServer(3f);
            InvokeOnWarmUpEnded(__instance);
            if (!GameNetwork.IsDedicatedServer)
            {
                MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
                Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
                MissionPeer missionPeer = GameNetwork.MyPeer?.GetComponent<MissionPeer>();
                if (missionPeer?.Team != null)
                {
                    string text = ((missionPeer.Team.Side == BattleSideEnum.Attacker) ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue() : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue());
                    MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/rally/" + text.ToLower()), position);
                }
                else
                {
                    MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/rally/generic"), position);
                }
            }

            Mission.Current.ResetMission();
            gameMode.MultiplayerTeamSelectComponent.BalanceTeams();
            gameMode.SpawnComponent.SpawningBehavior.Clear();

            if(gameMode is MPPSkirmishBehavior)
            {
                Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new MPPSkirmishSpawnFrameBehavior());
                Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new MPPSkirmishSpawningBehavior());
            }
            else if(gameMode is MPPBattleBehavior)
            {
                Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawnFrameBehavior(new MPPBattleSpawnFrameBehavior());
                Mission.Current.GetMissionBehavior<SpawnComponent>().SetNewSpawningBehavior(new MPPBattleSpawningBehavior());
            }



            //if (!__instance.CanMatchStartAfterWarmup())
            //{
            //    lobbyComponent.SetStateEndingAsServer();
            //}

            return false;
        }

        static void InvokeOnWarmUpEnded(MultiplayerWarmupComponent __instance)
        {
            var eventField = typeof(MultiplayerWarmupComponent).GetField("OnWarmupEnded",
                BindingFlags.Instance | BindingFlags.NonPublic);

            if (eventField != null)
            {
                var del = eventField.GetValue(__instance) as Action;
                del?.Invoke();
                Console.WriteLine("[Patch] Invoked OnWarmupEnded via reflection.");
            }
            else
            {
                Console.WriteLine("Could not find event field OnWarmupEnded.");
            }
        }

    }
}
