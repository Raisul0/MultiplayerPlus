using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaleWorlds.MountAndBlade.SkinVoiceManager;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusClient.CustomViews;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ObjectClass;

namespace MultiplayerPlusClient.Extensions.PlayerInfo
{
    public class SetPlayerIdHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SetPlayerId>(PlayerInfoSet);
        }

        public void PlayerInfoSet(SetPlayerId baseMessage)
        {
            MPActivePlayer.PlayerId = baseMessage.PlayerId;
        }

    }
}
