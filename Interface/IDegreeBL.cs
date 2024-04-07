using WebFormL1.EditModel;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.Interface
{
    public interface IDegreeBL
    {
        List<DegreeViewModel> FetchDegree(int employeeId);
        int GetNumberOfDegrees(int employeeId);
        Task<bool> AddDegree(DegreeEditModel degreeEditModel);
        Task<bool> EditDegree(DegreeEditModel degreeEditModel);
        Task<bool> DeleteDegree(Degree degree);
        DegreeEditModel SetDegreeEditModel(Degree degree);
        Task<Degree?> GetDegreeById(int? id);
        DegreeViewModel SetDegreeViewModelDelete(Degree degree);
        Task<Employee?> GetEmployeeById(int employeeId);
    }
}
