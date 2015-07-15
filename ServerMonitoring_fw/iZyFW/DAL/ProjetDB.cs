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
	public partial class ProjetDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("projet.id AS projet_id, projet.libelle AS projet_libelle, projet.idTheme AS projet_idTheme ");
			return mySql.ToString();
		}

		private static Projet LoadADO(MySqlDataReader myReader)
		{
			Projet myItem = new Projet();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["projet_id"]);
			myItem.libelle = iZyMySQL.GetStringFromDBString(myReader["projet_libelle"]);
			myItem.idTheme = iZyMySQL.GetIntFromDBInt(myReader["projet_idTheme"]);
			return myItem;
		}

		public static Projet Load(int id)
		{
			Projet myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Projet` projet ");
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

		public static List<Projet> FindAll()
		{
			Projet myItem = null;
			List<Projet> listItem = new List<Projet>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Projet` projet ");
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

		public static Projet Insert(Projet myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `Projet` ");
				mySql.Append("(`libelle`, `idTheme` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@libelle, @idTheme );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@idTheme", myItem.idTheme);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static Projet Update(Projet myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `Projet` SET ");
				mySql.Append("`libelle` = @libelle, ");
				mySql.Append("`idTheme` = @idTheme ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@idTheme", myItem.idTheme);
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
				mySql.Append("DELETE FROM `Projet` WHERE `id` = @id; ");
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
