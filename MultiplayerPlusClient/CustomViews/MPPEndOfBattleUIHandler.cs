using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.Multiplayer.View.MissionViews;
using TaleWorlds.MountAndBlade.Multiplayer.ViewModelCollection;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using MultiplayerPlusCommon.ViewModels;
using MultiplayerPlusCommon.Helpers;

namespace MultiplayerPlusClient.CustomViews
{
    [OverrideView(typeof(MultiplayerEndOfBattleUIHandler))]
    public class MPPEndOfBattleUIHandler : MissionView
    {
        public override void OnMissionScreenInitialize()
        {
            base.OnMissionScreenInitialize();
            this.ViewOrderPriority = 30;
            this._dataSource = new MPPEndOfBattleVM();
            this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
            this._gauntletLayer.LoadMovie("MultiplayerEndOfBattle", this._dataSource);
            this._lobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
            this._lobbyComponent.OnPostMatchEnded += this.OnPostMatchEnded;
            base.MissionScreen.AddLayer(this._gauntletLayer);
        }
        public override void OnMissionScreenFinalize()
        {
            base.OnMissionScreenFinalize();
            this._lobbyComponent.OnPostMatchEnded -= this.OnPostMatchEnded;
        }
        private void OnPostMatchEnded()
        {
            this._dataSource.OnBattleEnded();
        }

        public void SetMVPCustomization(
            string player1Taunt,
            string player1EquipmentHead,
            string player1EquipmentShoulder,
            string player1EquipmentBody,
            string player1EquipmentArms,
            string player1EquipmentLegs,
            string player1Weapon0,
            string player1Weapon1,
            string player1Weapon2,
            string player1Weapon3,
            string player2Taunt,
            string player2EquipmentHead,
            string player2EquipmentShoulder,
            string player2EquipmentBody,
            string player2EquipmentArms,
            string player2EquipmentLegs,
            string player2Weapon0,
            string player2Weapon1,
            string player2Weapon2,
            string player2Weapon3,
            string player3Taunt,
            string player3EquipmentHead,
            string player3EquipmentShoulder,
            string player3EquipmentBody,
            string player3EquipmentArms,
            string player3EquipmentLegs,
            string player3Weapon0,
            string player3Weapon1,
            string player3Weapon2,
            string player3Weapon3
            )
        {
            this._dataSource.Player1Taunt = player1Taunt;
            this._dataSource.Player1EquipmentHead = player1EquipmentHead;
            this._dataSource.Player1EquipmentShoulder = player1EquipmentShoulder;
            this._dataSource.Player1EquipmentBody = player1EquipmentBody;
            this._dataSource.Player1EquipmentArms = player1EquipmentArms;
            this._dataSource.Player1EquipmentLegs = player1EquipmentLegs;
            this._dataSource.Player1Weapon0 = player1Weapon0;
            this._dataSource.Player1Weapon1 = player1Weapon1;
            this._dataSource.Player1Weapon2 = player1Weapon2;
            this._dataSource.Player1Weapon3 = player1Weapon3;
            this._dataSource.Player2Taunt = player2Taunt;
            this._dataSource.Player2EquipmentHead = player2EquipmentHead;
            this._dataSource.Player2EquipmentShoulder = player2EquipmentShoulder;
            this._dataSource.Player2EquipmentBody = player2EquipmentBody;
            this._dataSource.Player2EquipmentArms = player2EquipmentArms;
            this._dataSource.Player2EquipmentLegs = player2EquipmentLegs;
            this._dataSource.Player2Weapon0 = player2Weapon0;
            this._dataSource.Player2Weapon1 = player2Weapon1;
            this._dataSource.Player2Weapon2 = player2Weapon2;
            this._dataSource.Player2Weapon3 = player2Weapon3;
            this._dataSource.Player3Taunt = player3Taunt;
            this._dataSource.Player3EquipmentHead = player3EquipmentHead;
            this._dataSource.Player3EquipmentShoulder = player3EquipmentShoulder;
            this._dataSource.Player3EquipmentBody = player3EquipmentBody;
            this._dataSource.Player3EquipmentArms = player3EquipmentArms;
            this._dataSource.Player3EquipmentLegs = player3EquipmentLegs;
            this._dataSource.Player3Weapon0 = player3Weapon0;
            this._dataSource.Player3Weapon1 = player3Weapon1;
            this._dataSource.Player3Weapon2 = player3Weapon2;
            this._dataSource.Player3Weapon3 = player3Weapon3;
        }

        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);
            this._dataSource.OnTick(dt);
        }

        private MPPEndOfBattleVM _dataSource;
        private GauntletLayer _gauntletLayer;
        private MissionLobbyComponent _lobbyComponent;
    }
}
