using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon
{
    public class SubModule : MBSubModuleBase
    {
        public const string ModuleId = "MultiplayerPlusCommon";
        protected override void OnSubModuleLoad()
        {

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
        }

        // TODO => Find if GameModels can be moved to their own extension
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {

        }
    }
}
