using System.Data;
using System.Data.SqlClient;
using Proje.Models;

namespace Proje.Models
{
    public class UpdateTeklifdb
    {
        public string teklifguncelleme(UpdateTeklif teklif) //Offer Update Module
        {

            SqlConnection con = new SqlConnection("server=localhost;uid=Root;pwd=Root;database=veritabani");
            try
            {
                SqlCommand com = new SqlCommand("teklifGuncelle", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = com.Parameters.AddWithValue("teklif_id", teklif.teklif_id);
                SqlParameter sqlParameter1 = com.Parameters.AddWithValue("teklif_para", teklif.teklif_para); //Data getting loaded into database with stored procedure
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                                       //The part where an error occurs a message popups
                }
                return (ex.Message.ToString());
            }


        }
        public string TeklifOnay(TeklifVerenler teklif) //Offer Accept Module
        {

            SqlConnection con = new SqlConnection("server=localhost;uid=Root;pwd=Root;database=veritabani");
            try
            {
                SqlCommand com = new SqlCommand("TalepOnayla", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParameter = com.Parameters.AddWithValue("teklif_id", teklif.teklif_id); //Data getting loaded into database with stored procedure
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                return ("OK");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();                                       //The part where an error occurs a message popups
                }
                return (ex.Message.ToString());
            }


        }
    }
}
