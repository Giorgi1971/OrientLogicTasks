using System;
namespace GPACalculatorAPI.Db.Entity
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNubmer { get; set; }
        public string Course { get; set; }
    }
}
