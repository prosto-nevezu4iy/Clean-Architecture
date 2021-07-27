using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
