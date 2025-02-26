using MultiplayerPlusCommon.ObjectClass;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class SetPlayerShoutWheel : GameNetworkMessage
    {
        public string Shout1Id { get; private set; }
        public string Shout1Name { get; private set; }
        public string Shout2Id { get; private set; }
        public string Shout2Name { get; private set; }
        public MPShoutWheel ShoutWheel { get; private set; }
        public SetPlayerShoutWheel() { 
        }

        public SetPlayerShoutWheel(MPShoutWheel ShoutWheel)
        {
            (Shout1Id, Shout1Name) = ShoutWheel.GetShoutIdNameSlot(1);
            (Shout2Id, Shout2Name) = ShoutWheel.GetShoutIdNameSlot(2);
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
            bool bufferReadValid = true;
            this.Shout1Id = ReadStringFromPacket(ref bufferReadValid);
            this.Shout1Name = ReadStringFromPacket(ref bufferReadValid);
            this.Shout2Id = ReadStringFromPacket(ref bufferReadValid);
            this.Shout2Name = ReadStringFromPacket(ref bufferReadValid);

            ShoutWheel = new MPShoutWheel();

            ShoutWheel.UpdateShoutSlot(1, Shout1Id,"", Shout1Name);
            ShoutWheel.UpdateShoutSlot(2, Shout2Id,"", Shout2Name);

            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.Shout1Id);
            WriteStringToPacket(this.Shout1Name);
            WriteStringToPacket(this.Shout2Id);
            WriteStringToPacket(this.Shout2Name);
        }
    }
}
