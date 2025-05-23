using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class SetMatchMVPCustomization : GameNetworkMessage
    {
        public string Player1Taunt { get; private set; }
        public string Player1EquipmentHead { get; private set; }
        public string Player1EquipmentShoulder { get; private set; }
        public string Player1EquipmentBody { get; private set; }
        public string Player1EquipmentArms { get; private set; }
        public string Player1EquipmentLegs { get; private set; }
        public string Player1Weapon0 { get; private set; }
        public string Player1Weapon1 { get; private set; }
        public string Player1Weapon2 { get; private set; }
        public string Player1Weapon3 { get; private set; }
        public string Player2Taunt { get; private set; }
        public string Player2EquipmentHead { get; private set; }
        public string Player2EquipmentShoulder { get; private set; }
        public string Player2EquipmentBody { get; private set; }
        public string Player2EquipmentArms { get; private set; }
        public string Player2EquipmentLegs { get; private set; }
        public string Player2Weapon0 { get; private set; }
        public string Player2Weapon1 { get; private set; }
        public string Player2Weapon2 { get; private set; }
        public string Player2Weapon3 { get; private set; }
        public string Player3Taunt { get; private set; }
        public string Player3EquipmentHead { get; private set; }
        public string Player3EquipmentShoulder { get; private set; }
        public string Player3EquipmentBody { get; private set; }
        public string Player3EquipmentArms { get; private set; }
        public string Player3EquipmentLegs { get; private set; }
        public string Player3Weapon0 { get; private set; }
        public string Player3Weapon1 { get; private set; }
        public string Player3Weapon2 { get; private set; }
        public string Player3Weapon3 { get; private set; }


        public SetMatchMVPCustomization(string player1Taunt,string player2Taunt,string player3Taunt, Equipment player1,Equipment player2,Equipment player3)
        {
            Player1Taunt = player1Taunt;
            Player2Taunt = player2Taunt;
            Player3Taunt = player3Taunt;

            Player1EquipmentHead     = player1[EquipmentIndex.Head].Item?.StringId ?? "";
            Player1EquipmentShoulder = player1[EquipmentIndex.Cape].Item?.StringId ?? "";
            Player1EquipmentBody     = player1[EquipmentIndex.Body].Item?.StringId ?? "";
            Player1EquipmentArms     = player1[EquipmentIndex.Gloves].Item?.StringId ?? "";
            Player1EquipmentLegs     = player1[EquipmentIndex.Leg].Item?.StringId ?? "";
            Player1Weapon0 = player1[EquipmentIndex.Weapon0].Item?.StringId ?? "";
            Player1Weapon1 = player1[EquipmentIndex.Weapon1].Item?.StringId ?? "";
            Player1Weapon2 = player1[EquipmentIndex.Weapon2].Item?.StringId ?? "";
            Player1Weapon3 = player1[EquipmentIndex.Weapon3].Item?.StringId ?? "";

            Player2EquipmentHead     = player2[EquipmentIndex.Head].Item?.StringId ?? "";
            Player2EquipmentShoulder = player2[EquipmentIndex.Cape].Item?.StringId ?? "";
            Player2EquipmentBody     = player2[EquipmentIndex.Body].Item?.StringId ?? "";
            Player2EquipmentArms     = player2[EquipmentIndex.Gloves].Item?.StringId ?? "";
            Player2EquipmentLegs     = player2[EquipmentIndex.Leg].Item?.StringId ?? "";
            Player2Weapon0 = player2[EquipmentIndex.Weapon0].Item?.StringId ?? "";
            Player2Weapon1 = player2[EquipmentIndex.Weapon1].Item?.StringId ?? "";
            Player2Weapon2 = player2[EquipmentIndex.Weapon2].Item?.StringId ?? "";
            Player2Weapon3 = player2[EquipmentIndex.Weapon3].Item?.StringId ?? "";

            Player3EquipmentHead     = player3[EquipmentIndex.Head].Item?.StringId ?? "";
            Player3EquipmentShoulder = player3[EquipmentIndex.Cape].Item?.StringId ?? "";
            Player3EquipmentBody     = player3[EquipmentIndex.Body].Item?.StringId ?? "";
            Player3EquipmentArms     = player3[EquipmentIndex.Gloves].Item?.StringId ?? "";
            Player3EquipmentLegs     = player3[EquipmentIndex.Leg].Item?.StringId ?? "";
            Player3Weapon0 = player3[EquipmentIndex.Weapon0].Item?.StringId ?? "";
            Player3Weapon1 = player3[EquipmentIndex.Weapon1].Item?.StringId ?? "";
            Player3Weapon2 = player3[EquipmentIndex.Weapon2].Item?.StringId ?? "";
            Player3Weapon3 = player3[EquipmentIndex.Weapon3].Item?.StringId ?? "";

        }

        public SetMatchMVPCustomization()
        {
        }

        protected override bool OnRead()
        {
            bool bufferReadValid = true;
            Player1Taunt = ReadStringFromPacket(ref bufferReadValid);
            Player2Taunt = ReadStringFromPacket(ref bufferReadValid);
            Player3Taunt = ReadStringFromPacket(ref bufferReadValid);

            Player1EquipmentHead      = ReadStringFromPacket(ref bufferReadValid);
            Player1EquipmentShoulder  = ReadStringFromPacket(ref bufferReadValid);
            Player1EquipmentBody      = ReadStringFromPacket(ref bufferReadValid);
            Player1EquipmentArms      = ReadStringFromPacket(ref bufferReadValid);
            Player1EquipmentLegs      = ReadStringFromPacket(ref bufferReadValid);
            Player1Weapon0 = ReadStringFromPacket(ref bufferReadValid);
            Player1Weapon1 = ReadStringFromPacket(ref bufferReadValid);
            Player1Weapon2 = ReadStringFromPacket(ref bufferReadValid);
            Player1Weapon3 = ReadStringFromPacket(ref bufferReadValid);

            Player2EquipmentHead      = ReadStringFromPacket(ref bufferReadValid);
            Player2EquipmentShoulder  = ReadStringFromPacket(ref bufferReadValid);
            Player2EquipmentBody      = ReadStringFromPacket(ref bufferReadValid);
            Player2EquipmentArms      = ReadStringFromPacket(ref bufferReadValid);
            Player2EquipmentLegs      = ReadStringFromPacket(ref bufferReadValid);
            Player2Weapon0 = ReadStringFromPacket(ref bufferReadValid);
            Player2Weapon1 = ReadStringFromPacket(ref bufferReadValid);
            Player2Weapon2 = ReadStringFromPacket(ref bufferReadValid);
            Player2Weapon3 = ReadStringFromPacket(ref bufferReadValid);

            Player3EquipmentHead      = ReadStringFromPacket(ref bufferReadValid);
            Player3EquipmentShoulder  = ReadStringFromPacket(ref bufferReadValid);
            Player3EquipmentBody      = ReadStringFromPacket(ref bufferReadValid);
            Player3EquipmentArms      = ReadStringFromPacket(ref bufferReadValid);
            Player3EquipmentLegs      = ReadStringFromPacket(ref bufferReadValid);
            Player3Weapon0 = ReadStringFromPacket(ref bufferReadValid);
            Player3Weapon1 = ReadStringFromPacket(ref bufferReadValid);
            Player3Weapon2 = ReadStringFromPacket(ref bufferReadValid);
            Player3Weapon3 = ReadStringFromPacket(ref bufferReadValid);

            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(Player1Taunt);
            WriteStringToPacket(Player2Taunt);
            WriteStringToPacket(Player3Taunt);

            WriteStringToPacket(Player1EquipmentHead);
            WriteStringToPacket(Player1EquipmentShoulder);
            WriteStringToPacket(Player1EquipmentBody);
            WriteStringToPacket(Player1EquipmentArms);
            WriteStringToPacket(Player1EquipmentLegs);
            WriteStringToPacket(Player1Weapon0);
            WriteStringToPacket(Player1Weapon1);
            WriteStringToPacket(Player1Weapon2);
            WriteStringToPacket(Player1Weapon3);

            WriteStringToPacket(Player2EquipmentHead);
            WriteStringToPacket(Player2EquipmentShoulder);
            WriteStringToPacket(Player2EquipmentBody);
            WriteStringToPacket(Player2EquipmentArms);
            WriteStringToPacket(Player2EquipmentLegs);
            WriteStringToPacket(Player2Weapon0);
            WriteStringToPacket(Player2Weapon1);
            WriteStringToPacket(Player2Weapon2);
            WriteStringToPacket(Player2Weapon3);

            WriteStringToPacket(Player3EquipmentHead);
            WriteStringToPacket(Player3EquipmentShoulder);
            WriteStringToPacket(Player3EquipmentBody);
            WriteStringToPacket(Player3EquipmentArms);
            WriteStringToPacket(Player3EquipmentLegs);
            WriteStringToPacket(Player3Weapon0);
            WriteStringToPacket(Player3Weapon1);
            WriteStringToPacket(Player3Weapon2);
            WriteStringToPacket(Player3Weapon3);
        }

        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.None;
        }

        protected override string OnGetLogFormat()
        {
            return $"FromServer.SetMatchMVPCustomization Player1Taunt:{Player1Taunt},Player2Taunt:{Player2Taunt},Player3Taunt:{Player3Taunt}";
        }
    }
}
