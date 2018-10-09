using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace MrCy.BaseClass
{
    class DBConn
    {
        public static SqlConnection CyCon()
        {
            return new SqlConnection("server=92996974633A47E\\SQLEXPRESS;database=db_MrCy;Integrated Security=true");
        }
    }
}
