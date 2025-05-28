using MultiplayerPlusClient.Patch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = TaleWorlds.Library.Debug;

namespace MultiplayerPlusClient.Patch
{
    public static class DirtyClientPatcher
    {
        public static bool Patch()
        {
            Debug.Print("** Mulitiplayer Plus, Patching Started ! **", 0, Debug.DebugColor.Yellow);
            bool patchSuccess = true;

            patchSuccess &= Patch_MultiplayerEndOfBattleVM.Patch();
            
            Debug.Print("** Mulitiplayer Plus, Patching Ended ! **", 0, Debug.DebugColor.Yellow);
            return patchSuccess;
        }
    }
}
