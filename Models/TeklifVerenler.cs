
namespace Proje.Models
{
    public class TeklifVerenler
    {
        //The model that includes necessary info for offers
        public int urun_id { get; set; }
        public string urun_adi { get; set; }
        public int urun_para { get; set; }
        public int kisi_id { get; set; }
        public int teklif_status { get; set; }
        public int teklif_id { get; set; }
        public int teklif_para { get; set; }

        
    }
}
