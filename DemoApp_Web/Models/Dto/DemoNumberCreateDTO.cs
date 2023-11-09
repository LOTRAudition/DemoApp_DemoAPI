using System.ComponentModel.DataAnnotations;

namespace DemoApp_Web.Models.Dto
{
    public class DemoNumberCreateDTO
    {
        [Required]
        public int DemoNo { get; set; }
        [Required]
        public int DemoID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
