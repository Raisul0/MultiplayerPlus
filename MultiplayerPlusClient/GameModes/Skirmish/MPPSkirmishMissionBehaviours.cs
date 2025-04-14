using System.Collections.Generic;
using System.Linq;
using MultiplayerPlusCommon;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusCommon.GameModes.Skirmish;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusClient.GameModes.Skirmish
{
    public static class MPPSkirmishMissionBehaviours
    {
        [MissionMethod]
        public static void OpenMPPSkirmishClientBehaviours(string scene)
        {
            MissionState.OpenNew("MPPSkirmish", new MissionInitializerRecord(scene), delegate (Mission missionController)
            {
                return new MissionBehavior[]
                {
                    MissionLobbyComponent.CreateBehavior(),
                    new MultiplayerAchievementComponent(),
                    new MissionMultiplayerGameModeFlagDominationClient(),
                    new MultiplayerRoundComponent(),
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
                    new MissionScoreboardComponent(new SkirmishScoreboardData()),
                    MissionMatchHistoryComponent.CreateIfConditionsAreMet(),
                    new EquipmentControllerLeaveLogic(),
                    new MissionRecentPlayersComponent(),
                    new VoiceChatHandler(),
                    new MultiplayerPreloadHelper()

                };
            }, true, true);
        }

        

    }
}