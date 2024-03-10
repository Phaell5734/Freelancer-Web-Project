namespace Proje.Models
{
    public class Profile
    {
        //Information on person to show it on profile page
        public string kisi_id {  get; set; }
        public string kisi_email { get; set; }
        public string kisi_isim { get; set; }
        public string kisi_soyisim { get; set; }
        public int kisi_para { get; set; }
        //Information on project to show it on profile page
        public int urun_id { get; set; }
        public string urun_adi { get; set; }
        public int urun_para { get; set; }
        public string is_type { get; set; }
        public string satin_alinan_id { get; set; }
        public string urun_aciklamasi { get; set; }
        public string urun_type { get; set; }
        public string urun_zaman { get; set; }
        //Information at offers to show it on profile page
        public int teklif_id { get; set; }
        public int teklif_para { get; set; }




    }
}
