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

        public void SetMVPAnimation(string player1Taunt,string player2Taunt,string player3Taunt)
        {
            this._dataSource.Player1Taunt = player1Taunt;
            this._dataSource.Player2Taunt = player2Taunt;
            this._dataSource.Player3Taunt = player3Taunt;
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
