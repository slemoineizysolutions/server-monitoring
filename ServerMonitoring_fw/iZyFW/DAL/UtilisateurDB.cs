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
	public partial class UtilisateurDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("utilisateur.id AS utilisateur_id, utilisateur.nom AS utilisateur_nom, utilisateur.login AS utilisateur_login, ");
			mySql.Append("utilisateur.password AS utilisateur_password ");
			return mySql.ToString();
		}

		private static Utilisateur LoadADO(MySqlDataReader myReader)
		{
			Utilisateur myItem = new Utilisateur();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["utilisateur_id"]);
			myItem.nom = iZyMySQL.GetStringFromDBString(myReader["utilisateur_nom"]);
			myItem.login = iZyMySQL.GetStringFromDBString(myReader["utilisateur_login"]);
			myItem.password = iZyMySQL.GetStringFromDBString(myReader["utilisateur_password"]);
			return myItem;
		}

		public static Utilisateur Load(int id)
		{
			Utilisateur myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Utilisateur` utilisateur ");
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

		public static List<Utilisateur> FindAll()
		{
			Utilisateur myItem = null;
			List<Utilisateur> listItem = new List<Utilisateur>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Utilisateur` utilisateur ");
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

		public static Utilisateur Insert(Utilisateur myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `Utilisateur` ");
				mySql.Append("(`nom`, `login`, ");
				mySql.Append("`password` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@nom, @login, ");
				mySql.Append("@password );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@nom", myItem.nom);
				myCommand.Parameters.AddWithValue("@login", myItem.login);
				myCommand.Parameters.AddWithValue("@password", myItem.password);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static Utilisateur Update(Utilisateur myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `Utilisateur` SET ");
				mySql.Append("`nom` = @nom, ");
				mySql.Append("`login` = @login, ");
				mySql.Append("`password` = @password ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@nom", myItem.nom);
				myCommand.Parameters.AddWithValue("@login", myItem.login);
				myCommand.Parameters.AddWithValue("@password", myItem.password);
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
				mySql.Append("DELETE FROM `Utilisateur` WHERE `id` = @id; ");
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
