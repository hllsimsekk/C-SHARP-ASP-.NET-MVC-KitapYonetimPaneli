using Bookcase.Models.Kitap.PostModel;
using Bookcase.Models.Kitap.Result;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Bookcase.Models.Kitap
{

    public class KitapModel : IKitapModel
    {
        private NpgsqlCommand cmd { get; set; }
        private NpgsqlConnection con { get; set; }


        public KitapModel()
        {
            con = new NpgsqlConnection("Server=localhost;Port=5432;Database=BookcaseDB;User Id = bookcase_user; Password=Aa123456*");
            con.Open();

            cmd = new NpgsqlCommand
            {
                Connection = con,
                CommandTimeout = 600
            };
        }

        public List<KitapListeResultModel> KitapListe(KitapListePostModel PostModel)
        {
            List<KitapListeResultModel> kitapList = [];


            cmd.CommandText = "select * from kitap.kitap_liste(p_ad=>@p_ad)";

            cmd.Parameters.Add(new NpgsqlParameter()
            {
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar,
                Direction = ParameterDirection.Input,
                ParameterName = "@p_ad",
                Value = PostModel.kitap_ad == null ? DBNull.Value : PostModel.kitap_ad
            });

            DataTable dt = new();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                KitapListeResultModel kitap = new()
                {
                    id = Convert.ToInt32(row["id"]),
                    ad = row["ad"].ToString(),
                    fiyat = Convert.ToDecimal(row["fiyat"]),
                    yazar_ad = row["yazar_ad"].ToString(),
                    kategori_ad = row["kategori_ad"].ToString()
                };

                kitapList.Add(kitap);
            }

            return kitapList;
        }

        public KitapOkuResultModel KitapOku(KitapOkuPostModel PostModel)
        {
            KitapOkuResultModel kitapDetay = null;

            cmd.CommandText = "select * from kitap.kitap_oku(p_id=>@p_id)";

            cmd.Parameters.Add(new NpgsqlParameter()
            {
                NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer,
                Direction = ParameterDirection.Input,
                ParameterName = "@p_id",
                Value = PostModel.id == null ? DBNull.Value : PostModel.id
            });

            DataTable dt = new();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                kitapDetay = new()
                {
                    id = Convert.ToInt32(row["id"]),
                    ad = row["ad"].ToString(),
                    fiyat = Convert.ToDecimal(row["fiyat"]),
                    yazar_id = Convert.ToInt32(row["yazar_id"]),
                    kategori_id = Convert.ToInt32(row["kategori_id"])
                };

            }

            return kitapDetay;
        }

        public List<KitapKategoriListeResultModel> KitapKategoriListe()
        {
            List<KitapKategoriListeResultModel> list = [];

            cmd.CommandText = "select * from kitap.kitap_kategori_liste()";

            DataTable dt = new();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new()
                {
                    id = Convert.ToInt32(row["id"]),
                    kategori_ad = row["kategori_ad"].ToString()
                });

            }

            return list;
        }
        public List<KitapYazarListeResultModel> KitapYazarListe()
        {
            List<KitapYazarListeResultModel> list = new List<KitapYazarListeResultModel>();

            cmd.CommandText = "SELECT * from kitap.kitap_yazar_liste()";

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new KitapYazarListeResultModel
                {
                    id = Convert.ToInt32(row["id"]),
                    yazar_ad = row["yazar_ad"].ToString()
                });
            }

            return list;
        }

        public islemSonuc KitapEkle(KitapEklePostModel postModel)
        {
            islemSonuc _result = new islemSonuc();

            cmd.CommandText = "SELECT * FROM kitap.kitap_ekle(@p_ad, @p_fiyat, @p_yazar_ad, @p_kategori_ad)";

            cmd.Parameters.AddWithValue("@p_ad", string.IsNullOrEmpty(postModel.kitapAd) ? (object)DBNull.Value : postModel.kitapAd);
            cmd.Parameters.AddWithValue("@p_fiyat", postModel.fiyat);
            cmd.Parameters.AddWithValue("@p_yazar_ad", string.IsNullOrEmpty(postModel.yazarAd) ? (object)DBNull.Value : postModel.yazarAd);
            cmd.Parameters.AddWithValue("@p_kategori_ad", string.IsNullOrEmpty(postModel.kategoriAd) ? (object)DBNull.Value : postModel.kategoriAd);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                if (reader.Read())
                {
                    _result.sonuc_id = Convert.ToInt16(reader["sonuc_id"]);
                    _result.id = Convert.ToInt32(reader["id"]);
                    _result.message = reader["message"].ToString();
                }
            }

            con.Close();

            return _result;
        }


        public islemSonuc KitapSil(KitapSilPostModel postModel)
        {
            islemSonuc _result = new islemSonuc();
            cmd.CommandText = "SELECT * FROM kitap.kitap_sil(p_kitap_id=>@p_kitap_id)";

            cmd.Parameters.Add(new NpgsqlParameter("@p_kitap_id", NpgsqlDbType.Integer)).Value = postModel.kitapId;

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    _result.sonuc_id = Convert.ToInt16(reader["sonuc_id"]);
                    _result.id = Convert.ToInt32(reader["id"]);
                    _result.message = reader["message"].ToString();
                }
            }

            con.Close();

            return _result;
        }

    }
}
