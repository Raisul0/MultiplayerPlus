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
            var tauntIndex = baseMessage.TauntIndex;

            var taunt = MPTauntWheel.Taunts.FirstOrDefault(x => x.TauntId == tauntIndex);

            var tauntAction = taunt?.TauntAction ?? string.Empty;
            var soundEventName = taunt?.SoundEventName ?? string.Empty;

            ActionIndexCache suitableTauntAction = ActionIndexCache.Create(tauntAction);

            if (suitableTauntAction.Index >= 0)
            {
                networkPeer.ControlledAgent.SetActionChannel(1, suitableTauntAction, false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
                var position = networkPeer.ControlledAgent.Position;
                var soundCodeId = SoundEvent.GetEventIdFromString(soundEventName);
                var soundEvent = SoundEvent.CreateEvent(soundCodeId, Mission.Current.Scene);

                Mission.Current.MakeSound(soundCodeId, position, false, true, -1, -1);
            }


            return true;
        }
    }
}
