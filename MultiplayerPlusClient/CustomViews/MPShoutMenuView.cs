using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ObjectClass;
using MultiplayerPlusCommon.ViewModels;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace MultiplayerPlusClient.CustomViews
{
    [DefaultView]
    public class MPShoutMenuView : MissionView
    {
        private bool complete = false;
        private bool _visiable = false;

        int ViewOrderPriority = 101;

        public override void OnMissionScreenInitialize()
        {
            base.OnMissionScreenInitialize();
        }

        public bool IsVisiable()
        {
            return _visiable;
        }

        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();
            _gauntletLayer = new GauntletLayer(ViewOrderPriority);
            this._dataSource = new MPShoutMenuVM();

        }

        public void UpdateShoutSlots(List<MPShout> shouts)
        {
            this._dataSource.PopulateShoutSlots(shouts);
        }
        public void Show()
        {
            if (!_dataSource.Populated) _dataSource.SetSlots();
            _movie = _gauntletLayer.LoadMovie(MPMovies.BannerRoyalShout, this._dataSource);
            MissionScreen.AddLayer(this._gauntletLayer);
            _gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
            _visiable = true;
        }

        public void Hide()
        {
            _gauntletLayer.ReleaseMovie(_movie);
            _dataSource.ExecuteShout();
            _gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
            _visiable = false;
        }

        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);
            if (this.IsMainAgentAvailable() && base.Mission.IsMainAgentItemInteractionEnabled)
            {
                if (Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.N))
                {
                    var tauntView = Mission.GetMissionBehavior<MPTauntMenuView>();
                    if (!tauntView.IsVisiable())
                    {
                        if (!_visiable)
                        {
                            Show();
                        }
                    }
                    else
                    {
                        GameNetwork.WriteMessage(new ServerMessage("Cannot open shout wheel while taunt wheel is open", false));
                    }
                    
                }
                else
                {
                    if (_visiable)
                    {
                        Hide();
                    }

                }

                return;
            }
        }


        private bool IsMainAgentAvailable()
        {
            Agent main = GameNetwork.MyPeer.ControlledAgent;
            return main != null && main.IsActive();
        }


        private GauntletLayer _gauntletLayer;
        private IGauntletMovie _movie;
        private MPShoutMenuVM _dataSource;
    }
}
