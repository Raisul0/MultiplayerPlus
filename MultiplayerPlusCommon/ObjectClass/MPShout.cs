using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaleWorlds.MountAndBlade.SkinVoiceManager;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPShout
    {
        public MPShout() 
        {
            ShoutId = "";
            ShoutName = "None";
            VoiceType = "";
        }
        public MPShout(string shoutId, string shoutName, string voiceType)
        {
            ShoutId = shoutId;
            ShoutName = shoutName;
            VoiceType = voiceType;
        }

        public string ShoutId { get; set; }
        public string ShoutName { get; set; }
        public string VoiceType { get; set; }
    }
}
