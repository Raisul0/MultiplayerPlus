using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ObjectClass;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.MPPLoadout
{
    public static class MPPLoadout
    {
        public static void LoadMPPLoadout(NetworkCommunicator peer)
        {
            try
            {
                MPAgent player = new MPAgent(peer);
                PostgresSQLQuery.SetSteamId(player.SteamId);

                var query = PostgresSQLQuery.GetPlayerAllLoadouts;
                using (var conn = new NpgsqlConnection(DBConnectionInfo.ConnectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var classCosmatics = new MPAgentClassCosmetic();

                                classCosmatics.Class = reader[0].ToString();
                                classCosmatics.Head = reader[1].ToString();
                                classCosmatics.Shoulder = reader[2].ToString();
                                classCosmatics.Body = reader[3].ToString();
                                classCosmatics.Arms = reader[4].ToString();
                                classCosmatics.Legs = reader[5].ToString();

                                player.ClassCosmetics.Add(classCosmatics);
                            }

                            reader.NextResult();

                            while (reader.Read())
                            {
                                for (int i = 1; i <= 10; i++)
                                {
                                    var tauntId = reader["taunt_" + i + "_id"].ToString();
                                    var tauntAction = reader["taunt_" + i + "_value"].ToString();
                                    var tauntName = reader["taunt_" + i + "_name"].ToString();

                                    player.TauntWheel.UpdateTauntSlot(i, tauntId, tauntAction, tauntName);

                                    var shoutId = reader["shout_" + i + "_id"].ToString();
                                    var voiceType = reader["shout_" + i + "_value"].ToString();
                                    var shoutName = reader["shout_" + i + "_name"].ToString();

                                    player.ShoutWheel.UpdateShoutSlot(i, shoutId, voiceType, shoutName);

                                }

                                player.GameMVPTaunt.TauntId = reader["mvp_game_taunt_id"].ToString();
                                player.GameMVPTaunt.TauntAction = reader["mvp_game_taunt_value"].ToString();
                                player.GameMVPTaunt.TauntName = reader["mvp_game_taunt_name"].ToString();

                                player.RoundMVPTaunt.TauntId = reader["mvp_round_taunt_id"].ToString();
                                player.RoundMVPTaunt.TauntAction= reader["mvp_round_taunt_value"].ToString();
                                player.RoundMVPTaunt.TauntName = reader["mvp_round_taunt_name"].ToString();

                                
                            }
                        }
                    }
                }

                MPPlayers.AddPlayer(player);

            }
            catch (NpgsqlException ex)
            {
                TaleWorlds.Library.Debug.Print(ex.Message);
            }
        }
    }
}
