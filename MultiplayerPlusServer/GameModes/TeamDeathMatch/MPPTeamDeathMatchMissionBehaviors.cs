using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusServer.GameModes.Common;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer.GameModes.TeamDeathMatch
{
    public static class MPPTeamDeathMatchMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPTeamDeathMatchServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPTeamDeathMatch", new MissionInitializerRecord(scene),
                delegate(Mission missionController)
                {
                    return new MissionBehavior[]
                    {

                        new MPPLobbyComponent(),
                        new MPPTeamDeathMatchBehavior(),
                        new MissionMultiplayerTeamDeathmatchClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPTeamDeathMatchSpawnFrameBehavior(), new MPPTeamDeathMatchSpawningBehavior()),
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
                        new MissionAgentPanicHandler(),
                        new AgentHumanAILogic(),
                        new EquipmentControllerLeaveLogic(),
                        new MultiplayerPreloadHelper(),

                        //Custom Behaviors

                        new TauntBehavior(),
                        new ShoutBehavior()

                    };
                }, true, true);

        }
    }
}