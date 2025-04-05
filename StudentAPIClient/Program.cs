// See https://aka.ms/new-console-template for more information


using StudentAPIClient;
using System.Net.Http.Json;

class Program
{
    static string EndPointString = "http://localhost:5231/api/Students/";
    static readonly HttpClient httpClient = new HttpClient();
    static async Task Main()
    {
        httpClient.BaseAddress = new Uri(EndPointString);
        await GetAllStudents();
        await GetPassedStudents();
        await GetAverageGrade();
        await GetStudentByID();
    }
    static async Task GetAllStudents()
    {
        try
        {
            Console.WriteLine("Getting all students");
            var students = await httpClient.GetFromJsonAsync<List<Student>>("All");
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name},Age: {student.Age},Grade: {student.Grade}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
    static async Task GetPassedStudents()
    {
        try
        {
            Console.WriteLine("Getting Passed students");
            var students = await httpClient.GetFromJsonAsync<List<Student>>("Passed");
            if (students != null)
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name},Age: {student.Age},Grade: {student.Grade}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
    static async Task GetAverageGrade()
    {
        try
        {
            Console.WriteLine("Getting Average Grade");
            var averageGrade = await httpClient.GetFromJsonAsync<double>("AverageGrade");
            Console.WriteLine($"Average Grade: {averageGrade}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    static async Task GetStudentByID()
    {
        int StudentID = 6;
        try
        {
            Console.WriteLine($"Getting student With ID: {StudentID}");
            var student = await httpClient.GetFromJsonAsync<Student>($"{StudentID}");
            if (student != null)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name},Age: {student.Age},Grade: {student.Grade}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }

}