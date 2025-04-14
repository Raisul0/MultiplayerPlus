using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.GameModes.TeamDeathMatch
{
    public class MPPTeamDeathMatchCommonBehavior : MissionMultiplayerGameModeBaseClient
    {
        public override bool IsGameModeUsingGold
        {
            get
            {
                return true;
            }
        }

        public override bool IsGameModeTactical
        {
            get
            {
                return false;
            }
        }

        public override bool IsGameModeUsingRoundCountdown
        {
            get
            {
                return false;
            }
        }

        public override MultiplayerGameType GameType
        {
            get
            {
                return MultiplayerGameType.TeamDeathmatch;
            }
        }

        public override int GetGoldAmount()
        {
            return 2000;
        }

        public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
        {
        }

        public override void AfterStart()
        {
            Mission.SetMissionMode(MissionMode.Battle, true);
        }

        public void OnBotsControlledChanged(MissionPeer component, int aliveCount, int totalCount)
        {
        }

        public MPPTeamDeathMatchCommonBehavior()
        {
        }
    }
}