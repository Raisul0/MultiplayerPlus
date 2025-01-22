using System.Collections.Generic;
using System.Linq;
using MultiplayerPlusCommon;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer;
using TaleWorlds.MountAndBlade.Source.Missions;


namespace MultiplayerPlusClient
{
    public static class MPPTeamDeathMatchMissionBehaviours
    {
        [MissionMethod]
        public static void OpenMPPTeamDeathMatchClientBehaviours(string scene)
        {
            MissionState.OpenNew("MPPTeamDeathMatch", new MissionInitializerRecord(scene), delegate (Mission missionController)
            {
                return new MissionBehavior[]
                {
                    MissionLobbyComponent.CreateBehavior(),
                    new MPPTeamDeathMatchCommonBehavior(),

                    // Native behaviors
                    new MultiplayerTimerComponent(),
                    new MultiplayerMissionAgentVisualSpawnComponent(),
                    new MissionLobbyEquipmentNetworkComponent(),
                    new MissionHardBorderPlacer(),
                    new MissionBoundaryPlacer(),
                    new MissionBoundaryCrossingHandler(),
                    new MultiplayerPollComponent(),
                    new MultiplayerGameNotificationsComponent(),
                    new MissionOptionsComponent(),
                    new MissionScoreboardComponent(new TDMScoreboardData()),
                };
            }, true, true);
        }

        

    }
}