namespace WebFormL1.Constant
{
    public class ConstantName
    {
        public const string PhonePattern = @"^(\+[0-9]{1,3}( )?)?(([0-9]{1,3})|\([0-9]{1,3}\))[- .]?[0-9]{3,4}[- .]?[0-9]{4}$";
        public const string IndentityPattern = "^[0-9]{12}";
        public const string NamePattern = "^(?![0-9]*$).{3,100}$";
        public const int MinYearOfBirthDate = 1950;
        public const int MinMonthOfBirthDate = 1;
        public const int MinDayOfBirthDate = 1;
        public const int MaxDay = 31;
        public const int MaxMonth = 12;
        public const int MaxYear = 2100;
        public const int PageSize = 10;
    }
}
