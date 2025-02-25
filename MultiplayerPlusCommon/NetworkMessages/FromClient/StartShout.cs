using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace MultiplayerPlusCommon.NetworkMessages.FromClient
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromClient)]
    public sealed class StartShout : GameNetworkMessage
    {
        public string ShoutId { get; set; }
        public int AgentIndex { get; private set; }
        public NetworkCommunicator Player { get; set; }
        public StartShout()
        {
        }
        public StartShout(string shoutId,int agent, NetworkCommunicator player)
        {
            ShoutId = shoutId;
            AgentIndex = agent;    
            Player = player;
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
            this.AgentIndex = ReadAgentIndexFromPacket(ref result);
            this.ShoutId = ReadStringFromPacket(ref result);
            this.Player = ReadNetworkPeerReferenceFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(ShoutId);
            WriteAgentIndexToPacket(this.AgentIndex);
            WriteNetworkPeerReferenceToPacket(this.Player);
        }
    }
}
