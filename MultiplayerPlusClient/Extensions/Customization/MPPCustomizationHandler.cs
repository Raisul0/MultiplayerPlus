using MultiplayerPlusClient.CustomViews;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusClient.Extensions.Customization
{
    public class MPPCustomizationHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SetMatchMVPCustomization>(SetPlayerMatchMVPCustomization);
            reg.Register<SetRoundMVPCustomization>(SetPlayerRoundMVPCustomization);
        }

        public void SetPlayerMatchMVPCustomization(SetMatchMVPCustomization setMatchMVP)
        {
            var endOfBattleUIHandler = Mission.Current.GetMissionBehavior<MPPEndOfBattleUIHandler>();
            endOfBattleUIHandler.SetMVPCustomization(
                    setMatchMVP.Player1Taunt,
                    setMatchMVP.Player1EquipmentHead,
                    setMatchMVP.Player1EquipmentShoulder,
                    setMatchMVP.Player1EquipmentBody,
                    setMatchMVP.Player1EquipmentArms,
                    setMatchMVP.Player1EquipmentLegs,
                    setMatchMVP.Player1Weapon0,
                    setMatchMVP.Player1Weapon1,
                    setMatchMVP.Player1Weapon2,
                    setMatchMVP.Player1Weapon3,
                    setMatchMVP.Player2Taunt,
                    setMatchMVP.Player2EquipmentHead,
                    setMatchMVP.Player2EquipmentShoulder,
                    setMatchMVP.Player2EquipmentBody,
                    setMatchMVP.Player2EquipmentArms,
                    setMatchMVP.Player2EquipmentLegs,
                    setMatchMVP.Player2Weapon0,
                    setMatchMVP.Player2Weapon1,
                    setMatchMVP.Player2Weapon2,
                    setMatchMVP.Player2Weapon3,
                    setMatchMVP.Player3Taunt,
                    setMatchMVP.Player3EquipmentHead,
                    setMatchMVP.Player3EquipmentShoulder,
                    setMatchMVP.Player3EquipmentBody,
                    setMatchMVP.Player3EquipmentArms,
                    setMatchMVP.Player3EquipmentLegs,
                    setMatchMVP.Player3Weapon0,
                    setMatchMVP.Player3Weapon1,
                    setMatchMVP.Player3Weapon2,
                    setMatchMVP.Player3Weapon3
                    );
        }

        public void SetPlayerRoundMVPCustomization(SetRoundMVPCustomization setRoundMVP)
        {
            var endOfRoundUIHandler = Mission.Current.GetMissionBehavior<MPPEndOfRoundUIHandler>();
            endOfRoundUIHandler.SetMVPCustomization(
                    setRoundMVP.PlayerTaunt,
                    setRoundMVP.PlayerEquipmentHead,
                    setRoundMVP.PlayerEquipmentShoulder,
                    setRoundMVP.PlayerEquipmentBody,
                    setRoundMVP.PlayerEquipmentArms,
                    setRoundMVP.PlayerEquipmentLegs,
                    setRoundMVP.PlayerWeapon0,
                    setRoundMVP.PlayerWeapon1,
                    setRoundMVP.PlayerWeapon2,
                    setRoundMVP.PlayerWeapon3,
                    setRoundMVP.PlayerSide
                );
        }
    }
}
