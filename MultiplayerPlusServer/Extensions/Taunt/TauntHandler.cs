using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
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
            var peer = baseMessage.Player;
            var tauntId = baseMessage.TauntId;

            var player = MPPlayers.GetMPAgentFromPlayerId(networkPeer.PlayerConnectionInfo.PlayerID.ToString());

            if(player != null)
            {
                var tauntAction = player.TauntWheel.FindTauntAction(tauntId);

                ActionIndexCache suitableTauntAction = ActionIndexCache.Create(tauntAction);

                if (suitableTauntAction.Index >= 0)
                {
                    var agent = networkPeer.ControlledAgent;
                    agent.SetActionChannel(1, suitableTauntAction, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
                }
            }
            

            return true;
        }
    }
}
