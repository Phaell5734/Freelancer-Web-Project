using System.Data;
using MySql.Data.MySqlClient;

namespace Proje.Models
{
    public class PostJobdb  //Postjob Models part where it connects with database
    {
        MySqlConnection con = new MySqlConnection("server=localhost;uid=root;pwd=root;database=veritabani");
        public string AddJob(PostJob urun)  //Addjob to database part
        {
            try
            {
                MySqlCommand com = new MySqlCommand("UrunEkle", con);  //Database UrunEkle Stored Procedure working
                com.CommandType = CommandType.StoredProcedure;
                MySqlParameter urunAdi = com.Parameters.AddWithValue("p_urun_adi", urun.urun_adi);
                MySqlParameter urunAciklamasi = com.Parameters.AddWithValue("p_urun_aciklamasi", urun.urun_aciklamasi);
                MySqlParameter urunType = com.Parameters.AddWithValue("p_urun_type", urun.urun_type);
                MySqlParameter isType = com.Parameters.AddWithValue("p_is_type", urun.is_type);                         //Adding values to the database with stored procedure
                MySqlParameter urunZaman = com.Parameters.AddWithValue("p_urun_zaman", urun.urun_zaman);
                MySqlParameter urunPara = com.Parameters.AddWithValue("p_urun_para", urun.urun_para);
                con.Open();
                com.ExecuteNonQuery();    //Executing the query
                con.Close();
                return ("OK");
            }
            catch (Exception ex)     //Exception to see what is going wrong
            {
                if (con.State== ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
            

        }
        

    }
}
