using NPOI.SS.Formula.Functions;

namespace WebFormL1.Utility
{
    public class Paging<T>
    {
        public int TotalPages { get; set; }
        public List<T>? ViewModel { get; set; }
        public Paging(int totalPages, List<T> viewModel)
        {
            TotalPages = totalPages;
            ViewModel = viewModel;
        }
    }
}
