using MultiplayerPlusClient.GameModes.Battle;
using MultiplayerPlusClient.GameModes.Duel;
using MultiplayerPlusClient.GameModes.Skirmish;
using MultiplayerPlusClient.GameModes.TeamDeathMatch;
using MultiplayerPlusCommon.GameModes.Battle;
using MultiplayerPlusCommon.GameModes.Duel;
using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusCommon.GameModes.TeamDeathMatch;
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
            InformationManager.DisplayMessage(new InformationMessage("** Multiplayer Plus, Mod Loading..."));
            TaleWorlds.Library.Debug.Print("** Multiplayer Plus, Mod Loading...");

            //TeamDeathMatch 
            MPPTeamDeathMatchGameMode.OnStartMultiplayerGame += MPPTeamDeathMatchMissionBehaviors.OpenMPPTeamDeathMatchClientBehaviors;
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPTeamDeathMatchGameMode("MPPTeamDeathMatch"));

            //Skirmish 
            MPPSkirmishGameMode.OnStartMultiplayerGame += MPPSkirmishMissionBehaviors.OpenMPPSkirmishClientBehaviors;
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPSkirmishGameMode("MPPSkirmish"));

            //Battle 
            MPPBattleGameMode.OnStartMultiplayerGame += MPPBattleMissionBehaviors.OpenMPPBattleClientBehaviors;
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPBattleGameMode("MPPBattle"));

            //Duel 
            MPPDuelGameMode.OnStartMultiplayerGame += MPPDuelMissionBehaviors.OpenMPPDuelClientBehaviors;
            TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPDuelGameMode("MPPDuel"));
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
            mission.AddMissionBehavior(new ClientAutoHandler());
        }
    }
}