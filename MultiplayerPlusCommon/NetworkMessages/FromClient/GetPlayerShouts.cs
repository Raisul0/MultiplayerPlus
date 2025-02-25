using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusCommon.NetworkMessages.FromClient
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromClient)]
    public sealed class GetPlayerShouts : GameNetworkMessage
    {
        public string UserName { get; set; }
        public MatrixFrame Location { get; set; }
        public GetPlayerShouts() { 
        }
        public GetPlayerShouts(string userName)
        {
            UserName = userName;
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
            this.UserName = ReadStringFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.UserName);
        }
    }
}
