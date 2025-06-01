using TaleWorlds.Library;

namespace MultiplayerPlusCommon.GameModes.Duel
{
    public class AdimiToolsConsoleLog
    {
        public static void Log(string str, Debug.DebugColor color = Debug.DebugColor.Green)
        {
            Debug.Print("[Adimi Tools]: " + str, 0, color);
        }
    }
}


