using HarmonyLib;
using MultiplayerPlusCommon.GameModes.Battle;
using MultiplayerPlusCommon.GameModes.Duel;
using MultiplayerPlusCommon.GameModes.FreeForAll;
using MultiplayerPlusCommon.GameModes.Siege;
using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusCommon.GameModes.TeamDeathMatch;
using MultiplayerPlusServer.GameModes.Battle;
using MultiplayerPlusServer.GameModes.Duel;
using MultiplayerPlusServer.GameModes.FreeForAll;
using MultiplayerPlusServer.GameModes.Siege;
using MultiplayerPlusServer.GameModes.Skirmish;
using MultiplayerPlusServer.GameModes.TeamDeathMatch;
using MultiplayerPlusServer.Patch;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Debug = TaleWorlds.Library.Debug;

namespace MultiplayerPlusServer;

public class SubModule : MBSubModuleBase
{
    public const string ModuleId = "MultiplayerPlusServer";
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
        DirtyServerPatcher.Patch();
        Debug.Print("** Mulitiplayer Plus, OnSubModuleLoad BY RAISUL **", 0, Debug.DebugColor.Red);
    }
    
    protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
    {
        Debug.Print("** Mulitiplayer Plus, InitializeGameStarter BY RAISUL **", 0, Debug.DebugColor.Red);
        base.InitializeGameStarter(game, starterObject);
    }

    public override void OnMultiplayerGameStart(Game game, object starterObject)
    {
        Debug.Print("** Mulitiplayer Plus, OnMultiplayerGameStart **");

        //TeamDeathMatch 
        MPPTeamDeathMatchGameMode.OnStartMultiplayerGame += MPPTeamDeathMatchMissionBehaviors.OpenMPPTeamDeathMatchServerBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPTeamDeathMatchGameMode("MPPTeamDeathMatch"));

        //Skirmish
        MPPSkirmishGameMode.OnStartMultiplayerGame += MPPSkirmishMissionBehaviors.OpenMPPSkirmishMissionServerBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPSkirmishGameMode("MPPSkirmish"));

        //Battle 
        MPPBattleGameMode.OnStartMultiplayerGame += MPPBattleMissionBehaviors.OpenMPPBattleMissionServerBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPBattleGameMode("MPPBattle"));

        //Duel 
        MPPDuelGameMode.OnStartMultiplayerGame += DrDuelMissionBehavior.OpenDrDuelServerMissionBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPDuelGameMode("DrDuel"));

        //Siege 
        MPPSiegeGameMode.OnStartMultiplayerGame += MPPSiegeMissionBehaviors.OpenMPPSiegeMissionServerBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPSiegeGameMode("MPPSiege"));

        //Free For All 
        MPPFreeForAllGameMode.OnStartMultiplayerGame += MPPFreeForAllMissionBehaviors.OpenMPPFreeForAllMissionServerBehaviors;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPFreeForAllGameMode("MPPFreeForAll"));

    }
    public override void OnBeforeMissionBehaviorInitialize(Mission mission)
    {

        AddCommonBehaviors(mission);

    }
    private void AddCommonBehaviors(Mission mission)
    {
        mission.AddMissionBehavior(new ServerAutoHandler());
    }
}