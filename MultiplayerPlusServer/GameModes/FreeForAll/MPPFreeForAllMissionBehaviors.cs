using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.Common;
using MultiplayerPlusServer.GameModes.FreeForAll;
using MultiplayerPlusServer.GameModes.TeamDeathMatch;
using MultiplayerPlusServer.GameModes.Warmup;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer.GameModes.FreeForAll
{
    public static class MPPFreeForAllMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPFreeForAllMissionServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPFreeForAll", new MissionInitializerRecord(scene),
                delegate (Mission missionController)
                {
                    return new MissionBehavior[]
                    {
                        new MPPLobbyComponent(),
                        new MPPFreeForAllBehavior(),
                        new MissionMultiplayerFFAClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPFFASpawnFrameBehavior(), new MPPWarmupSpawningBehavior()),
                        new MissionLobbyEquipmentNetworkComponent(),
                        new MultiplayerTeamSelectComponent(),
                        new MissionHardBorderPlacer(),
                        new MissionBoundaryPlacer(),
                        new MissionBoundaryCrossingHandler(),
                        new MultiplayerPollComponent(),
                        new MultiplayerAdminComponent(),
                        new MultiplayerGameNotificationsComponent(),
                        new MissionOptionsComponent(),
                        new MissionScoreboardComponent(new FFAScoreboardData()),
                        new MissionAgentPanicHandler(),
                        new AgentHumanAILogic(),
                        new EquipmentControllerLeaveLogic(),
                        new MultiplayerPreloadHelper()
                    };
                }, true, true);

        }
    }
}