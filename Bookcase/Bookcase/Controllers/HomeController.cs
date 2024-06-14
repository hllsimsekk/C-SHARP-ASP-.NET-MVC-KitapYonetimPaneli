using Bookcase.Models;
using Bookcase.Models.Kitap;
using Bookcase.Models.Kitap.PostModel;
using Bookcase.Models.Kitap.Result;
using Microsoft.AspNetCore.Mvc;

namespace Bookcase.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult kitapListe(KitapListePostModel postModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                    //                                      .Select(e => e.ErrorMessage)
                    //                                      .ToList();

                    //return Json(new { success = false, errors = errorMessages });
                }

                IKitapModel kitapModel = new KitapModel();
                List<KitapListeResultModel> kitapList = kitapModel.KitapListe(postModel);

                /*paging*/

                var queryResultPage = kitapList
                  .Skip(postModel.length * postModel.start / postModel.length)
                  .Take(postModel.length);

                var _return = new DataGridReturnModel()
                {
                    recordsTotal = kitapList.Count,
                    recordsFiltered = kitapList.Count,
                    data = queryResultPage,
                };

                return Json(_return);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult KitapOku(KitapOkuPostModel postModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                    //                                      .Select(e => e.ErrorMessage)
                    //                                      .ToList();

                    //return Json(new { success = false, errors = errorMessages });
                }

                IKitapModel kitapModel = new KitapModel();
                var kitapDetay = kitapModel.KitapOku(postModel);

                return Json(kitapDetay);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult KitapKategoriListe()
        {
            try
            {
                IKitapModel kitapModel = new KitapModel();
                var kategoriListesi = kitapModel.KitapKategoriListe();

                return Json(kategoriListesi);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult KitapYazarListe()
        {
            try
            {
                IKitapModel kitapModel = new KitapModel();
                var yazarListesi = kitapModel.KitapYazarListe();

                return Json(yazarListesi);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult KitapEkle(KitapEklePostModel postModel)
        {
            Console.WriteLine("Veriler: " + postModel);

            if (!ModelState.IsValid)
            {
                return Json(new { sonuc_id = 0, id = 0, message = "Geçersiz model. Lütfen tüm alanlarý doðru þekilde doldurun." });
            }

            IKitapModel kitapModel = new KitapModel();
            Models.islemSonuc result = kitapModel.KitapEkle(postModel);

            if (result.sonuc_id == 1)
            {
                return Json(new { sonuc_id = result.sonuc_id, id = result.id, message = "Kitap baþarýyla eklendi." });
            }
            else
            {
                return Json(new { sonuc_id = result.sonuc_id, id = result.id, message = "Kitap eklenirken bir hata oluþtu. Detaylar: " + result.message });
            }
        }

        [HttpPost]
        public JsonResult KitapSil(KitapSilPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { sonuc_id = 0, id = 0, message = "Geçersiz model. Lütfen tüm alanlarý doðru þekilde doldurun." });
            }

            IKitapModel kitapModel = new KitapModel();
            Models.islemSonuc result = kitapModel.KitapSil(postModel);

            if (result.sonuc_id == 1)
            {
                return Json(new { sonuc_id = result.sonuc_id, id = result.id, message = "Kitap baþarýyla silindi." });
            }
            else
            {
                return Json(new { sonuc_id = result.sonuc_id, id = result.id, message = "Kitap silinirken bir hata oluþtu. Detaylar: " + result.message });
            }
        }
    }
}