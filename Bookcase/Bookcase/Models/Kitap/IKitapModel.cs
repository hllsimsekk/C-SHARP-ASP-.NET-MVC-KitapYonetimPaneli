using Bookcase.Models.Kitap.PostModel;
using Bookcase.Models.Kitap.Result;
using System.Data;

namespace Bookcase.Models.Kitap
{
    public interface IKitapModel
    {
        List<KitapListeResultModel> KitapListe(KitapListePostModel postModel);
        KitapOkuResultModel KitapOku(KitapOkuPostModel PostModel);
        List<KitapKategoriListeResultModel> KitapKategoriListe();
        List<KitapYazarListeResultModel> KitapYazarListe();
        islemSonuc KitapEkle(KitapEklePostModel postModel);
        islemSonuc KitapSil(KitapSilPostModel postModel);
    }
}
