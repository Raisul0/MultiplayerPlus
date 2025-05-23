using HarmonyLib;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
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
    public class Patch_MissionScoreboardComponent
    {
        private static readonly Harmony Harmony = new Harmony(SubModule.ModuleId + nameof(Patch_MissionScoreboardComponent));

        private static bool _patched;
        public static bool Patch()
        {
            Debug.Print("** Patch_MissionScoreboardComponent Patch Begin ! **", 0, Debug.DebugColor.Yellow);
            try
            {
                var orgMethod = typeof(MissionScoreboardComponent).GetMethod("SetPeerAsMVP",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                var newMethod = new HarmonyMethod(typeof(Patch_MissionScoreboardComponent).GetMethod(
                        nameof(PrefixSetPeerAsMVP), BindingFlags.Static | BindingFlags.Public));

                if (_patched)
                    return false;
                _patched = true;
                Harmony.Patch(orgMethod, prefix: newMethod);
            }
            catch (Exception e)
            {
                Debug.Print("** Patch_MissionScoreboardComponent Patch Failed ! **", 0, Debug.DebugColor.DarkYellow);
                Debug.Print(e.Message, 0, Debug.DebugColor.Red);
                return false;
            }

            Debug.Print("** Patch_MissionScoreboardComponent Patch Ended ! **", 0, Debug.DebugColor.Yellow);
            return true;
        }

        public static void PrefixSetPeerAsMVP(MissionPeer peer)
        {
            var playerId = peer.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString();
            var playerTaunt = MPPlayers.GetRoundMVPTauntByPlayerId(playerId);
            Equipment playerEquipment = new Equipment();
            MPPlayers.EquipPlayerGameMVPEquipment(playerId, playerEquipment);
            var playerSide = peer.Team.Side;

            if (GameNetwork.IsServer)
            {
                GameNetwork.BeginBroadcastModuleEvent();
                GameNetwork.WriteMessage(new SetRoundMVPCustomization(playerTaunt, playerEquipment, playerSide));
                GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None);
            }

        }
    }
}
