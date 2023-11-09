using DemoApp_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoApp_Web.Models.VM
{
    public class DemoNumberUpdateVM
    {
        public DemoNumberUpdateVM()
        {
            DemoNumber = new DemoNumberUpdateDTO();
        }
        public DemoNumberUpdateDTO DemoNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DemoList { get; set; }
    }
}
