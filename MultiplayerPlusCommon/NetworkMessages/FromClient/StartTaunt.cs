﻿using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace MultiplayerPlusCommon.NetworkMessages.FromClient
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromClient)]
    public sealed class StartTaunt : GameNetworkMessage
    {
        public string TauntId { get; set; }
        public NetworkCommunicator Player { get; set; }
        public StartTaunt() { 
        }
        public StartTaunt(string tauntId, NetworkCommunicator player)
        {
            TauntId = tauntId;
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
            this.TauntId = ReadStringFromPacket(ref result);
            this.Player = ReadNetworkPeerReferenceFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(TauntId);
            WriteNetworkPeerReferenceToPacket(this.Player);
        }
    }
}
