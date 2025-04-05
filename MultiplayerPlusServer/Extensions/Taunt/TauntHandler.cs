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
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusServer.Extensions.Taunt
{
    public class TauntHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<StartTaunt>(UseTaunt);
        }

        public bool UseTaunt(NetworkCommunicator networkPeer, StartTaunt baseMessage)
        {
            var tauntId = baseMessage.TauntId;
            var player = MPPlayers.GetMPAgentFromPlayerId(networkPeer.PlayerConnectionInfo.PlayerID.ToString());

            if(player != null)
            {
                var taunt = player.TauntWheel.GetTauntFromId(tauntId);
                var tauntAction = taunt.TauntAction;
                var tauntPrefab = taunt.PrefabName;
                var tauntSound = taunt.SoundEventName;

                ActionIndexCache suitableTauntAction = ActionIndexCache.Create(tauntAction);

                if (suitableTauntAction.Index >= 0)
                {
                    var agent = networkPeer.ControlledAgent;
                    var frame = agent.Frame;

                    var groundHeight = Mission.Current.Scene.GetGroundHeightAtPosition(frame.origin);
                    var frameHeight = frame.origin.z;

                    if (groundHeight + 0.2 < frameHeight)
                    {
                        SendServerMessage("Cannot perfrom Taunt while above Ground!",networkPeer);
                        return false;
                    }

                    else if (agent.HasMount)
                    {
                        SendServerMessage("Cannot perfrom Taunt while mounted!", networkPeer);
                        return false;
                    }

                    agent.SetActionChannel(1, suitableTauntAction, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);

                    if (GameNetwork.IsServer && !string.IsNullOrEmpty(tauntPrefab))
                    {
                        GameNetwork.BeginBroadcastModuleEvent();
                        GameNetwork.WriteMessage(new SpawnTauntPrefab(tauntPrefab, tauntSound, frame));
                        GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None);
                    }
                    
                }
            }
            

            return true;
        }

        public void SendServerMessage(string message,NetworkCommunicator peer)
        {
            if (GameNetwork.IsServer)
            {
                GameNetwork.BeginModuleEventAsServer(peer);
                GameNetwork.WriteMessage(new ServerMessage(message, false));
                GameNetwork.EndModuleEventAsServer();
            }
        }
    }
}
