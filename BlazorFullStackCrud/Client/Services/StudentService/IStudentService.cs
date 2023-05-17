using BlazorFullStackCrud.Shared;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public interface IStudentService
    {
        List<Student> Student { get; set; }
        List<Department> Department { get; set; }
        Task GetDepartment();
        Task GetStudent();
        Task<Student> GetSingleStudent(int id);
        Task CreateStudent(Student hero);
        Task UpdateStudent(Student hero);
        Task DeleteStudent(int id);
    }
}
