using MultiplayerPlusCommon.ObjectClass;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class SetPlayerTauntWheel : GameNetworkMessage
    {
        public string Taunt1Id { get; private set; }
        public string Taunt1Name { get; private set; }
        public string Taunt2Id { get; private set; }
        public string Taunt2Name { get; private set; }
        public MPTauntWheel TauntWheel { get; private set; }
        public SetPlayerTauntWheel() { 
        }

        public SetPlayerTauntWheel(MPTauntWheel TauntWheel)
        {
            (Taunt1Id, Taunt1Name) = TauntWheel.GetTauntIdNameSlot(1);
            (Taunt2Id, Taunt2Name) = TauntWheel.GetTauntIdNameSlot(2);
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
            this.Taunt1Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt1Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt2Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt2Name = ReadStringFromPacket(ref bufferReadValid);

            TauntWheel = new MPTauntWheel();

            TauntWheel.UpdateTauntSlot(1, Taunt1Id,"", Taunt1Name);
            TauntWheel.UpdateTauntSlot(2, Taunt2Id,"", Taunt2Name);

            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.Taunt1Id);
            WriteStringToPacket(this.Taunt1Name);
            WriteStringToPacket(this.Taunt2Id);
            WriteStringToPacket(this.Taunt2Name);
        }
    }
}
