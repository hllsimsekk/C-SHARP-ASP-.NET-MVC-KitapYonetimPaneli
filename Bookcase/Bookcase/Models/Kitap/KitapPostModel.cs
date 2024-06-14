using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookcase.Models.Kitap.PostModel
{
    public class KitapListePostModel : IValidatableObject
    {
        //[Required]
        [StringLength(255, ErrorMessage = "Kitap adı 255 karakterden fazla olamaz")]
        public string kitap_ad { get; set; }


        public int start { get; set; }
        public int length { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = [];
            return results;
        }
    }

    public class KitapOkuPostModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "id değeri sıfırdan büyük bir sayı olmalıdır")]
        public int? id { get; set; }
    }

    public class KitapEklePostModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "Kitap adı 50 karakterden fazla olamaz")]
        public string kitapAd { get; set; }

        [Required]
        public decimal fiyat { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Yazar adı 50 karakterden fazla olamaz")]
        public string yazarAd { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Kitap adı 50 karakterden fazla olamaz")]
        public string kategoriAd { get; set; }
    }

    public class KitapSilPostModel
    {
        [Required]
        public int kitapId { get; set; }
    }
}