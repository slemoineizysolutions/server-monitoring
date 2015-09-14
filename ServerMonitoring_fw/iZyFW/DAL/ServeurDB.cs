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
	public partial class ServeurDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("serveur.id AS serveur_id, serveur.libelle AS serveur_libelle, serveur.ipLocale AS serveur_ipLocale, ");
			mySql.Append("serveur.ipPublique AS serveur_ipPublique, cheminInfosMonitoring AS serveur_cheminInfosMonitoring ");
			return mySql.ToString();
		}

		private static Serveur LoadADO(MySqlDataReader myReader)
		{
			Serveur myItem = new Serveur();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["serveur_id"]);
			myItem.libelle = iZyMySQL.GetStringFromDBString(myReader["serveur_libelle"]);
			myItem.ipLocale = iZyMySQL.GetStringFromDBString(myReader["serveur_ipLocale"]);
			myItem.ipPublique = iZyMySQL.GetStringFromDBString(myReader["serveur_ipPublique"]);
			myItem.cheminInfosMonitoring = iZyMySQL.GetStringFromDBString(myReader["serveur_cheminInfosMonitoring"]);
			return myItem;
		}

		public static Serveur Load(int id)
		{
			Serveur myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Serveur` serveur ");
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

		public static List<Serveur> FindAll()
		{
			Serveur myItem = null;
			List<Serveur> listItem = new List<Serveur>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `Serveur` serveur ");
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

		public static Serveur Insert(Serveur myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `Serveur` ");
				mySql.Append("(`libelle`, `ipLocale`, ");
				mySql.Append("`ipPublique`, `cheminInfosMonitoring` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@libelle, @ipLocale, ");
				mySql.Append("@ipPublique, @cheminInfosMonitoring );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@ipLocale", myItem.ipLocale);
				myCommand.Parameters.AddWithValue("@ipPublique", myItem.ipPublique);
				myCommand.Parameters.AddWithValue("@cheminInfosMonitoring", myItem.cheminInfosMonitoring);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static Serveur Update(Serveur myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `Serveur` SET ");
				mySql.Append("`libelle` = @libelle, ");
				mySql.Append("`ipLocale` = @ipLocale, ");
				mySql.Append("`ipPublique` = @ipPublique, ");
				mySql.Append("`cheminInfosMonitoring` = @cheminInfosMonitoring ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@ipLocale", myItem.ipLocale);
				myCommand.Parameters.AddWithValue("@ipPublique", myItem.ipPublique);
				myCommand.Parameters.AddWithValue("@cheminInfosMonitoring", myItem.cheminInfosMonitoring);
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
				mySql.Append("DELETE FROM `Serveur` WHERE `id` = @id; ");
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
