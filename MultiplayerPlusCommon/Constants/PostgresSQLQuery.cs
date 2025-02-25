using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.Constants
{
    public static class PostgresSQLQuery
    {

        public static string SteamId = "";

        public static string GetPlayerClassLoadouts =
            "Select class_str,hi.game_str,si.game_str,bi.game_str,ai.game_str,li.game_str from users u " +
            "left join class_loadouts cl on u.id=cl.user_id " +
            "left join items hi on cl.head_item_id=hi.id " +
            "left join items si on cl.shoulder_item_id=si.id " +
            "left join items bi on cl.body_item_id=bi.id " +
            "left join items ai on cl.arms_item_id=ai.id " +
            "left join items li on cl.legs_item_id=li.id " +
            $"where u.steam_id='";

        public static string GetPlayerUniversalLoadouts =
            "select " +
            "t1.id taunt_1_id,t1.taunt_str taunt_1_value,t1.name taunt_1_name, " +
            "s1.id shout_1_id,s1.shout_str shout_1_value,s1.name shout_1_name,  " +
            "t2.id taunt_2_id,t2.taunt_str taunt_2_value,t2.name taunt_2_name, " +
            "s2.id shout_2_id,s2.shout_str shout_2_value,s2.name shout_2_name  " +
            "from universal_loadouts ul  " +
            "left join users u on ul.user_id=u.id " +
            "left join taunts t1 on ul.taunt_1_id = t1.id " +
            "left join shouts s1 on ul.shout_1_id = s1.id " +
            "left join taunts t2 on ul.taunt_2_id = t2.id " +
            "left join shouts s2 on ul.shout_2_id = s2.id " +
            $"where u.steam_id='";

        public static string GetPlayerAllLoadouts => GetPlayerClassLoadouts + SteamId + "'; " + GetPlayerUniversalLoadouts + SteamId + "' ";

        public static void SetSteamId(string steamId)
        {
            SteamId = steamId;
        }
    }
}
