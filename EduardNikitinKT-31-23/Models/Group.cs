namespace EduardNikitinKT_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public List<Student> Students { get; set; } = [];
    }
}
