using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
namespace Website.Models
{
    public class DatabaseModel
    {
        private SqlConnection preSqlCon = new SqlConnection(@"Data Source=BRADLEIGHM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;");
        private SqlConnection sqlCon = new SqlConnection(@"Data Source=BRADLEIGHM\SQLEXPRESS;Initial Catalog=TeamManagement;Integrated Security=True;");
        
        public DatabaseModel()
        {
            SqlCommand createDB = new SqlCommand("If(db_id(N'TeamManagement') IS NULL) BEGIN CREATE DATABASE TeamManagement; END;", preSqlCon);
            SqlCommand createTables = new SqlCommand("", sqlCon);
            preSqlCon.Open();
            createDB.ExecuteNonQuery();
            sqlCon.Open();
            createDB.ExecuteNonQuery();
            
        }

    }
}