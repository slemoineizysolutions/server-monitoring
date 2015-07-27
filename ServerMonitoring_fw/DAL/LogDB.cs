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
	public partial class LogDB
	{
		public static List<Log> FindAll(int idProjet)
		{
			Log myItem = null;
			List<Log> listItem = new List<Log>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Log` log ");
			mySql.Append("WHERE idProjet = @idProjet ");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idProjet", idProjet);
				myConnection.Open();
				using (MySqlDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						while (myReader.Read())
						{
							myItem = LoadADO(myReader);
							listItem.Add(myItem);
						}
					}
					myReader.Close();
				}
				myConnection.Close();
			}
			return listItem;
		}

		public static List<Log> FindFavoris(int idUtilisateur)
		{
			Log myItem = null;
			List<Log> listItem = new List<Log>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Log` log ");
			mySql.Append("JOIN `UtilisateurFavoris` utilisateurfavoris ON utilisateurfavoris.idEntite = log.id ");
			mySql.Append("WHERE utilisateurfavoris.idType = @idType AND utilisateurfavoris.idUtilisateur = @idUtilisateur");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idType", EnumTypeFavoris.LOG);
				myCommand.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
				myConnection.Open();
				using (MySqlDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						while (myReader.Read())
						{
							myItem = LoadADO(myReader);
							listItem.Add(myItem);
						}
					}
					myReader.Close();
				}
				myConnection.Close();
			}
			return listItem;
		}
	}
}
