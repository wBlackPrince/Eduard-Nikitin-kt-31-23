namespace EduardNikitinKT_31_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = string.Empty;

        public int Course { get; set; }

        public int SpecialityId { get; set; }

        public List<Student> Students { get; set; } = [];
    }
}
