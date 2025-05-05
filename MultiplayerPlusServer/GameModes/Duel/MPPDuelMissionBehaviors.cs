using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.Duel;
using MultiplayerPlusServer.GameModes.TeamDeathMatch;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer.GameModes.Skirmish
{
    public static class MPPDuelMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPDuelMissionServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPDuel", new MissionInitializerRecord(scene),
                delegate (Mission missionController)
                {
                    return new MissionBehavior[]
                    {
                        MissionLobbyComponent.CreateBehavior(),
                        new MissionMultiplayerDuel(),
                        new MissionMultiplayerGameModeDuelClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPDuelSpawnFrameBehavior(), new MPPDuelSpawningBehavior()),
                        new MissionLobbyEquipmentNetworkComponent(),
                        new MissionHardBorderPlacer(),
                        new MissionBoundaryPlacer(),
                        new MissionBoundaryCrossingHandler(),
                        new MultiplayerPollComponent(),
                        new MultiplayerAdminComponent(),
                        new MultiplayerGameNotificationsComponent(),
                        new MissionOptionsComponent(),
                        new MissionScoreboardComponent(new DuelScoreboardData()),
                        new MissionAgentPanicHandler(),
                        new AgentHumanAILogic(),
                        new EquipmentControllerLeaveLogic(),
                        new MultiplayerPreloadHelper()
                    };
                }, true, true);

        }
    }
}