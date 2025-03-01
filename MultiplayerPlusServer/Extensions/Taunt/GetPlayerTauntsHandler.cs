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
    public class GetPlayerTauntsHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<GetPlayerTaunts>(FillTauntWheel);
        }

        public bool FillTauntWheel(NetworkCommunicator networkPeer, GetPlayerTaunts baseMessage)
        {
            var playerId = baseMessage.PlayerId;
            var player = MPPlayers.GetMPAgentFromPlayerId(playerId);

            if(player != null)
            {
                if (GameNetwork.IsServer)
                {
                    GameNetwork.BeginModuleEventAsServer(networkPeer);
                    GameNetwork.WriteMessage(new SetPlayerTauntWheel(player.TauntWheel));
                    GameNetwork.EndModuleEventAsServer();
                }
            }
            
            return true;
        }
    }
}
