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
        public string Taunt3Id { get; private set; }
        public string Taunt3Name { get; private set; }
        public string Taunt4Id { get; private set; }
        public string Taunt4Name { get; private set; }
        public string Taunt5Id { get; private set; }
        public string Taunt5Name { get; private set; }
        public string Taunt6Id { get; private set; }
        public string Taunt6Name { get; private set; }
        public string Taunt7Id { get; private set; }
        public string Taunt7Name { get; private set; }
        public string Taunt8Id { get; private set; }
        public string Taunt8Name { get; private set; }
        public string Taunt9Id { get; private set; }
        public string Taunt9Name { get; private set; }
        public string Taunt10Id { get; private set; }
        public string Taunt10Name { get; private set; }
        public MPTauntWheel TauntWheel { get; private set; }
        public SetPlayerTauntWheel() { 
        }

        public SetPlayerTauntWheel(MPTauntWheel TauntWheel)
        {
            (Taunt1Id, Taunt1Name) = TauntWheel.GetTauntIdNameSlot(1);
            (Taunt2Id, Taunt2Name) = TauntWheel.GetTauntIdNameSlot(2);
            (Taunt3Id, Taunt3Name) = TauntWheel.GetTauntIdNameSlot(3);
            (Taunt4Id, Taunt4Name) = TauntWheel.GetTauntIdNameSlot(4);
            (Taunt5Id, Taunt5Name) = TauntWheel.GetTauntIdNameSlot(5);
            (Taunt6Id, Taunt6Name) = TauntWheel.GetTauntIdNameSlot(6);
            (Taunt7Id, Taunt7Name) = TauntWheel.GetTauntIdNameSlot(7);
            (Taunt8Id, Taunt8Name) = TauntWheel.GetTauntIdNameSlot(8);
            (Taunt9Id, Taunt9Name) = TauntWheel.GetTauntIdNameSlot(9);
            (Taunt10Id, Taunt10Name) = TauntWheel.GetTauntIdNameSlot(10);
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
            this.Taunt3Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt3Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt4Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt4Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt5Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt5Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt6Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt6Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt7Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt7Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt8Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt8Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt9Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt9Name = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt10Id = ReadStringFromPacket(ref bufferReadValid);
            this.Taunt10Name = ReadStringFromPacket(ref bufferReadValid);

            TauntWheel = new MPTauntWheel();

            TauntWheel.UpdateTauntSlot(1, Taunt1Id,"", Taunt1Name);
            TauntWheel.UpdateTauntSlot(2, Taunt2Id,"",  Taunt2Name);
            TauntWheel.UpdateTauntSlot(3, Taunt3Id, "", Taunt3Name);
            TauntWheel.UpdateTauntSlot(4, Taunt4Id, "", Taunt4Name);
            TauntWheel.UpdateTauntSlot(5, Taunt5Id, "", Taunt5Name);
            TauntWheel.UpdateTauntSlot(6, Taunt6Id, "", Taunt6Name);
            TauntWheel.UpdateTauntSlot(7, Taunt7Id, "", Taunt7Name);
            TauntWheel.UpdateTauntSlot(8, Taunt8Id, "", Taunt8Name);
            TauntWheel.UpdateTauntSlot(9, Taunt9Id, "", Taunt9Name);
            TauntWheel.UpdateTauntSlot(10, Taunt10Id, "", Taunt10Name);

            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.Taunt1Id);
            WriteStringToPacket(this.Taunt1Name);
            WriteStringToPacket(this.Taunt2Id);
            WriteStringToPacket(this.Taunt2Name);
            WriteStringToPacket(this.Taunt3Id);
            WriteStringToPacket(this.Taunt3Name);
            WriteStringToPacket(this.Taunt4Id);
            WriteStringToPacket(this.Taunt4Name);
            WriteStringToPacket(this.Taunt5Id);
            WriteStringToPacket(this.Taunt5Name);
            WriteStringToPacket(this.Taunt6Id);
            WriteStringToPacket(this.Taunt6Name);
            WriteStringToPacket(this.Taunt7Id);
            WriteStringToPacket(this.Taunt7Name);
            WriteStringToPacket(this.Taunt8Id);
            WriteStringToPacket(this.Taunt8Name);
            WriteStringToPacket(this.Taunt9Id);
            WriteStringToPacket(this.Taunt9Name);
            WriteStringToPacket(this.Taunt10Id);
            WriteStringToPacket(this.Taunt10Name);
        }
    }
}
