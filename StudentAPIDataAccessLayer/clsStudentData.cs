using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.Pkcs;
namespace StudentAPIDataAccessLayer
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
        public StudentDTO(int Id, string Name, int Age, int Grade)
        {
            this.Id = Id;
            this.Name = Name;
            this.Age = Age;
            this.Grade = Grade;
        }
    }
    public class clsStudentData
    {
        public static List<StudentDTO> GetAllStudents()
        {
            List<StudentDTO> students = new List<StudentDTO>();
            string query = "SP_GetAllStudents";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new StudentDTO(reader.GetInt32(reader.GetOrdinal("Id")),
                                                            reader.GetString(reader.GetOrdinal("Name")),
                                                            reader.GetInt32(reader.GetOrdinal("Age")),
                                                            reader.GetInt32(reader.GetOrdinal("Grade"))
                                            ));

                            }
                        }
                    }
                    return students;
                }
            }
            catch (Exception ex)
            {
                return students;
            }
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            List<StudentDTO> students = new List<StudentDTO>();
            string query = "SP_GetPassedStudents";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new StudentDTO(reader.GetInt32("Id"),
                                                            reader.GetString("Name"),
                                                            reader.GetInt32("Age"),
                                                            reader.GetInt32("Grade")
                                            ));

                            }
                        }
                    }
                    return students;
                }
            }
            catch (Exception ex)
            {
                return students;
            }
        }
        public static double GetAverageGrade()
        {
            string query = "SP_GetAverageGrade";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        return Convert.ToDouble(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static StudentDTO? GetStudentByID(int StudentID)
        {
            string query = "SP_GetStudentByID";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", StudentID);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetInt32(reader.GetOrdinal("Age")),
                                    reader.GetInt32(reader.GetOrdinal("Grade"))
                                 );
                            }
                            else
                            {
                                return null;
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool DeleteStudent(int StudentID)
        {
            string query = "SP_DeleteStudent";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", StudentID);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static int AddNewStudent(StudentDTO studentDTO)
        {
            string query = "SP_AddStudent";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", studentDTO.Name);
                        cmd.Parameters.AddWithValue("@Age", studentDTO.Age);
                        cmd.Parameters.AddWithValue("@Grade", studentDTO.Grade);
                        var outputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputIdParam);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static bool UpdateStudent(StudentDTO studentDTO)
        {
            string query = "SP_UpdateStudent";
            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", studentDTO.Id);
                        cmd.Parameters.AddWithValue("@Name", studentDTO.Name);
                        cmd.Parameters.AddWithValue("@Age", studentDTO.Age);
                        cmd.Parameters.AddWithValue("@Grade", studentDTO.Grade);
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
