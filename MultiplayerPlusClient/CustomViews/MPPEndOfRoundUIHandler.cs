using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Multiplayer.View.MissionViews;
using TaleWorlds.MountAndBlade.Multiplayer.ViewModelCollection.EndOfRound;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;
using MultiplayerPlusCommon.ViewModels;
using TaleWorlds.Core;

namespace MultiplayerPlusClient.CustomViews
{
    [OverrideView(typeof(MultiplayerEndOfRoundUIHandler))]
    public class MPPEndOfRoundUIHandler : MissionView
    {
        private IRoundComponent RoundComponent
        {
            get
            {
                return this._mpGameModeBase.RoundComponent;
            }
        }
        public override void OnMissionScreenInitialize()
        {
            base.OnMissionScreenInitialize();
            this._missionLobbyComponent = base.Mission.GetMissionBehavior<MissionLobbyComponent>();
            this._scoreboardComponent = base.Mission.GetMissionBehavior<MissionScoreboardComponent>();
            this._mpGameModeBase = base.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
            this.ViewOrderPriority = 23;
            SpriteData spriteData = UIResourceManager.SpriteData;
            TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
            ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
            this._dataSource = new MPPEndOfRoundVM(this._scoreboardComponent, this._missionLobbyComponent, this.RoundComponent);
            this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
            this._gauntletLayer.LoadMovie("MultiplayerEndOfRound", this._dataSource);
            base.MissionScreen.AddLayer(this._gauntletLayer);
            ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
            if (this.RoundComponent != null)
            {
                this.RoundComponent.OnRoundStarted += this.RoundStarted;
                this._scoreboardComponent.OnRoundPropertiesChanged += this.OnRoundPropertiesChanged;
                this.RoundComponent.OnPostRoundEnded += this.ShowEndOfRoundUI;
                this._scoreboardComponent.OnMVPSelected += this.OnMVPSelected;
            }
            this._missionLobbyComponent.OnPostMatchEnded += this.OnPostMatchEnded;
        }

        public override void OnMissionScreenFinalize()
        {
            base.OnMissionScreenFinalize();
            if (this.RoundComponent != null)
            {
                this.RoundComponent.OnRoundStarted -= this.RoundStarted;
                this._scoreboardComponent.OnRoundPropertiesChanged -= this.OnRoundPropertiesChanged;
                this.RoundComponent.OnPostRoundEnded -= this.ShowEndOfRoundUI;
                this._scoreboardComponent.OnMVPSelected -= this.OnMVPSelected;
            }
            this._missionLobbyComponent.OnPostMatchEnded -= this.OnPostMatchEnded;
            base.MissionScreen.RemoveLayer(this._gauntletLayer);
            this._gauntletLayer = null;
            this._dataSource.OnFinalize();
            this._dataSource = null;
        }
        private void RoundStarted()
        {
            ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
            this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
            this._dataSource.IsShown = false;
        }
        private void OnRoundPropertiesChanged()
        {
            if (this.RoundComponent.RoundCount != 0 && this._missionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending)
            {
                this._dataSource.Refresh();
            }
        }

        private void ShowEndOfRoundUI()
        {
            this.ShowEndOfRoundUI(false);
        }

        private void ShowEndOfRoundUI(bool isForced)
        {
            if (isForced || (this.RoundComponent.RoundCount != 0 && this._missionLobbyComponent.CurrentMultiplayerState != MissionLobbyComponent.MultiplayerGameState.Ending))
            {
                ScreenManager.SetSuspendLayer(this._gauntletLayer, false);
                this._gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Mouse);
                this._dataSource.IsShown = true;
                this._dataSource.ApplyCharacterCosmetics();
                this._dataSource.PlayCustomAnimation();
            }
        }

        private void OnPostMatchEnded()
        {
            ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
            this._dataSource.IsShown = false;
        }

        private void OnMVPSelected(MissionPeer mvpPeer, int mvpCount)
        {
            this._dataSource.OnMVPSelected(mvpPeer);
        }

        public void SetMVPCustomization(
            string playerTaunt,
            string playerEquipmentHead,
            string playerEquipmentShoulder,
            string playerEquipmentBody,
            string playerEquipmentArms,
            string playerEquipmentLegs,
            string playerWeapon0,
            string playerWeapon1,
            string playerWeapon2,
            string playerWeapon3,
            int playerSide
            )
        {
            if(BattleSideEnum.Attacker == (BattleSideEnum)playerSide)
            {
                this._dataSource.AttackerMVPTaunt = playerTaunt;
                this._dataSource.AttackerEquipmentHead = playerEquipmentHead;
                this._dataSource.AttackerEquipmentShoulder = playerEquipmentShoulder;
                this._dataSource.AttackerEquipmentBody = playerEquipmentBody;
                this._dataSource.AttackerEquipmentArms = playerEquipmentArms;
                this._dataSource.AttackerEquipmentLegs = playerEquipmentLegs;
                this._dataSource.AttackerWeapon0 = playerWeapon0;
                this._dataSource.AttackerWeapon1 = playerWeapon1;
                this._dataSource.AttackerWeapon2 = playerWeapon2;
                this._dataSource.AttackerWeapon3 = playerWeapon3;
            }
            else if(BattleSideEnum.Defender == (BattleSideEnum)playerSide)
            {
                this._dataSource.DefenderMVPTaunt = playerTaunt;
                this._dataSource.DefenderEquipmentHead = playerEquipmentHead;
                this._dataSource.DefenderEquipmentShoulder = playerEquipmentShoulder;
                this._dataSource.DefenderEquipmentBody = playerEquipmentBody;
                this._dataSource.DefenderEquipmentArms = playerEquipmentArms;
                this._dataSource.DefenderEquipmentLegs = playerEquipmentLegs;
                this._dataSource.DefenderWeapon0 = playerWeapon0;
                this._dataSource.DefenderWeapon1 = playerWeapon1;
                this._dataSource.DefenderWeapon2 = playerWeapon2;
                this._dataSource.DefenderWeapon3 = playerWeapon3;
            }
        }

        private MPPEndOfRoundVM _dataSource;
        private GauntletLayer _gauntletLayer;
        private MissionLobbyComponent _missionLobbyComponent;
        private MissionScoreboardComponent _scoreboardComponent;
        private MissionMultiplayerGameModeBaseClient _mpGameModeBase;
    }
}
