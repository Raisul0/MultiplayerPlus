using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade.Multiplayer.ViewModelCollection;
using TaleWorlds.MountAndBlade;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.ObjectClass;
using MultiplayerPlusCommon.NetworkMessages.FromClient;

namespace MultiplayerPlusCommon.ViewModels
{
    public class MPPEndOfBattleVM : ViewModel
    {
        public MPPEndOfBattleVM()
        {
            this._activeDelay = MissionLobbyComponent.PostMatchWaitDuration / 2f;
            this._gameMode = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
            this.RefreshValues();
        }
        public override void RefreshValues()
        {
            base.RefreshValues();
            this.TitleText = new TextObject("{=GPfkMajw}Battle Ended", null).ToString();
            this.DescriptionText = new TextObject("{=ADPaaX8R}Best Players of This Battle", null).ToString();
        }
        public void OnTick(float dt)
        {
            if (this._isBattleEnded)
            {
                this._activateTimeElapsed += dt;
                if (this._activateTimeElapsed >= this._activeDelay)
                {
                    this._isBattleEnded = false;
                    this.OnEnabled();
                }
            }
        }
        private void OnEnabled()
        {
            var firstPlayer = "";
            var secondPlayer = "";
            var thirdPlayer = "";
            MissionScoreboardComponent missionBehavior = Mission.Current.GetMissionBehavior<MissionScoreboardComponent>();
            List<MissionPeer> list = new List<MissionPeer>();
            foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in from s in missionBehavior.Sides
                                                                                               where s != null && s.Side != BattleSideEnum.None
                                                                                               select s)
            {
                foreach (MissionPeer item in missionScoreboardSide.Players)
                {
                    list.Add(item);
                }
            }
            list.Sort((MissionPeer p1, MissionPeer p2) => this.GetPeerScore(p2).CompareTo(this.GetPeerScore(p1)));
            if (list.Count > 0)
            {
                this.HasFirstPlace = true;
                MissionPeer peer = list[0];
                this.FirstPlacePlayer = new MPEndOfBattlePlayerVM(peer, this.GetPeerScore(peer), 1);
                firstPlayer = peer.Name;
            }
            if (list.Count > 1)
            {
                this.HasSecondPlace = true;
                MissionPeer peer2 = list[1];
                this.SecondPlacePlayer = new MPEndOfBattlePlayerVM(peer2, this.GetPeerScore(peer2), 2);
                secondPlayer = peer2.Name;
            }
            if (list.Count > 2)
            {
                this.HasThirdPlace = true;
                MissionPeer peer3 = list[2];
                this.ThirdPlacePlayer = new MPEndOfBattlePlayerVM(peer3, this.GetPeerScore(peer3), 3);
                thirdPlayer = peer3.Name;
            }

            ApplyCharacterCosmetics();
            PlayCustomAnimation();

            this.IsEnabled = true;
        }

        private void ApplyCharacterCosmetics()
        {
            if (FirstPlacePlayer != null)
            {
                SetMVPEquipment(FirstPlacePlayer,EquipmentIndex.Head,  Player1EquipmentHead);
                SetMVPEquipment(FirstPlacePlayer,EquipmentIndex.Cape,  Player1EquipmentShoulder);
                SetMVPEquipment(FirstPlacePlayer,EquipmentIndex.Body,  Player1EquipmentBody);
                SetMVPEquipment(FirstPlacePlayer,EquipmentIndex.Gloves,Player1EquipmentArms);
                SetMVPEquipment(FirstPlacePlayer,EquipmentIndex.Leg,   Player1EquipmentLegs);
                SetMVPEquipment(FirstPlacePlayer, EquipmentIndex.Weapon0, Player1Weapon0);
                SetMVPEquipment(FirstPlacePlayer, EquipmentIndex.Weapon1, Player1Weapon1);
                SetMVPEquipment(FirstPlacePlayer, EquipmentIndex.Weapon2, Player1Weapon2);
                SetMVPEquipment(FirstPlacePlayer, EquipmentIndex.Weapon3, Player1Weapon3);
                FirstPlacePlayer.Preview.HeroVisual.ExecuteEquipWeaponAtIndex(EquipmentIndex.Weapon1,false);
            }

            if (SecondPlacePlayer != null)
            {
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Head, Player2EquipmentHead);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Cape, Player2EquipmentShoulder);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Body, Player2EquipmentBody);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Gloves, Player2EquipmentArms);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Leg, Player2EquipmentLegs);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Weapon0, Player2Weapon0);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Weapon1, Player2Weapon1);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Weapon2, Player2Weapon2);
                SetMVPEquipment(SecondPlacePlayer, EquipmentIndex.Weapon3, Player2Weapon3);
                SecondPlacePlayer.Preview.HeroVisual.ExecuteEquipWeaponAtIndex(EquipmentIndex.Weapon1, false);
            }

            if (ThirdPlacePlayer != null)
            {
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Head, Player3EquipmentHead);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Cape, Player3EquipmentShoulder);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Body, Player3EquipmentBody);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Gloves, Player3EquipmentArms);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Leg, Player3EquipmentLegs);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Weapon0, Player3Weapon0);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Weapon1, Player3Weapon1);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Weapon2, Player3Weapon2);
                SetMVPEquipment(ThirdPlacePlayer, EquipmentIndex.Weapon3, Player3Weapon3);
                ThirdPlacePlayer.Preview.HeroVisual.ExecuteEquipWeaponAtIndex(EquipmentIndex.Weapon1, false);
            }
        }

        private void SetMVPEquipment(MPEndOfBattlePlayerVM mvpPlayer, EquipmentIndex equipmentIndex, string itemId)
        {
            if (mvpPlayer != null)
            {
                if (!string.IsNullOrEmpty(itemId))
                {
                    var equipmentElement = new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(itemId));
                    mvpPlayer.Preview.HeroVisual.SetEquipment(equipmentIndex, equipmentElement);
                }
            }
        }

        private void PlayCustomAnimation()
        {
            if (FirstPlacePlayer != null)
            {
                FirstPlacePlayer.Preview.HeroVisual.ExecuteStartCustomAnimation(Player1Taunt);
            }

            if (SecondPlacePlayer != null)
            {
                SecondPlacePlayer.Preview.HeroVisual.ExecuteStartCustomAnimation(Player2Taunt);
            }

            if (ThirdPlacePlayer != null)
            {
                ThirdPlacePlayer.Preview.HeroVisual.ExecuteStartCustomAnimation(Player3Taunt);
            }
        }
        public void OnBattleEnded()
        {
            this._isBattleEnded = true;
        }
        private int GetPeerScore(MissionPeer peer)
        {
            if (peer == null)
            {
                return 0;
            }
            if (this._gameMode.GameType != MultiplayerGameType.Duel)
            {
                return peer.Score;
            }
            DuelMissionRepresentative component = peer.GetComponent<DuelMissionRepresentative>();
            if (component == null)
            {
                return 0;
            }
            return component.Score;
        }

        [DataSourceProperty]
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                if (value != this._isEnabled)
                {
                    this._isEnabled = value;
                    base.OnPropertyChangedWithValue(value, "IsEnabled");
                }
            }
        }

        [DataSourceProperty]
        public bool HasFirstPlace
        {
            get
            {
                return this._hasFirstPlace;
            }
            set
            {
                if (value != this._hasFirstPlace)
                {
                    this._hasFirstPlace = value;
                    base.OnPropertyChangedWithValue(value, "HasFirstPlace");
                }
            }
        }

        [DataSourceProperty]
        public bool HasSecondPlace
        {
            get
            {
                return this._hasSecondPlace;
            }
            set
            {
                if (value != this._hasSecondPlace)
                {
                    this._hasSecondPlace = value;
                    base.OnPropertyChangedWithValue(value, "HasSecondPlace");
                }
            }
        }

        [DataSourceProperty]
        public bool HasThirdPlace
        {
            get
            {
                return this._hasThirdPlace;
            }
            set
            {
                if (value != this._hasThirdPlace)
                {
                    this._hasThirdPlace = value;
                    base.OnPropertyChangedWithValue(value, "HasThirdPlace");
                }
            }
        }

        [DataSourceProperty]
        public string TitleText
        {
            get
            {
                return this._titleText;
            }
            set
            {
                if (value != this._titleText)
                {
                    this._titleText = value;
                    base.OnPropertyChangedWithValue<string>(value, "TitleText");
                }
            }
        }

        [DataSourceProperty]
        public string DescriptionText
        {
            get
            {
                return this._descriptionText;
            }
            set
            {
                if (value != this._descriptionText)
                {
                    this._descriptionText = value;
                    base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
                }
            }
        }

        [DataSourceProperty]
        public MPEndOfBattlePlayerVM FirstPlacePlayer
        {
            get
            {
                return this._firstPlacePlayer;
            }
            set
            {
                if (value != this._firstPlacePlayer)
                {
                    this._firstPlacePlayer = value;
                    base.OnPropertyChangedWithValue<MPEndOfBattlePlayerVM>(value, "FirstPlacePlayer");
                }
            }
        }

        [DataSourceProperty]
        public MPEndOfBattlePlayerVM SecondPlacePlayer
        {
            get
            {
                return this._secondPlacePlayer;
            }
            set
            {
                if (value != this._secondPlacePlayer)
                {
                    this._secondPlacePlayer = value;
                    base.OnPropertyChangedWithValue<MPEndOfBattlePlayerVM>(value, "SecondPlacePlayer");
                }
            }
        }

        [DataSourceProperty]
        public MPEndOfBattlePlayerVM ThirdPlacePlayer
        {
            get
            {
                return this._thirdPlacePlayer;
            }
            set
            {
                if (value != this._thirdPlacePlayer)
                {
                    this._thirdPlacePlayer = value;
                    base.OnPropertyChangedWithValue<MPEndOfBattlePlayerVM>(value, "ThirdPlacePlayer");
                }
            }
        }

        private readonly MissionMultiplayerGameModeBaseClient _gameMode;
        private readonly float _activeDelay;
        private bool _isBattleEnded;
        private float _activateTimeElapsed;
        private bool _isEnabled;
        private bool _hasFirstPlace;
        private bool _hasSecondPlace;
        private bool _hasThirdPlace;
        private string _titleText;
        private string _descriptionText;
        private MPEndOfBattlePlayerVM _firstPlacePlayer;
        private MPEndOfBattlePlayerVM _secondPlacePlayer;
        private MPEndOfBattlePlayerVM _thirdPlacePlayer;

        public string Player1Taunt              { get;  set; }
        public string Player1EquipmentHead      { get;  set; }
        public string Player1EquipmentShoulder  { get;  set; }
        public string Player1EquipmentBody      { get;  set; }
        public string Player1EquipmentArms      { get;  set; }
        public string Player1EquipmentLegs      { get;  set; }
        public string Player1Weapon0 { get; set; }
        public string Player1Weapon1 { get; set; }
        public string Player1Weapon2 { get; set; }
        public string Player1Weapon3 { get; set; }
        public string Player2Taunt              { get;  set; }
        public string Player2EquipmentHead      { get;  set; }
        public string Player2EquipmentShoulder  { get;  set; }
        public string Player2EquipmentBody      { get;  set; }
        public string Player2EquipmentArms      { get;  set; }
        public string Player2EquipmentLegs      { get;  set; }
        public string Player2Weapon0 { get; set; }
        public string Player2Weapon1 { get; set; }
        public string Player2Weapon2 { get; set; }
        public string Player2Weapon3 { get; set; }
        public string Player3Taunt              { get;  set; }
        public string Player3EquipmentHead      { get;  set; }
        public string Player3EquipmentShoulder  { get;  set; }
        public string Player3EquipmentBody      { get;  set; }
        public string Player3EquipmentArms      { get;  set; }
        public string Player3EquipmentLegs      { get;  set; }
        public string Player3Weapon0 { get; set; }
        public string Player3Weapon1 { get; set; }
        public string Player3Weapon2 { get; set; }
        public string Player3Weapon3 { get; set; }
    }
}
