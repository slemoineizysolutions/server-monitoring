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
	public partial class EnumThemeDB
	{
		#region LOAD / FIND

		private static string GetSelectFields()
		{
			StringBuilder mySql = new StringBuilder();
			mySql.Append("enumtheme.id AS enumtheme_id, enumtheme.libelle AS enumtheme_libelle, enumtheme.cssClass AS enumtheme_cssClass ");
			return mySql.ToString();
		}

		private static EnumTheme LoadADO(MySqlDataReader myReader)
		{
			EnumTheme myItem = new EnumTheme();
			myItem.id = iZyMySQL.GetIntFromDBInt(myReader["enumtheme_id"]);
			myItem.libelle = iZyMySQL.GetStringFromDBString(myReader["enumtheme_libelle"]);
			myItem.cssClass = iZyMySQL.GetStringFromDBString(myReader["enumtheme_cssClass"]);
			return myItem;
		}

		public static EnumTheme Load(int id)
		{
			EnumTheme myItem = null;
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `EnumTheme` enumtheme ");
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

		public static List<EnumTheme> FindAll()
		{
			EnumTheme myItem = null;
			List<EnumTheme> listItem = new List<EnumTheme>();
			StringBuilder mySql = new StringBuilder();
			mySql.Append("SELECT ");
			mySql.Append(GetSelectFields() + " ");
			mySql.Append("FROM `EnumTheme` enumtheme ");
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

		public static EnumTheme Insert(EnumTheme myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("INSERT INTO `EnumTheme` ");
				mySql.Append("(`libelle`, `cssClass` )");
				mySql.Append(" VALUES ");
				mySql.Append("(@libelle, @cssClass );");
				mySql.Append("SELECT LAST_INSERT_ID(); ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@cssClass", myItem.cssClass);
				myConnection.Open();
				myItem.id = iZyMySQL.GetIntFromDBInt(myCommand.ExecuteScalar());
				myConnection.Close();
			}

			return myItem;
		}

		public static EnumTheme Update(EnumTheme myItem)
		{
			using (MySqlConnection myConnection = new MySqlConnection(Param.MySQLConnectionString))
			{
				StringBuilder mySql = new StringBuilder();
				mySql.Append("UPDATE `EnumTheme` SET ");
				mySql.Append("`libelle` = @libelle, ");
				mySql.Append("`cssClass` = @cssClass ");
				mySql.Append("WHERE `id` = @id ");
				MySqlCommand myCommand = new MySqlCommand(mySql.ToString(), myConnection);
				myCommand.CommandType = CommandType.Text;
				myCommand.Parameters.AddWithValue("@id", myItem.id);
				myCommand.Parameters.AddWithValue("@libelle", myItem.libelle);
				myCommand.Parameters.AddWithValue("@cssClass", myItem.cssClass);
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
				mySql.Append("DELETE FROM `EnumTheme` WHERE `id` = @id; ");
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
