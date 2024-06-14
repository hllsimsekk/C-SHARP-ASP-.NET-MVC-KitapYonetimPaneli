namespace Bookcase.Models.Kitap.Result
{

    public class KitapListeResultModel
    {
        public int id { get; set; }
        public string ad { get; set; }
        public decimal fiyat { get; set; }
        public string yazar_ad { get; set; }
        public string kategori_ad { get; set; }
    }


    public class KitapOkuResultModel
    {
        public int id { get; set; }
        public string ad { get; set; }
        public decimal fiyat { get; set; }
        public int yazar_id { get; set; }
        public int kategori_id { get; set; }
    }

    public class KitapKategoriListeResultModel
    {
        public int id { get; set; }
        public string kategori_ad { get; set; }
    }

    public class KitapYazarListeResultModel
    {
        public int id { get; set; }
        public string yazar_ad { get; set; }
    }

    public class islemSonuc
    {
        public int id { get; set; }
        public short sonuc_id { get; set; }
        public string message { get; set; }
    }
}

