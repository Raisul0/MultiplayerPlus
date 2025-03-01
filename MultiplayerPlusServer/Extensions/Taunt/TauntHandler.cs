using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
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
                    agent.SetActionChannel(1, suitableTauntAction, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);

                    if (GameNetwork.IsServer)
                    {
                        GameNetwork.BeginModuleEventAsServer(networkPeer);
                        GameNetwork.WriteMessage(new SpawnTauntPrefab(tauntPrefab, tauntSound, frame));
                        GameNetwork.EndModuleEventAsServer();
                    }
                    
                }
            }
            

            return true;
        }
    }
}
