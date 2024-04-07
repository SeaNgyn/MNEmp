using NPOI.HSSF.UserModel;
using WebFormL1.Models;

namespace WebFormL1.Utility
{
    public class ExcelFileExcute
    {
        public static List<Province> GetProvinces(string fileName)
        {
            List<Province> provinces = new List<Province>();
            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var workBook = new HSSFWorkbook(fs);
            var sheet = workBook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                Province province = new();
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    province.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    province.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    province.Level = cell.StringCellValue;
                                    break;
                            }
                        }
                    }
                }
                provinces.Add(province);
            }
            return provinces;
        }

        public static List<District> GetDistricts(string filename)
        {
            List<District> districts = new();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                District district = new();
                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    district.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    district.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    district.Level = cell.StringCellValue;
                                    break;
                                case 3:
                                    district.ProvinceId = int.Parse(cell.StringCellValue);
                                    break;
                            }
                        }
                    }
                }
                districts.Add(district);
            }
            return districts;
        }

        public static List<Commune> GetCommunes(string filename)
        {
            List<Commune> communes = new();
            using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            var sheet = workbook.GetSheetAt(0);
            for (int i = 1; i <= sheet.LastRowNum - 1; i++)
            {
                var row = sheet.GetRow(i);
                Commune commune = new();

                if (row != null)
                {
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    commune.Id = int.Parse(cell.StringCellValue);
                                    break;
                                case 1:
                                    commune.Name = cell.StringCellValue;
                                    break;
                                case 2:
                                    commune.Level = cell.StringCellValue;
                                    break;
                                case 3:
                                    commune.DistrictId = int.Parse(cell.StringCellValue);
                                    break;
                            }
                        }
                    }
                }
                communes.Add(commune);
            }
            return communes;
        }
    }
}
