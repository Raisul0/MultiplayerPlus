using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.SkinVoiceManager;

namespace MultiplayerPlusServer.Extensions.Shout
{
    public class ShoutHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<StartShout>(MakeShout);
        }

        public bool MakeShout(NetworkCommunicator networkPeer, StartShout baseMessage)
        {
            var peer = baseMessage.Player;
            var shoudId = baseMessage.ShoutId;

            var player = MPPlayers.GetMPAgentFromPlayerId(networkPeer.PlayerConnectionInfo.PlayerID.ToString());

            if(player != null)
            {
                var voiceType = player.ShoutWheel.FindShoutVoiceType(shoudId);

                if (!string.IsNullOrEmpty(voiceType))
                {

                    networkPeer.ControlledAgent.MakeVoice(new SkinVoiceType(voiceType), SkinVoiceManager.CombatVoiceNetworkPredictionType.OwnerPrediction);

                    if (GameNetwork.IsMultiplayer)
                    {
                        GameNetwork.BeginBroadcastModuleEvent();
                        GameNetwork.WriteMessage(new AgentShoutTextDisplay(networkPeer.ControlledAgent.Index, voiceType));
                        GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkPeer);
                    }

                }
            }
            
            return true;
        }
    }
}
