using System.Collections.Generic;
using System.Linq;
using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Behaviors;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusClient.GameModes.TeamDeathMatch
{
    public static class MPPTeamDeathMatchMissionBehaviours
    {
        [MissionMethod]
        public static void OpenMPPTeamDeathMatchClientBehaviours(string scene)
        {
            MissionState.OpenNew("MPPTeamDeathMatch", new MissionInitializerRecord(scene), delegate (Mission missionController)
            {
                return new MissionBehavior[]
                {

					MissionLobbyComponent.CreateBehavior(),
                    new MissionMultiplayerTeamDeathmatchClient(),
                    new MultiplayerAchievementComponent(),
                    new MultiplayerTimerComponent(),
                    new MultiplayerMissionAgentVisualSpawnComponent(),
                    new ConsoleMatchStartEndHandler(),
                    new MissionLobbyEquipmentNetworkComponent(),
                    new MultiplayerTeamSelectComponent(),
                    new MissionHardBorderPlacer(),
                    new MissionBoundaryPlacer(),
                    new MissionBoundaryCrossingHandler(),
                    new MultiplayerPollComponent(),
                    new MultiplayerAdminComponent(),
                    new MultiplayerGameNotificationsComponent(),
                    new MissionOptionsComponent(),
                    new MissionScoreboardComponent(new TDMScoreboardData()),
                    MissionMatchHistoryComponent.CreateIfConditionsAreMet(),
                    new EquipmentControllerLeaveLogic(),
                    new MissionRecentPlayersComponent(),
                    new MultiplayerPreloadHelper(),
                    new TauntBehavior(),
                    new ShoutBehavior()

                };
            }, true, true);
        }

        

    }
}