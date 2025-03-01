using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace MultiplayerPlusCommon.NetworkMessages.FromClient
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromClient)]
    public sealed class GetPlayerShouts : GameNetworkMessage
    {
        public string PlayerId { get; set; }
        public MatrixFrame Location { get; set; }
        public GetPlayerShouts() { 
        }
        public GetPlayerShouts(string playerId)
        {
            PlayerId = playerId;
        }
        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.General;
        }

        protected override string OnGetLogFormat()
        {
            return "Checking";
        }

        protected override bool OnRead()
        {
            bool result = true;
            this.PlayerId = ReadStringFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.PlayerId);
        }
    }
}
