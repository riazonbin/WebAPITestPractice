
namespace WPFWebAPITest.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Role_Id { get; set; }

        public virtual Role Role { get; set; }
    }
}