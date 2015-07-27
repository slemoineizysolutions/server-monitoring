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
	public partial class BaseDonneeDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("basedonnee.id AS basedonnee_id, basedonnee.idProjet AS basedonnee_idProjet, basedonnee.libelle AS basedonnee_libelle, ");
			mySql.Append("basedonnee.host AS basedonnee_host, basedonnee.databaseName AS basedonnee_databaseName, basedonnee.user AS basedonnee_user, ");
			mySql.Append("basedonnee.password AS basedonnee_password, basedonnee.cheminSauvegarde AS basedonnee_cheminSauvegarde ");
			return mySql.ToString();
		}

		private static BaseDonnee LoadADO(MySqlDataReader myReader)
		{
			BaseDonnee myItem = new BaseDonnee();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["basedonnee_id"]);
			myItem.idProjet = iZyMySQL.GetIntFromDBInt(myReader["basedonnee_idProjet"]);
			myItem.libelle = iZyMySQL.GetStringFromDBString(myReader["basedonnee_libelle"]);
			myItem.host = iZyMySQL.GetStringFromDBString(myReader["basedonnee_host"]);
			myItem.databaseName = iZyMySQL.GetStringFromDBString(myReader["basedonnee_databaseName"]);
			myItem.user = iZyMySQL.GetStringFromDBString(myReader["basedonnee_user"]);
			myItem.password = iZyMySQL.GetStringFromDBString(myReader["basedonnee_password"]);
			myItem.cheminSauvegarde = iZyMySQL.GetStringFromDBString(myReader["basedonnee_cheminSauvegarde"]);
			return myItem;
		}

		public static BaseDonnee Load(int id)
		{
			BaseDonnee myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `BaseDonnee` basedonnee ");
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

		public static List<BaseDonnee> FindAll()
		{
			BaseDonnee myItem = null;
			List<BaseDonnee> listItem = new List<BaseDonnee>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `BaseDonnee` basedonnee ");
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

		public static BaseDonnee Insert(BaseDonnee myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `BaseDonnee` ");
				mySql.Append("(`idProjet`, `libelle`, ");
				mySql.Append("`host`, `databaseName`, `user`, ");
				mySql.Append("`password`, `cheminSauvegarde` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@idProjet, @libelle, ");
				mySql.Append("@host, @databaseName, @user, ");
				mySql.Append("@password, @cheminSauvegarde );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@idProjet", myItem.idProjet);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@host", myItem.host);
				myCommand.Parameters.AddWithValue("@databaseName", myItem.databaseName);
				myCommand.Parameters.AddWithValue("@user", myItem.user);
				myCommand.Parameters.AddWithValue("@password", myItem.password);
				myCommand.Parameters.AddWithValue("@cheminSauvegarde", myItem.cheminSauvegarde);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static BaseDonnee Update(BaseDonnee myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `BaseDonnee` SET ");
				mySql.Append("`idProjet` = @idProjet, ");
				mySql.Append("`libelle` = @libelle, ");
				mySql.Append("`host` = @host, ");
				mySql.Append("`databaseName` = @databaseName, ");
				mySql.Append("`user` = @user, ");
				mySql.Append("`password` = @password, ");
				mySql.Append("`cheminSauvegarde` = @cheminSauvegarde ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@idProjet", myItem.idProjet);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@host", myItem.host);
				myCommand.Parameters.AddWithValue("@databaseName", myItem.databaseName);
				myCommand.Parameters.AddWithValue("@user", myItem.user);
				myCommand.Parameters.AddWithValue("@password", myItem.password);
				myCommand.Parameters.AddWithValue("@cheminSauvegarde", myItem.cheminSauvegarde);
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
				mySql.Append("DELETE FROM `BaseDonnee` WHERE `id` = @id; ");
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
