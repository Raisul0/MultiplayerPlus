using MultiplayerPlusCommon;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Debug = TaleWorlds.Library.Debug;

namespace MultiplayerPlusServer;

public class SubModule : MBSubModuleBase
{
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
        Debug.Print("** Mulitiplayer Plus, OnSubModuleLoad BY RAISUL **", 0, Debug.DebugColor.Red);
        Console.WriteLine("hello_");
    }
    
    protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
    {
        Debug.Print("** Mulitiplayer Plus, InitializeGameStarter BY RAISUL **", 0, Debug.DebugColor.Red);
        base.InitializeGameStarter(game, starterObject);
    }

    public override void OnMultiplayerGameStart(Game game, object starterObject)
    {
        Debug.Print("** Mulitiplayer Plus, OnMultiplayerGameStart **");
        MPPTeamDeathMatchGameMode.OnStartMultiplayerGame += MPPTeamDeathMatchMissionBehaviours.OpenMPPTeamDeathMatchServerBehaviours;
        TaleWorlds.MountAndBlade.Module.CurrentModule.AddMultiplayerGameMode(new MPPTeamDeathMatchGameMode("MPPTeamDeathMatch"));

    }
    public override void OnBeforeMissionBehaviorInitialize(Mission mission)
    {

        AddCommonBehaviors(mission);

    }
    private void AddCommonBehaviors(Mission mission)
    {
        //mission.AddMissionBehavior(new ServerAutoHandler());
    }
}