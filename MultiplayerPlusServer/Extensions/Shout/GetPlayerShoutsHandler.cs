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
    public class GetPlayerShoutsHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<GetPlayerShouts>(FillShoutWheel);
        }

        public bool FillShoutWheel(NetworkCommunicator networkPeer, GetPlayerShouts baseMessage)
        {
            var userName = baseMessage.UserName;
            var player = MPPlayers.GetMPAgentFromUserName(userName);

            if(player != null)
            {
                if (GameNetwork.IsServer)
                {
                    GameNetwork.BeginModuleEventAsServer(networkPeer);
                    GameNetwork.WriteMessage(new SetPlayerShoutWheel(player.ShoutWheel));
                    GameNetwork.EndModuleEventAsServer();
                }
            }
            
            return true;
        }
    }
}
