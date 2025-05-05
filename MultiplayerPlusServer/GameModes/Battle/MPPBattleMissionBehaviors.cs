using MultiplayerPlusServer.GameModes.Skirmish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace MultiplayerPlusServer.GameModes.Battle
{
    public static class MPPBattleMissionBehaviors 
    {
        [MissionMethod]
        public static void OpenMPPBattleMissionServerBehaviors(string scene)
        {
            MissionState.OpenNew("MPPSkirmish", new MissionInitializerRecord(scene),
                delegate (Mission missionController)
                {
                    return new MissionBehavior[]
                    {
                        MissionLobbyComponent.CreateBehavior(),
                        new MultiplayerRoundController(),
                        new MPPBattleBehavior(),
                        new MultiplayerWarmupComponent(),
                        new MissionMultiplayerGameModeFlagDominationClient(),
                        new MultiplayerTimerComponent(),
                        new SpawnComponent(new MPPBattleSpawnFrameBehavior(), new MPPBattleSpawningBehavior()),
                        new MissionLobbyEquipmentNetworkComponent(),
                        new MultiplayerTeamSelectComponent(),
                        new MissionHardBorderPlacer(),
                        new MissionBoundaryPlacer(),
                        new AgentVictoryLogic(),
                        new AgentHumanAILogic(),
                        new MissionBoundaryCrossingHandler(),
                        new MultiplayerPollComponent(),
                        new MultiplayerAdminComponent(),
                        new MultiplayerGameNotificationsComponent(),
                        new MissionOptionsComponent(),
                        new MissionScoreboardComponent(new BattleScoreboardData()),
                        new EquipmentControllerLeaveLogic(),
                        new MultiplayerPreloadHelper()
                    };
                }, true, true);

        }
    }
}
