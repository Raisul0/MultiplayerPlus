using MultiplayerPlusCommon;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusClient
{
    public class SubModule : MBSubModuleBase
    {
        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
        }
        public override void OnMultiplayerGameStart(Game game, object starterObject)
        {
            InformationManager.DisplayMessage(new InformationMessage("** Multiplayer Plus, Team Death Match Game Start Loading..."));
            TaleWorlds.Library.Debug.Print("** Multiplayer Plus, Team Death Match Game Start Loading...");

            MPPTeamDeathMatchGameMode.OnStartMultiplayerGame += MPPTeamDeathMatchMissionBehaviours.OpenMPPTeamDeathMatchClientBehaviours;
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPTeamDeathMatchGameMode("MPPTeamDeathMatch"));
        }
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission)
        {

            AddCommonBehaviors(mission);

        }

        public void AddCommonBehaviors(Mission mission)
        {
            //mission.AddMissionBehavior(new ClientAutoHandler());
        }
    }
}