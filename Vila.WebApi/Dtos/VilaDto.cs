using System.ComponentModel.DataAnnotations;
using Vila.WebApi.ModelValidation;

namespace Vila.WebApi.Dtos
{
    public class VilaDto
    {
        public int VilaId { get; set; }
        [Required(ErrorMessage ="نام ویلا اجباری می باشد")]
        [MaxLength(100,ErrorMessage = "نام ویلا نباید بیش از 100 حرف باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = "استان ویلا را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "استان ویلا نباید بیش از 100 حرف باشد")]
        public string State { get; set; }
        [Required(ErrorMessage = "شهر ویلا را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "شهر ویلا نباید بیش از 100 حرف باشد")]
        public string City { get; set; }
        [Required(ErrorMessage = "آدرس ویلا را وارد نمایید")]
        [MaxLength(500, ErrorMessage = "آدرس ویلا نباید بیش از 500 حرف باشد")]
        public string Address { get; set; }
        [Required(ErrorMessage = "موبایل صاحب ویلا را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "شماره موبایل نباید بیشتر از 11 عدد باشد")]
        [MinLength(11, ErrorMessage = "شماره موبایل ویلا نباید کمتر از 11 عدد باشد")]
        public string Mobile { get; set; }
        [Required]
        [DateValidation]
        public string BuildDate { get; set; }
    }
}
