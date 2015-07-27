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
	public partial class LogDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("log.id AS log_id, log.idProjet AS log_idProjet, log.libelle AS log_libelle, ");
			mySql.Append("log.cheminFichier AS log_cheminFichier, log.commentaire AS log_commentaire ");
			return mySql.ToString();
		}

		private static Log LoadADO(MySqlDataReader myReader)
		{
			Log myItem = new Log();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["log_id"]);
			myItem.idProjet = iZyMySQL.GetIntFromDBInt(myReader["log_idProjet"]);
			myItem.libelle = iZyMySQL.GetStringFromDBString(myReader["log_libelle"]);
			myItem.cheminFichier = iZyMySQL.GetStringFromDBString(myReader["log_cheminFichier"]);
			myItem.commentaire = iZyMySQL.GetStringFromDBString(myReader["log_commentaire"]);
			return myItem;
		}

		public static Log Load(int id)
		{
			Log myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Log` log ");
			mySql.Append("WHERE `id` = @id ");
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", id);
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

		public static List<Log> FindAll()
		{
			Log myItem = null;
			List<Log> listItem = new List<Log>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Log` log ");
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

		public static Log Insert(Log myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `Log` ");
				mySql.Append("(`idProjet`, `libelle`, ");
				mySql.Append("`cheminFichier`, commentaire )");
				mySql.Append(" VALUES ");
				mySql.Append("(@idProjet, @libelle, ");
				mySql.Append("@cheminFichier, @commentaire );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idProjet", myItem.idProjet);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@cheminFichier", myItem.cheminFichier);
				myCommand.Parameters.AddWithValue("@commentaire", myItem.commentaire);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static Log Update(Log myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `Log` SET ");
				mySql.Append("`idProjet` = @idProjet, ");
				mySql.Append("`libelle` = @libelle, ");
				mySql.Append("`cheminFichier` = @cheminFichier, ");
				mySql.Append("`commentaire` = @commentaire ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@idProjet", myItem.idProjet);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@cheminFichier", myItem.cheminFichier);
				myCommand.Parameters.AddWithValue("@commentaire", myItem.commentaire);
				myConnection.Open();
				myCommand.ExecuteNonQuery();
				myConnection.Close();
			}

			return myItem;
		}

		public static bool Delete(int id)
		{
			bool deleted = false;
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("DELETE FROM `Log` WHERE `id` = @id; ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", id);
				myConnection.Open();
				deleted = myCommand.ExecuteNonQuery() > 0;
				myConnection.Close();
			}

			return deleted;
		}

		#endregion INSERT / UPDATE / DELETE
	}
}
