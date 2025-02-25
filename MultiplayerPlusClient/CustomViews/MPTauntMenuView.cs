using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ViewModels;
using NetworkMessages.FromClient;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using TaleWorlds.TwoDimension;

namespace MultiplayerPlusClient.CustomViews
{
    [DefaultView]
    public class MPTauntMenuView : MissionView
    {
        private bool complete = false;
        private bool _visiable = false;
        private SpriteCategory spriteCategory;
        // Token: 0x0400037C RID: 892
        private List<string> spriteCategoryNames = new List<string>();

        int ViewOrderPriority = 100;

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
            this._dataSource = new MPTauntMenuVM();
            this.LoadSpriteCategories();
        }

        private void LoadSpriteCategories()
        {
            this.spriteCategoryNames.Add("ui_taunt_sprits");
            SpriteData spriteData = UIResourceManager.SpriteData;
            TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
            ResourceDepot resourceDepot = UIResourceManager.UIResourceDepot;
            foreach (string categoryName in this.spriteCategoryNames)
            {
                this.spriteCategory = spriteData.SpriteCategories[categoryName];
                this.spriteCategory.Load(resourceContext, resourceDepot);
            }
        }

        public void Show()
        {
            if (!_dataSource.Populated) _dataSource.SetSlots();
            _movie = _gauntletLayer.LoadMovie(MPMovies.BannerRoyalTaunt, this._dataSource);
            MissionScreen.AddLayer(this._gauntletLayer);
            _gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
            _visiable = true;
        }

        public void Hide()
        {
            _gauntletLayer.ReleaseMovie(_movie);
            _dataSource.ExecuteTaunt();
            _gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
            _visiable = false;
        }

        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);
            if (this.IsMainAgentAvailable() && base.Mission.IsMainAgentItemInteractionEnabled)
            {
                if (Input.IsKeyDown(TaleWorlds.InputSystem.InputKey.M))
                {
                    var shoutView = Mission.GetMissionBehavior<MPShoutMenuView>();
                    if (!shoutView.IsVisiable())
                    {
                        if (!_visiable)
                        {
                            Show();
                        }
                    }
                    else
                    {
                        GameNetwork.WriteMessage(new ServerMessage("Cannot open taunt wheel while shout wheel is open", false));
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
        private MPTauntMenuVM _dataSource;
    }
}
