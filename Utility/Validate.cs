namespace WebFormL1.Utility
{
    public class Validate
    {
        public static int ValidatePageNumber(ref int? pageNumber)
        {
            return pageNumber is null or <= 1 ? 1 : pageNumber.Value;
        }
        public static int ValidatePageSize(ref int? pageSize)
        {
            return pageSize is null ? 5 : pageSize.Value;
        }


    }
}
