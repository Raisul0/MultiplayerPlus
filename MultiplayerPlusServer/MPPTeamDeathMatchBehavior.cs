
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;
using MathF = TaleWorlds.Library.MathF;

namespace MultiplayerPlusServer
{
    public class MPPTeamDeathMatchBehavior : MissionMultiplayerGameModeBase
    {
        const float DAMAGE_TICK_DELAY = 2f;
        const float ZONE_RADIUS_LEEWAY = 4f;
        const float WARNING_INTERVAL = 10f;

        private Dictionary<MissionPeer, float> _playerWarningTimestamps = new Dictionary<MissionPeer, float>();
        private bool _zoneInitialized;
        private bool _gameEnded;
        private bool _spawnStarted;
        private float _damageTick;

        public override bool IsGameModeHidingAllAgentVisuals
        {
            get
            {
                return true;
            }
        }

        public override bool IsGameModeUsingOpposingTeams
        {
            get
            {
                return false;
            }
        }

        public override MultiplayerGameType GetMissionType()
        {
            return MultiplayerGameType.TeamDeathmatch;
        }

        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();

        }

        public override void AfterStart()
        {
            string attackerCultureStringId = MultiplayerOptions.OptionType.CultureTeam1.GetStrValue();
            string defenderCultureStringId = MultiplayerOptions.OptionType.CultureTeam2.GetStrValue();

            var attackerCulture = MBObjectManager.Instance.GetObject<BasicCultureObject>(attackerCultureStringId);
            var defenderCulture = MBObjectManager.Instance.GetObject<BasicCultureObject>(defenderCultureStringId);

            var bannerTeamOne = new Banner(attackerCulture.BannerKey, attackerCulture.BackgroundColor1, attackerCulture.ForegroundColor1);
            var bannerTeamTwo = new Banner(defenderCulture.BannerKey, defenderCulture.BackgroundColor2, defenderCulture.ForegroundColor2);

            Mission.Teams.Add(BattleSideEnum.Attacker, attackerCulture.BackgroundColor1, attackerCulture.ForegroundColor1, bannerTeamOne);
            Mission.Teams.Add(BattleSideEnum.Defender, defenderCulture.BackgroundColor2, defenderCulture.ForegroundColor2, bannerTeamTwo);
        }

        protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
        {

        }
        public override void OnMissionTick(float dt)
        {
            base.OnMissionTick(dt);
        }



        public MPPTeamDeathMatchBehavior()
        {
        }
    }
}
