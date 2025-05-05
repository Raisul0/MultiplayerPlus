using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.TeamDeathMatch;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusServer.GameModes.Skirmish
{
    public static class MPPSkirmishMissionBehaviors
    {
        [MissionMethod]
        public static void OpenMPPSkirmishMissionServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPSkirmish", new MissionInitializerRecord(scene),
                delegate (Mission missionController)
                {
                    return new MissionBehavior[]
                    {
                        MissionLobbyComponent.CreateBehavior(),
                        new MPPSkirmishBehavior(),
                        new MultiplayerRoundController(),
                        new MultiplayerWarmupComponent(),
                        new MissionMultiplayerGameModeFlagDominationClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPSkirmishSpawnFrameBehavior(), new MPPSkirmishSpawningBehavior()),
                        new MissionLobbyEquipmentNetworkComponent(),
                        new MultiplayerTeamSelectComponent(),
                        new MissionHardBorderPlacer(),
                        new MissionBoundaryPlacer(),
                        new AgentVictoryLogic(),
                        new MissionAgentPanicHandler(),
                        new AgentHumanAILogic(),
                        new MissionBoundaryCrossingHandler(),
                        new MultiplayerPollComponent(),
                        new MultiplayerAdminComponent(),
                        new MultiplayerGameNotificationsComponent(),
                        new MissionOptionsComponent(),
                        new MissionScoreboardComponent(new SkirmishScoreboardData()),
                        new EquipmentControllerLeaveLogic(),
                        new VoiceChatHandler(),
                        new MultiplayerPreloadHelper(),
                    };
                }, true, true);

        }
    }
}