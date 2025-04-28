using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.Constants
{
    public static class PostgresSQLQuery
    {
        public static int NoOfColumn = 10;
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
            "select " + GetTauntColumns() + GetShoutColumns() +
            "from universal_loadouts ul  " +
            "left join users u on u.id=ul.user_id " +
            GetTauntJoins() +
            GetShoutJoins() +
            $"where u.steam_id='";

        public static string GetPlayerAllLoadouts => GetPlayerClassLoadouts + SteamId + "'; " + GetPlayerUniversalLoadouts + SteamId + "' ";

        public static void SetSteamId(string steamId)
        {
            SteamId = steamId;
        }

        public static string GetTauntColumns()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 1; i <= NoOfColumn; i++)
            {
                sb.Append("t"+i+".id taunt_"+i+"_id ");
                sb.Append(",");
                sb.Append("t" + i + ".taunt_str taunt_" + i + "_value ");
                sb.Append(",");
                sb.Append("t" + i + ".name taunt_" + i + "_name ");
                sb.Append(",");
            }
            return sb.ToString();
        }

        public static string GetShoutColumns()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= NoOfColumn; i++)
            {
                sb.Append("s" + i + ".id shout_" + i + "_id ");
                sb.Append(",");
                sb.Append("s" + i + ".shout_str shout_" + i + "_value ");
                sb.Append(",");
                sb.Append("s" + i + ".name shout_" + i + "_name ");
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);  
            return sb.ToString();
        }
        
        public static string GetTauntJoins()
        {
            StringBuilder sb = new StringBuilder();
            for(int i=1;i <= NoOfColumn; i++)
            {
                sb.Append("left join taunts t" + i + " on ul.taunt_" + i + "_id = t" + i + ".id ");
            }

            return sb.ToString();
        }

        public static string GetShoutJoins()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= NoOfColumn; i++)
            {
                sb.Append("left join shouts s" + i + " on ul.shout_" + i + "_id = s" + i + ".id ");
            }

            return sb.ToString();
        }
    }
}
