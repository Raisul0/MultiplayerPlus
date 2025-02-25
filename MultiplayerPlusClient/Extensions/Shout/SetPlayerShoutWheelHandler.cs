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

namespace MultiplayerPlusClient.Extensions.Shout
{
    public class SetPlayerShoutWheelHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SetPlayerShoutWheel>(SetShoutWHeel);
        }

        public void SetShoutWHeel(SetPlayerShoutWheel baseMessage)
        {
            var shoutMenuView = Mission.Current.GetMissionBehavior<MPShoutMenuView>();
            shoutMenuView.UpdateShoutSlots(baseMessage.ShoutWheel.Shouts);

        }

    }
}
