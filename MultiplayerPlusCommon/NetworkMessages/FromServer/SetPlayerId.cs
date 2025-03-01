using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class SetPlayerId : GameNetworkMessage
    {
        public string PlayerId { get; private set; }

        public SetPlayerId(string playerId)
        {
            PlayerId = playerId;
        }

        public SetPlayerId()
        {
        }

        protected override bool OnRead()
        {
            bool bufferReadValid = true;
            PlayerId = ReadStringFromPacket(ref bufferReadValid);
            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(PlayerId);
        }

        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.None;
        }

        protected override string OnGetLogFormat()
        {
            return "FromServer.SetPlayerId PlayerId: " + PlayerId ;
        }
    }
}
