using System.Collections.Generic;
using Client.Core.Models.Employees;

namespace Client.Core.Dtos
{
    public class GetEmployeesDto
    {
        public IList<BasicEmployeeModel> Employees { get; set; } = new List<BasicEmployeeModel>();
    }
}
