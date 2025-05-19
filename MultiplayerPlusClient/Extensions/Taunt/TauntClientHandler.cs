using MultiplayerPlusClient.CustomViews;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusClient.Extensions.Taunt
{
    public class TauntClientHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SetMatchMVPTaunt>(SetPlayerMatchMVPTaunt);
        }

        public void SetPlayerMatchMVPTaunt(SetMatchMVPTaunt setMatchMVPTaunt)
        {
            var endOfBattleUIHandler = Mission.Current.GetMissionBehavior<MPPEndOfBattleUIHandler>();
            endOfBattleUIHandler.SetMVPAnimation(setMatchMVPTaunt.Player1Taunt, setMatchMVPTaunt.Player2Taunt, setMatchMVPTaunt.Player3Taunt);
        }
    }
}
