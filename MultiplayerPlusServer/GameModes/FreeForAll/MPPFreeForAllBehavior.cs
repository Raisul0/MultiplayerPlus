using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using MultiplayerPlusCommon.MPPLoadout;
using MultiplayerPlusCommon.NetworkMessages.FromServer;

namespace MultiplayerPlusServer.GameModes.FreeForAll
{
    public class MPPFreeForAllBehavior : MissionMultiplayerGameModeBase
    {
        public override bool IsGameModeHidingAllAgentVisuals => true;

        public override bool IsGameModeUsingOpposingTeams => false;

        public override MultiplayerGameType GetMissionType()
        {
            return MultiplayerGameType.FreeForAll;
        }

        public override void AfterStart()
        {
            BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue());
            Banner banner = new Banner(@object.BannerKey, @object.BackgroundColor1, @object.ForegroundColor1);
            Team team = base.Mission.Teams.Add(BattleSideEnum.Attacker, @object.BackgroundColor1, @object.ForegroundColor1, banner, isPlayerGeneral: false);
            team.SetIsEnemyOf(team, isEnemyOf: true);
        }

        protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
        {
            networkPeer.AddComponent<FFAMissionRepresentative>();
            MPPLoadout.LoadMPPLoadout(networkPeer);
            if (GameNetwork.IsServer)
            {
                GameNetwork.BeginModuleEventAsServer(networkPeer);
                GameNetwork.WriteMessage(new SetPlayerId(networkPeer.PlayerConnectionInfo.PlayerID.ToString()));
                GameNetwork.EndModuleEventAsServer();
            }
        }

        protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
        {
            MissionPeer component = networkPeer.GetComponent<MissionPeer>();
            component.Team = base.Mission.AttackerTeam;
            component.Culture = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue());
        }
    }
}
