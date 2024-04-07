using WebFormL1.Models;
using WebFormL1.Utility;

namespace WebFormL1.SeedData
{
    public class SeedAdministrativeCountrySubdivision
    {
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<Commune> Communes { get; set; }

        public SeedAdministrativeCountrySubdivision()
        {
            var fileNameProvince = "Province.xls";
            string pathProvince = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\", fileNameProvince);
            Provinces = ExcelFileExcute.GetProvinces(pathProvince);

            var fileNameDistrict = "District.xls";
            var pathDistrict = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\", fileNameDistrict);
            Districts = ExcelFileExcute.GetDistricts(pathDistrict);

            var fileNameCommune = "Commune.xls";
            var pathCommune = Path.Combine(Environment.CurrentDirectory, @"ExcelFile\", fileNameCommune);
            Communes = ExcelFileExcute.GetCommunes(pathCommune);
        }
    }
}
