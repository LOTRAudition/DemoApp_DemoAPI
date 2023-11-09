using DemoApp_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoApp_Web.Models.VM
{
    public class DemoNumberCreateVM
    {
        public DemoNumberCreateVM()
        {
            DemoNumber = new DemoNumberCreateDTO();
        }
        public DemoNumberCreateDTO DemoNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DemoList { get; set; }
    }
}
