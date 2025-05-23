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
    public sealed class SetRoundMVPCustomization : GameNetworkMessage
    {
        public string PlayerTaunt { get; private set; }
        public string PlayerEquipmentHead { get; private set; }
        public string PlayerEquipmentShoulder { get; private set; }
        public string PlayerEquipmentBody { get; private set; }
        public string PlayerEquipmentArms { get; private set; }
        public string PlayerEquipmentLegs { get; private set; }
        public string PlayerWeapon0 { get; private set; }
        public string PlayerWeapon1 { get; private set; }
        public string PlayerWeapon2 { get; private set; }
        public string PlayerWeapon3 { get; private set; }
        public int PlayerSide { get; set; }


        public SetRoundMVPCustomization(string playerTaunt,Equipment player, BattleSideEnum side)
        {
            PlayerTaunt = playerTaunt;
            PlayerEquipmentHead     = player[EquipmentIndex.Head].Item?.StringId ?? "";
            PlayerEquipmentShoulder = player[EquipmentIndex.Cape].Item?.StringId ?? "";
            PlayerEquipmentBody     = player[EquipmentIndex.Body].Item?.StringId ?? "";
            PlayerEquipmentArms     = player[EquipmentIndex.Gloves].Item?.StringId ?? "";
            PlayerEquipmentLegs     = player[EquipmentIndex.Leg].Item?.StringId ?? "";
            PlayerWeapon0 = player[EquipmentIndex.Weapon0].Item?.StringId ?? "";
            PlayerWeapon1 = player[EquipmentIndex.Weapon1].Item?.StringId ?? "";
            PlayerWeapon2 = player[EquipmentIndex.Weapon2].Item?.StringId ?? "";
            PlayerWeapon3 = player[EquipmentIndex.Weapon3].Item?.StringId ?? "";
            PlayerSide = (int)side;
        }

        public SetRoundMVPCustomization()
        {
        }

        protected override bool OnRead()
        {
            bool bufferReadValid = true;

            PlayerTaunt              = ReadStringFromPacket(ref bufferReadValid);
            PlayerEquipmentHead      = ReadStringFromPacket(ref bufferReadValid);
            PlayerEquipmentShoulder  = ReadStringFromPacket(ref bufferReadValid);
            PlayerEquipmentBody      = ReadStringFromPacket(ref bufferReadValid);
            PlayerEquipmentArms      = ReadStringFromPacket(ref bufferReadValid);
            PlayerEquipmentLegs      = ReadStringFromPacket(ref bufferReadValid);
            PlayerWeapon0 = ReadStringFromPacket(ref bufferReadValid);
            PlayerWeapon1 = ReadStringFromPacket(ref bufferReadValid);
            PlayerWeapon2 = ReadStringFromPacket(ref bufferReadValid);
            PlayerWeapon3 = ReadStringFromPacket(ref bufferReadValid);
            PlayerSide               = ReadIntFromPacket(new CompressionInfo.Integer(0,1), ref bufferReadValid);

            return bufferReadValid;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(PlayerTaunt);
            WriteStringToPacket(PlayerEquipmentHead);
            WriteStringToPacket(PlayerEquipmentShoulder);
            WriteStringToPacket(PlayerEquipmentBody);
            WriteStringToPacket(PlayerEquipmentArms);
            WriteStringToPacket(PlayerEquipmentLegs);
            WriteStringToPacket(PlayerWeapon0);
            WriteStringToPacket(PlayerWeapon1);
            WriteStringToPacket(PlayerWeapon2);
            WriteStringToPacket(PlayerWeapon3);
            WriteIntToPacket(PlayerSide, new CompressionInfo.Integer(0, 1));
        }

        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.None;
        }

        protected override string OnGetLogFormat()
        {
            return $"FromServer.SetRoundMVPCustomization";
        }
    }
}
