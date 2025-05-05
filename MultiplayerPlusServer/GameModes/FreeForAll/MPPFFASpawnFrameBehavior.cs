using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusServer.GameModes.FreeForAll
{
    public class MPPFFASpawnFrameBehavior : SpawnFrameBehaviorBase
    {
        public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
        {
            return GetSpawnFrameFromSpawnPoints(SpawnPoints.ToList(), null, hasMount);
        }
    }
}
