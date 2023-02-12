using System.Runtime.Serialization;

namespace P_4_BonusManagement.Repositories
{
    internal class EmployeeNotFoundException : Exception
    {
        public int EmployeeId { get; }
        public EmployeeNotFoundException()
        {
        }

        public EmployeeNotFoundException(int employeeId, string? message) : base(message)
        {
            EmployeeId = employeeId;
        }
    }
}