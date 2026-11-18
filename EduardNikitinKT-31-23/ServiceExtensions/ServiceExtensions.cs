using EduardNikitinKT_31_23.Interfaces.GradeInterfaces;
using EduardNikitinKT_31_23.Interfaces.GroupInterfaces;
using EduardNikitinKT_31_23.Interfaces.StudentInterfaces;

namespace EduardNikitinKT_31_23.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IGroupService, GroupService>();

            return services;
        }
    }
}
