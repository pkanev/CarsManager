namespace Client.Core.Models.Employees
{
    public class EmployeeModel : BasicEmployeeModel
    {
        public int TownId { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string ImageName { get; set; }
    }
}
