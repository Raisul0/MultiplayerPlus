using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusServer
{
    public class MPPTeamDeathMatchSpawnFrameBehavior : SpawnFrameBehaviorBase
    {
        public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
        {
            return GetSpawnFrameFromSpawnPoints(SpawnPoints.ToList(), null, hasMount);
        }

        public MatrixFrame GetClosestSpawnFrame(Team team, bool hasMount, bool isInitialSpawn, MatrixFrame spawnPos)
        {
            return GetSpawnFrame(team, hasMount, isInitialSpawn);
        }

        public MPPTeamDeathMatchSpawnFrameBehavior()
        {
        }
    }
}
