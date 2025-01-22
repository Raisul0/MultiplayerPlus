using MultiplayerPlusCommon;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer
{
    public static class MPPTeamDeathMatchMissionBehaviours
    {
        [MissionMethod]
        public static void OpenMPPTeamDeathMatchServerBehaviours(string scene)
        {
            MissionState.OpenNew("MPPTeamDeathMatch", new MissionInitializerRecord(scene),
                delegate(Mission missionController)
                {
                    return new MissionBehavior[]
                    {

                        //new MPPTeamDeathMatchLobbyComponent(),
                        //new MPPTeamDeathMatchBehavior(),
                        //new MPPTeamDeathMatchCommonBehavior(),

                        //new MultiplayerTimerComponent(),
                        //new MultiplayerTeamSelectComponent(),
                        //new SpawnComponent(new MPPTeamDeathMatchSpawnFrameBehavior(),
                        //new MPPTeamDeathMatchSpawningBehavior()),

                        //new AgentHumanAILogic(),
                        //new MissionLobbyEquipmentNetworkComponent(),
                        //new MissionHardBorderPlacer(),
                        //new MissionBoundaryPlacer(),
                        //new MissionBoundaryCrossingHandler(),
                        //new MultiplayerPollComponent(),
                        //new MultiplayerGameNotificationsComponent(),
                        //new MissionOptionsComponent(),
                        //new MissionScoreboardComponent(new TDMScoreboardData()),

                        
                        new MissionMultiplayerTeamDeathmatch(),
                        new MissionMultiplayerTeamDeathmatchClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new TeamDeathmatchSpawnFrameBehavior(), new TeamDeathmatchSpawningBehavior()),
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
                        new MultiplayerPreloadHelper()

                    };
                }, true, true);

        }
    }
}