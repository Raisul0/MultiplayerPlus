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
    public sealed class AgentShout : GameNetworkMessage
    {
        public string VoiceType { get; private set; }
        public int AgentIndex { get; private set; }

        public AgentShout(string voiceType, int agentIndex)
        {
            this.VoiceType = voiceType;
            AgentIndex = agentIndex;
        }
        public AgentShout()
        {
        }
        protected override bool OnRead()
        {
            bool result = true;
            this.VoiceType = ReadStringFromPacket(ref result);
            this.AgentIndex = ReadAgentIndexFromPacket(ref result);
            return result;
        }
        protected override void OnWrite()
        {
            WriteAgentIndexToPacket(this.AgentIndex);
            WriteStringToPacket(this.VoiceType);
        }
        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.None;
        }
        protected override string OnGetLogFormat()
        {
            return "FromServer.AgentShout";
        }
    }
}
