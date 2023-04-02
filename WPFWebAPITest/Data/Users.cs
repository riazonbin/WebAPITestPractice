
namespace WPFWebAPITest.Data
{
    public class Users
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public int Role_Id { get; set; }

        public virtual Roles Roles { get; set; }
    }
}