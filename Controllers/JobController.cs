using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Humanizer;

namespace Proje.Controllers
{
    //[Authorize]
    //Postjob showjob & give offers moduls are in this controller
    public class JobController : Controller
    {
        //For Mysql connection codes
        MySqlCommand com;
        MySqlDataReader dr;
        MySqlConnection con;

        //Creating from models for database to load into the models
        List<PostJob> job;
        List<TeklifVerenler> Teklif;
        PostJobdb dbop;
        TeklifVerenlerdb dbo;
        private readonly ILogger<JobController> _logger;
        
        public IActionResult PostJob()   //For getting postjob without it sending anything back
        {
            return View();
        }
        [HttpPost]
        public ActionResult TeklifSend([Bind] TeklifVerenler teklif) //To send offers
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string res = dbo.TeklifAdd(teklif);   //Uses TeklifVerenlerdb
                    TempData["msg"] = res;
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;             //The part where an error occurs a message popups
            }
            return View(TeklifSend);
        }

        [HttpPost]
        public IActionResult PostJob([Bind] PostJob job)  //Postjob kısmında değerler girince Postjobdb ile veritabanına veri aktaran modül
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string res = dbop.AddJob(job);       //Uses Postjobdb model
                    TempData["msg"] = res;
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;           //The part where an error occurs a message popups
            }
            return View();
        }
        private void FetchData()                       //Module for fetching data to show jobs
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,is_type,urun_zaman,urun_para FROM urunler"; //WHERE urun_status='1' gibi kod ekle. (for myself)
                dr = com.ExecuteReader();
                while (dr.Read())     //The part where reads the database data to use it however we want
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     // Data from database loading into a model with integer and string to show in frontend
                        urun_type = dr["urun_type"].ToString(),
                        is_type = dr["is_type"].ToString(),
                        urun_zaman = dr["urun_zaman"].ToString(),
                        urun_para = (int)dr["urun_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                                 //The part where an error occurs a message popups
            }

        }
        
        void connectionString()
        {
            con.ConnectionString = "server=localhost;uid=root;pwd=root;database=veritabani;";      //Connection string for mysql
        }

        public IActionResult ShowJob(string searchString)                //Search module
        {

            FetchData();
            if (!String.IsNullOrEmpty(searchString))
            {       //If the search is empty no search
                return View(job);
            }
            else
            {
                var SearchByJobName = job.Where(p => p.urun_adi == searchString).Select(p => p.urun_adi);  //The part where search occurs
                return View(job);
            }

        }
        public JobController(ILogger<JobController> logger)  //Controller summons so every running gets only 1 value 
        { 
            com = new MySqlCommand();
            con = new MySqlConnection();
            job = new List<PostJob>();
            Teklif = new List<TeklifVerenler>();             //Sql & database assesment into models
            dbop = new PostJobdb();
            _logger = logger;
            connectionString();
        }
    }
}
