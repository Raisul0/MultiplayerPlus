
using AdimiToolsShared;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace MultiplayerPlusServer.GameModes.Duel;

internal class DrDuelMissionBehavior : MissionBasedMultiplayerGameMode
{
    public const string GameName = "Duel";

    public DrDuelMissionBehavior()
        : base(GameName)
    {
    }

    public static void OpenDrDuelServerMissionBehaviors(string scene)
    {
        MissionState.OpenNew("DrDuel", new MissionInitializerRecord(scene), missionController =>
        {
            var spawnComponent = new SpawnComponent(new AdimiToolsDuelSpawnFrame(), new AdimiToolsDuelSpawningBehavior());
            return new MissionBehavior[]
            {
                    MissionLobbyComponent.CreateBehavior(),
                    new AdimiToolsMissionMultiplayerDuel(spawnComponent),
                    new MissionMultiplayerGameModeDuelClient(),
                    new MultiplayerTimerComponent(),
                    spawnComponent,
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
                    new MultiplayerPreloadHelper(),
                    new NotAllPlayersReadyComponent(),
            };
        }, true, true);
    }
}
