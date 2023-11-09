using DemoApp_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoApp_Web.Models.VM
{
    public class DemoNumberDeleteVM
    {
        public DemoNumberDeleteVM()
        {
            DemoNumber = new DemoNumberDTO();
        }
        public DemoNumberDTO DemoNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DemoList { get; set; }
    }
}
