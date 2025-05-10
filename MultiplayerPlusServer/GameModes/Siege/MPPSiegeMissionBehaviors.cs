using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.Siege;
using MultiplayerPlusServer.GameModes.TeamDeathMatch;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer.GameModes.Siege
{
    public static class MPPSiegeMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPSiegeMissionServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPSiege", new MissionInitializerRecord(scene),
                delegate (Mission missionController)
                {
                    return new MissionBehavior[]
                    {
                        MissionLobbyComponent.CreateBehavior(),
                        new MPPSiegeBehavior(),
                        new MultiplayerWarmupComponent(),
                        new MissionMultiplayerSiegeClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPSiegeSpawnFrameBehavior(), new MPPSiegeSpawningBehavior()),
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
                        new MissionAgentPanicHandler(),
                        new AgentHumanAILogic(),
                        new EquipmentControllerLeaveLogic(),
                        new MultiplayerPreloadHelper()

                    };
                }, true, true);

        }
    }
}