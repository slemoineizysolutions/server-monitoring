using MySql.Data.MySqlClient;
using ServerMonitoring_fw.BIZ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitoring_fw.DAL
{
	public partial class UtilisateurDB
	{
		public static Utilisateur Login(string login, string password)
		{
			Utilisateur myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Utilisateur` utilisateur ");
			mySql.Append("WHERE `login` = @login ");
			mySql.Append("AND `password` = SHA1(@password) ");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@login", login);
				myCommand.Parameters.AddWithValue("@password", password);
				myConnection.Open();
				using (MySqlDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						myReader.Read();
						myItem = LoadADO(myReader);
					}
					myReader.Close();
				}
				myConnection.Close();
			}
			return myItem;
		}
	}
}
