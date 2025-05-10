using System.Collections.Generic;
using System.Linq;
using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusCommon.GameModes.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusClient.GameModes.Siege
{
    public static class MPPSiegeMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPSiegeClientBehaviors(string scene)
        {
            MissionState.OpenNew("MPPSiege", new MissionInitializerRecord(scene), delegate (Mission missionController)
            {
                return new MissionBehavior[]
                {
                    MissionLobbyComponent.CreateBehavior(),
                    new MultiplayerWarmupComponent(),
                    new MissionMultiplayerSiegeClient(),
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
                    new MissionScoreboardComponent(new SiegeScoreboardData()),
                    MissionMatchHistoryComponent.CreateIfConditionsAreMet(),
                    new EquipmentControllerLeaveLogic(),
                    new MissionRecentPlayersComponent(),
                    new MultiplayerPreloadHelper()

                };
            }, true, true);
        }

        

    }
}