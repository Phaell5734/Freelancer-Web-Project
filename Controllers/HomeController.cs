using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proje.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)         //Controller summons so every running gets only 1 value 
        {
            com = new MySqlCommand();
            con = new MySqlConnection();
            kisibilgileri = new List<Profile>();                       //Sql & database assesment into models
            job = new List<PostJob>();
            _logger = logger;
            connectionString();
        }

        public IActionResult Index()  
        {
            return View();
        }
        public IActionResult applicantPick()
        {
            return View();
        }
        //Codes for mysql connections
        MySqlCommand com;
        MySqlDataReader dr;
        MySqlConnection con;
        //Creating from models for database to load into the models
        List<Profile> kisibilgileri;
        List<PostJob> job;
        List<TeklifVerenler> teklif;
        UpdateTeklifdb dbo;

        [Authorize]
        //Profile View Module
        public IActionResult Profile()
        {
            FetchPersonData();
            return View();
        }
        [Authorize]
        public IActionResult View1()
        {
            FetchTeklifData();
            return View();
        }
        [Authorize]
        public IActionResult View2()
        {
            FetchView2Data();
            return View();
        }
        [Authorize]
        public IActionResult View3()
        {
            FetchView3Data();
            return View();
        }
        [Authorize]
        public IActionResult View4()
        {
            FetchView4Data();
            return View();
        }
        [Authorize]
        public IActionResult View5()
        {
            FetchView5Data();
            return View();
        }

        private void FetchPersonData() //Getting Profile Info
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT Id,kisi_isim,kisi_soyisim,kisi_para,Email FROM aspnetusers";
                dr = com.ExecuteReader();                                                                //Getting profile info from database
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        kisi_id = dr["Id"].ToString(),
                        kisi_isim = dr["kisi_isim"].ToString(),
                        kisi_soyisim = dr["kisi_soyisim"].ToString(),
                        kisi_email = dr["kisi_email"]?.ToString(),        //The part where profile info getting pulled from database into a model
                        kisi_para = (int)dr["kisi_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)  //Exception to see what is going wrong
            {

               throw ex;
            }

        }
        public ActionResult TeklifOnay([Bind] TeklifVerenler teklif) //Offer Accept Module
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string res = dbo.TeklifOnay(teklif);   //Uses TeklifVerenlerdb
                    TempData["msg"] = res;
                }

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;             //The part where an error occurs a message popups
            }
            return View(TeklifOnay);
        }

        private void FetchTeklifData()  //The module for fetching offer data
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_para,Id,teklif_id,teklif_para FROM view_teklifler";
                dr = com.ExecuteReader();  //Takes offers from database and reads it so we can use however we want
                while (dr.Read())
                {
                    kisibilgileri.Add(new Profile()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_para = (int)dr["urun_para"],             //Offers getting loaded into a created model so we can show them in frontend
                        kisi_id = dr["Id"].ToString(),
                        teklif_id = (int)dr["teklif_id"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)     //Exception to see what is going wrong
            {
                throw ex;
            }

        }
        void connectionString()
        {
            con.ConnectionString = "server=localhost;uid=root;pwd=root;database=veritabani;";
        }
        private void FetchView2Data()                       //Module for fetching data to show jobs
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM view_satilan_urunler";
                dr = com.ExecuteReader();
                while (dr.Read())     //The part where reads the database data to use it however we want
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Data from database loading into a model with integer and string to show in frontend
                        urun_type = dr["urun_type"].ToString(),
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
        private void FetchView3Data()  //The module that shows taken projects
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT Id,urun_id,urun_adi,urun_aciklamasi,teklif_id,teklif_para FROM view_satin_alinan_urunler";
                dr = com.ExecuteReader();
                while (dr.Read())        //The part where reads the database data to use it however we want
                {
                    kisibilgileri.Add(new Profile()
                    {
                        kisi_id = dr["Id"].ToString(),
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),                               //Data from database loading into a model with integer and string to show in frontend
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),
                        teklif_id = (int)dr["teklif_id"],
                        teklif_para = (int)dr["teklif_para"]
                    });
                }
                con.Close();
            }
            catch (Exception ex)      //Exception to see what is going wrong
            {
                throw ex;
            }

        }
        
        private void FetchView4Data()                       //xxxxx
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM view_online_isler";
                dr = com.ExecuteReader();
                while (dr.Read())     //The part where reads the database data to use it however we want
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Data from database loading into a model with integer and string to show in frontend
                        urun_type = dr["urun_type"].ToString(),
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
        private void FetchView5Data()                       //xxxxx
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT urun_id,urun_adi,urun_aciklamasi,urun_type,urun_zaman,urun_para FROM view_fiziksel_isler";
                dr = com.ExecuteReader();
                while (dr.Read())     //The part where reads the database data to use it however we want
                {
                    job.Add(new PostJob()
                    {
                        urun_id = (int)dr["urun_id"],
                        urun_adi = dr["urun_adi"].ToString(),
                        urun_aciklamasi = dr["urun_aciklamasi"].ToString(),     //Data from database loading into a model with integer and string to show in frontend
                        urun_type = dr["urun_type"].ToString(),
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
