namespace CarsManager.Application.Common.Constants
{
    public static class EmployeeConstants
    {
        public const string NAME_REGEX = @"^[a-zа-я\s\-]+$";
        public const string PHONE_REGEX = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
        public const int NAME_MAX_LENGTH = 100;
        public const int ADDRESS_MAX_LENGTH = 256;
    }
}
