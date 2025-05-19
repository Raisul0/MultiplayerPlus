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
    public sealed class SetMatchMVPTaunt : GameNetworkMessage
    {
        public string Player1Taunt { get; private set; }
        public string Player2Taunt { get; private set; }
        public string Player3Taunt { get; private set; }

        public SetMatchMVPTaunt(string player1Taunt,string player2Taunt,string player3Taunt)
        {
            Player1Taunt = player1Taunt;
            Player2Taunt = player2Taunt;
            Player3Taunt = player3Taunt;
        }

        public SetMatchMVPTaunt()
        {
        }

        protected override bool OnRead()
        {
            bool bufferReadValid = true;
            Player1Taunt = ReadStringFromPacket(ref bufferReadValid);
            Player2Taunt = ReadStringFromPacket(ref bufferReadValid);
            Player3Taunt = ReadStringFromPacket(ref bufferReadValid);
            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(Player1Taunt);
            WriteStringToPacket(Player2Taunt);
            WriteStringToPacket(Player3Taunt);
        }

        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.None;
        }

        protected override string OnGetLogFormat()
        {
            return $"FromServer.SetMatchMVPTaunt Player1Taunt:{Player1Taunt},Player2Taunt:{Player2Taunt},Player3Taunt:{Player3Taunt}";
        }
    }
}
