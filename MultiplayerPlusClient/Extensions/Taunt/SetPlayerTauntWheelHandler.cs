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

namespace MultiplayerPlusClient.Extensions.Taunt
{
    public class SetPlayerTauntWheelHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SetPlayerTauntWheel>(SetTauntWHeel);
        }

        public void SetTauntWHeel(SetPlayerTauntWheel baseMessage)
        {
            var TauntMenuView = Mission.Current.GetMissionBehavior<MPTauntMenuView>();
            TauntMenuView.UpdateTauntSlots(baseMessage.TauntWheel.Taunts);

        }

    }
}
