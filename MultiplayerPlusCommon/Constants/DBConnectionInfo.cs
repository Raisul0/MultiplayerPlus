using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.Constants
{
    public static class DBConnectionInfo
    {
        public const string Host = "aws-0-us-east-1.pooler.supabase.com";
        public const string Port = "5432";
        public const string Database = "postgres";
        public const string Username = "postgres.fefzymsmuhcgfjfvirpo";
        public const string Password = "QlHkBDlvEufVayPx";
        public const string ConnectionString = "Host="+Host+";Port="+Port+";Database="+ Database+ ";Username="+ Username+ ";Password="+ Password+"";
    }
}
