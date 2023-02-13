
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFirstExample.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Remote(action: "HasProductName", controller: "Products")]
        [Required(ErrorMessage = "İsim alanı boş olamaz!")]
        [StringLength(50, ErrorMessage = "İsim alanına en fazla 50 karakter girilebilir!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Fiyat alanı boş olamaz!")]
        [Range(1, 1000, ErrorMessage = "Fiyat alanı 1 ile 1000 arasında bir değer olmalıdır!")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})" ,ErrorMessage="Fiyat alanında noktadan sonra en fazla 2 basamak olmalıdır!")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Stok alanı boş olamaz!")]
        [Range(1, 200, ErrorMessage = "Stok alanı 1 ile 200 arasında bir değer olmalıdır!")]
        public int? Stock { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş olamaz!")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Açıklama alanına 10 ile 300 arası karakter girilebilir!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Renk seçimi boş olamaz!")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Yayınlanma tarihi boş olamaz!")]
        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
        [Required(ErrorMessage = "Yayınlanma süresi boş olamaz!")]
        public int? Expire { get; set; }
        //[Required(ErrorMessage = "E-Posta alanı boş olamaz!")]
        //[EmailAddress(ErrorMessage = "E-Posta adresi uygun formatta değil!")]
        //public String EmailAddress{ get; set; }
    }
}
