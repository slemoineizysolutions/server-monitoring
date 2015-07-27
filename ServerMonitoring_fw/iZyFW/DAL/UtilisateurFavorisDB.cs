using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iZyTools.DataBase.MySQL;
using MySql.Data.MySqlClient;
using ServerMonitoring_fw.BIZ;

namespace ServerMonitoring_fw.DAL
{
	public partial class UtilisateurFavorisDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("utilisateurfavoris.idUtilisateur AS utilisateurfavoris_idUtilisateur, utilisateurfavoris.idEntite AS utilisateurfavoris_idEntite, utilisateurfavoris.idType AS utilisateurfavoris_idType ");
			return mySql.ToString();
		}

		private static UtilisateurFavoris LoadADO(MySqlDataReader myReader)
		{
			UtilisateurFavoris myItem = new UtilisateurFavoris();
			myItem.idUtilisateur = iZyMySQL.GetIntFromDBInt(myReader["utilisateurfavoris_idUtilisateur"]);
			myItem.idEntite = iZyMySQL.GetIntFromDBInt(myReader["utilisateurfavoris_idEntite"]);
			myItem.idType = iZyMySQL.GetIntFromDBInt(myReader["utilisateurfavoris_idType"]);
			return myItem;
		}

		public static UtilisateurFavoris Load(int idUtilisateur, int idEntite, int idType)
		{
			UtilisateurFavoris myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `UtilisateurFavoris` utilisateurfavoris ");
			mySql.Append("WHERE `idUtilisateur` = @idUtilisateur AND `idEntite` = @idEntite AND `idType` = @idType ");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
				myCommand.Parameters.AddWithValue("@idEntite", idEntite);
				myCommand.Parameters.AddWithValue("@idType", idType);
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

		public static List<UtilisateurFavoris> FindAll()
		{
			UtilisateurFavoris myItem = null;
			List<UtilisateurFavoris> listItem = new List<UtilisateurFavoris>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `UtilisateurFavoris` utilisateurfavoris ");
			mySql.Append("WHERE 1 ");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
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

		#endregion LOAD / FIND

		#region INSERT / UPDATE / DELETE

		public static UtilisateurFavoris Insert(UtilisateurFavoris myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `UtilisateurFavoris` ");
				mySql.Append("(`idUtilisateur`, `idEntite`, `idType` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@idUtilisateur, @idEntite, @idType );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idUtilisateur", myItem.idUtilisateur);
				myCommand.Parameters.AddWithValue("@idEntite", myItem.idEntite);
				myCommand.Parameters.AddWithValue("@idType", myItem.idType);
				myConnection.Open();
				myCommand.ExecuteScalar();
				myConnection.Close();
			}

			return myItem;
		}

		public static UtilisateurFavoris Update(UtilisateurFavoris myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `UtilisateurFavoris` SET ");
				
				mySql.Append("WHERE `idUtilisateur` = @idUtilisateur AND `idEntite` = @idEntite AND `idType` = @idType ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idUtilisateur", myItem.idUtilisateur);
				myCommand.Parameters.AddWithValue("@idEntite", myItem.idEntite);
				myCommand.Parameters.AddWithValue("@idType", myItem.idType);
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}

			return myItem;
		}

		public static bool Delete(int idUtilisateur, int idEntite, int idType)
		{
			bool deleted = false;
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("DELETE FROM `UtilisateurFavoris` WHERE `idUtilisateur` = @idUtilisateur AND `idEntite` = @idEntite AND `idType` = @idType; ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
				myCommand.Parameters.AddWithValue("@idEntite", idEntite);
				myCommand.Parameters.AddWithValue("@idType", idType);
				myConnection.Open();
				deleted = myCommand.ExecuteNonQuery() > 0;
				myConnection.Close();
			}

			return deleted;
		}

		#endregion INSERT / UPDATE / DELETE
	}
}
