using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebFormL1.Models;

namespace WebFormL1.Controllers
{
    public class BaseController : Controller
    {
        public List<SelectListItem> GetPageSizeOptions(int ?size)
        {
            List<SelectListItem> items = new List<SelectListItem>
                {
                    new SelectListItem { Text = "5", Value = "5" },
                    new SelectListItem { Text = "10", Value = "10" },
                    new SelectListItem { Text = "20", Value = "20" },
                    new SelectListItem { Text = "25", Value = "25" },
                    new SelectListItem { Text = "50", Value = "50" },
                    new SelectListItem { Text = "100", Value = "100" },
                    new SelectListItem { Text = "200", Value = "200" }
                };
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            return items;
        }
        public int ValidateImportEmployee(List<Employee> importEmployees)
        {
            try
            {
                var i = 1;
                foreach (var employee in importEmployees)
                {
                    TryValidateModel(employee!);
                    if (!ModelState.IsValid)
                    {
                        string errors = JsonConvert.SerializeObject(ModelState.Values
                                                .SelectMany(state => state.Errors)
                                                .Select(error => error.ErrorMessage));
                        ViewData["ImportError"] = $"Dữ liệu không hợp lệ! Kiểm tra bản ghi thứ {i}:\n{errors}";
                        return i;
                    }
                    i++;
                }
                return 0;
            }
            catch (Exception ex)
            {
                ViewData["ImportError"] = ex.Message;
                TempData["ErrorMessage"] = "Lỗi xử lý dữ liệu";
                return -1;
            }

        }
    }
}

