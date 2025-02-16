using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusCommon.Helpers
{
    public static class MultiplayerPlusHelper
    {
        public static ItemObject GetItemObjectfromString(string itemId)
        {
            return MBObjectManager.Instance.GetObject<ItemObject>(itemId);
        }
    }
}
