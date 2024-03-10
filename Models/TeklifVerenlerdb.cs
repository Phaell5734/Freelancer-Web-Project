using System.Data;
using MySql.Data.MySqlClient;

namespace Proje.Models
{
    public class TeklifVerenlerdb  //Seeing the given offers module model
    {
        MySqlConnection con = new MySqlConnection("server=localhost;uid=root;pwd=root;database=veritabani");  //MySql Connectionstring
        public string TeklifAdd(TeklifVerenler urun)  //Add Offer Module
        {
            try
            {
                MySqlCommand com = new MySqlCommand("TeklifEkle", con); //AddOffer Stored Procedure getting called here
                com.CommandType = CommandType.StoredProcedure;
                MySqlParameter urunZaman = com.Parameters.AddWithValue("urun_id", urun.urun_id);
                MySqlParameter urunPara = com.Parameters.AddWithValue("teklif_para", urun.teklif_para); //Stored Procedure parameters
                con.Open();
                com.ExecuteNonQuery();
                con.Close();                                                //Sql Connection Part
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)  //When an error occurs a message will pop up
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public string TeklifKabul(TeklifVerenler talep)  //Offer Accept Module Model
        {
            try
            {
                MySqlCommand com = new MySqlCommand();

                con.Open();
                com.Connection = con;
                com.CommandText = "CALL TalepOnayla (" + talep.teklif_id + ", 1)";     //Offer getting accepted with Stored Procedure
                com.ExecuteNonQuery();
                con.Close();
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                          //Exception to see what is going wrong
                }
                return (ex.Message.ToString());
            }
        }


    }
}
