using WebFormL1.EditModel;
using WebFormL1.Interface;
using WebFormL1.Models;
using WebFormL1.ViewModel;

namespace WebFormL1.BusinessLogic
{
    public class DegreeBL : IDegreeBL
    {
        private readonly IDegreeService _service;
        public DegreeBL(IDegreeService service)
        {
            _service = service;
        }

        public List<DegreeViewModel> FetchDegree(int employeeId)
        {
            var degrees = _service.GetAll(employeeId);
            var degreesViewModel = _service.GetPagedDegreeViewModels(degrees!).ToList();

            return new List<DegreeViewModel>(degreesViewModel);
        }

        public int GetNumberOfDegrees(int employeeId)
        {
            return _service.GetDegreeCountForEmployee(employeeId);
        }

        public async Task<bool> AddDegree(DegreeEditModel degreeEditModel)
        {
            try
            {
                var degree = new Degree(degreeEditModel.Id, degreeEditModel.Name!, degreeEditModel.DateOfIssue,
                                   degreeEditModel.ProvinceId, degreeEditModel.ExpirationDate, degreeEditModel.EmployeeId);

                _service.BaseService().Insert(degree);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditDegree(DegreeEditModel degreeEditModel)
        {
            try
            {
                var degree = new Degree(degreeEditModel.Id, degreeEditModel.Name!, degreeEditModel.DateOfIssue,
                                    degreeEditModel.ProvinceId, degreeEditModel.ExpirationDate, degreeEditModel.EmployeeId);

                _service.BaseService().Update(degree);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDegree(Degree degree)
        {
            try
            {
                _service.BaseService().Delete(degree);
                if (await _service.BaseService().SaveChangeAsync() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public DegreeEditModel SetDegreeEditModel(Degree degree)
        {
            return new DegreeEditModel(degree.ID, degree.Name!, degree.DateOfIssue,
                                                 degree.ProvinceId, degree.ExpirationDate, degree.EmployeeId);
        }

        public async Task<Degree?> GetDegreeById(int? id)
        {
            return await _service.GetDegreeById(id);
        }

        public DegreeViewModel SetDegreeViewModelDelete(Degree degree)
        {
            return new DegreeViewModel(degree.ID, degree.Name!, degree.DateOfIssue,
                                                        degree.Province!.Name!, degree.ExpirationDate,
                                                        degree.Employee!.Name!, degree.EmployeeId);
        }
        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            return await _service.GetEmployeeById(employeeId);
        }
    }
}
