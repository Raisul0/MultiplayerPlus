using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusCommon.NetworkMessages.FromClient
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromClient)]
    public sealed class GetPlayerMatchMVPTaunt : GameNetworkMessage
    {
        public string Top1Username { get; set; }
        public string Top2Username { get; set; }
        public string Top3Username { get; set; }
        public GetPlayerMatchMVPTaunt() { 
        }
        public GetPlayerMatchMVPTaunt(string top1Username,string top2Username,string top3Username)
        {
            Top1Username = top1Username;
            Top2Username= top2Username;
            Top3Username= top3Username;
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
            this.Top1Username = ReadStringFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.Top1Username);
        }
    }
}
