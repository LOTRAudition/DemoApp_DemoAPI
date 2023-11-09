using System.ComponentModel.DataAnnotations;

namespace DemoApp_DemoAPI.Models.Dto
{
    public class DemoNumberUpdateDTO
    {
        [Required]
        public int DemoNo { get; set; }
        [Required]
        public int DemoID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
