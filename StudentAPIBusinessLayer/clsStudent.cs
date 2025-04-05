using Microsoft.VisualBasic;
using StudentAPIDataAccessLayer;

namespace StudentAPIBusinessLayer
{
    public class clsStudent
    {
        public enum enMode { AddNew, Update };
        enMode _Mode;
        public StudentDTO SDTO
        {
            get
            {
                return new StudentDTO(this.ID, this.Name, this.Age, this.Grade);
            }
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
        public clsStudent(StudentDTO studentDTO, enMode mode)
        {
            this.ID = studentDTO.Id;
            this.Name = studentDTO.Name;
            this.Age = studentDTO.Age;
            this.Grade = studentDTO.Grade;
            this._Mode = mode;
        }
        public static List<StudentDTO> GetAllStudents()
        {
            return clsStudentData.GetAllStudents();
        }
        public static List<StudentDTO> GetPassedStudents()
        {
            return clsStudentData.GetPassedStudents();
        }
        public static double GetAverageGrade()
        {
            return clsStudentData.GetAverageGrade();
        }
        public static clsStudent? Find(int ID)
        {
            var studentDTO = clsStudentData.GetStudentByID(ID);
            if (studentDTO != null)
            {
                return new clsStudent(studentDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public static bool DeleteStudent(int ID)
        {
            return clsStudentData.DeleteStudent(ID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        int id = clsStudentData.AddNewStudent(this.SDTO);
                        if (id > 0)
                        {
                            this.ID = id;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                case enMode.Update:
                    return clsStudentData.UpdateStudent(this.SDTO);
                default:
                    return false;
            }
        }
    }
}
