using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPShoutWheel
    {
        public List<MPShout> Shouts = new List<MPShout> {
            new MPShout(),
            new MPShout(),
            new MPShout(),
            new MPShout(),
            new MPShout(),
            new MPShout(),
        };

        public void UpdateShoutSlot(int slotId, string shoutId, string voiceType, string shoutName)
        {
            if(!string.IsNullOrEmpty(voiceType))
            {
                var index = slotId - 1;
                var shout = Shouts.ElementAt(index);
                shout.ShoutId = shoutId;
                shout.VoiceType = voiceType;
                shout.ShoutName = shoutName;
            }
        }

        public (string,string) GetShoutIdNameSlot(int slotId)
        {
            var index = slotId - 1;
            var shout = Shouts.ElementAt(index);

            return (shout.ShoutId,shout.ShoutName);
        }

        public string FindShoutVoiceType(string shoutId)
        {
            return Shouts.FirstOrDefault(x => x.ShoutId == shoutId)?.VoiceType ?? "";
        }
    }
}
