using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskTreeMD.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
