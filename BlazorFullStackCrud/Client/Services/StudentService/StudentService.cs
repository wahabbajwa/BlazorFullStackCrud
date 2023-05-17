using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public class StudentService : IStudentService
    {
        
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public StudentService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Student> Student { get; set; } = new List<Student>();
        public List<Department> Department { get; set; } = new List<Department>();

        public async Task CreateStudent(Student hero)
        {
            var result = await _http.PostAsJsonAsync("api/Student", hero);
            await SetStudent(result);
        }

        private async Task SetStudent(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Student>>();
            Student = response;
            _navigationManager.NavigateTo("Student");

        }

        public async Task DeleteStudent(int id)
        {
            var result = await _http.DeleteAsync($"api/student/{id}");
            await SetStudent(result);
        }

        public async Task GetDepartment()
        {
            var result = await _http.GetFromJsonAsync<List<Department>>("api/student/department");
            if (result != null)
                Department = result;
        }

        public async Task<Student> GetSingleStudent(int id)
        {
            var result = await _http.GetFromJsonAsync<Student>($"api/student/{id}");
            if (result != null)
                return result;
            throw new Exception("Student not found!");
        }

        public async Task GetStudent()
        {
            var result = await _http.GetFromJsonAsync<List<Student>>("api/student");
            if (result != null)
               Student = result;
        }

        public async Task UpdateStudent(Student hero)
        {
            var result = await _http.PutAsJsonAsync($"api/superhero/{hero.Id}", hero);
            await SetStudent(result);
        }
    }
}
