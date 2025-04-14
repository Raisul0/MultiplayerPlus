using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Diamond;

namespace MultiplayerPlusCommon.GameModes.Skirmish
{
    public class MPPSkirmishGameMode : MultiplayerGameMode
    {
        public delegate void StartMultiplayerGameDelegate(string scene);
        public static StartMultiplayerGameDelegate OnStartMultiplayerGame;
        public MPPSkirmishGameMode(string name) : base(name)
        {

        }

        public override void JoinCustomGame(JoinGameData joinGameData)
        {
            LobbyGameStateCustomGameClient lobbyGameStateCustomGameClient = Game.Current.GameStateManager.CreateState<LobbyGameStateCustomGameClient>();
            lobbyGameStateCustomGameClient.SetStartingParameters(NetworkMain.GameClient, joinGameData.GameServerProperties.Address, joinGameData.GameServerProperties.Port, joinGameData.PeerIndex, joinGameData.SessionKey);
            Game.Current.GameStateManager.PushState(lobbyGameStateCustomGameClient, 0);
        }

        public override void StartMultiplayerGame(string scene)
        {
            if (OnStartMultiplayerGame != null)
            {
                OnStartMultiplayerGame(scene);
            }
        }
    }
}