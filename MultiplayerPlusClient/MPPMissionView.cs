using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Multiplayer.View.MissionViews;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace MultiplayerPlus.Client
{
    [ViewCreatorModule]
    public class MPPMissionView
    {
        [ViewMethod("MPPTeamDeathMatch")]
        public static MissionView[] OpenMPPTeamDeathMatchMission(Mission mission)
        {
            List<MissionView> missionViews = new List<MissionView>
            {
                MultiplayerViewCreator.CreateMissionServerStatusUIHandler(),
                MultiplayerViewCreator.CreateMissionMultiplayerPreloadView(mission),
                MultiplayerViewCreator.CreateMissionKillNotificationUIHandler(),
                ViewCreator.CreateMissionAgentStatusUIHandler(mission),
                ViewCreator.CreateMissionMainAgentEquipmentController(mission),
                ViewCreator.CreateMissionMainAgentCheerBarkControllerView(mission),
                MultiplayerViewCreator.CreateMissionMultiplayerEscapeMenu("MPPTeamDeathMatch"),
                MultiplayerViewCreator.CreateMissionScoreBoardUIHandler(mission, true),
                MultiplayerViewCreator.CreatePollProgressUIHandler(),
                MultiplayerViewCreator.CreateMultiplayerMissionDeathCardUIHandler(null),
                ViewCreator.CreateOptionsUIHandler(),
                ViewCreator.CreateMissionMainAgentEquipDropView(mission),
                ViewCreator.CreateMissionBoundaryCrossingView(),
                new MissionBoundaryWallView(),
            };
            return missionViews.ToArray();
        }
    }
}
