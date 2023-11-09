using System.ComponentModel.DataAnnotations;

namespace DemoApp_DemoAPI.Models.Dto
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
