using Microsoft.Data.SqlClient;
using MajorsController;

namespace StudentsController {
    public class StudentController {

        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public void OpenConnection() {
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
            if (SqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open :(");
            }
        }
        public void CloseConnection() {
            SqlConnection.Close();
        }
        public List<Student> GetAllStudents() {
            //add check to make sure connection is established
            var students = new List<Student>(65);//could add num in () to give function an idea of how many items will be passed it, helps to speed it up
            var sql = $"SELECT *, m.ID 'MajorPK' FROM STUDENT S join Major M on M.ID = S.MajorID;";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) { //check to see if reader is still reading, while it is reading, do this
                var student = new Student(); //create new instance of student to read a row, then stick into this instance, then add instance to collection, repeat then send collection to user
                student.ID = Convert.ToInt32(reader["ID"]);//how we pull value out of column for row workign on, evaluates to object, not an int, so have to convert from base object into int
                student.Firstname = Convert.ToString(reader["Firstname"]);
                student.Lastname = Convert.ToString(reader["Lastname"]);
                student.StateCode = Convert.ToString(reader["StateCode"]);
                student.SAT = (reader["SAT"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["SAT"]); //use ternary to check if value is null in sql so if null it converts to c#null
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                student.MajorID = (reader["MajorID"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["MajorID"]);
                //if not doing a join this is everything, but since doing an inner join will also have info for major
                var major = new Major();
                major.ID = Convert.ToInt32(reader["MajorPK"]);
                major.Code = Convert.ToString(reader["Code"]);
                major.Description = Convert.ToString(reader["Description"]);
                major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

                student.Major = major;
                students.Add(student);
            }
            reader.Close();
            SqlConnection.Close();
            return students;
        }
        public Student GetStudentByPK(int studentID) {
            var sql = $"SELECT *, m.ID 'MajorPK' FROM STUDENT s left join Major M on M.ID = S.MajorID where s.ID = {studentID};";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (!reader.HasRows) {
                reader.Close();
                SqlConnection.Close();
                return null;
            }
            var student = new Student();
            student.ID = Convert.ToInt32(reader["ID"]);
            student.Firstname = Convert.ToString(reader["Firstname"]);
            student.Lastname = Convert.ToString(reader["Lastname"]);
            student.StateCode = Convert.ToString(reader["StateCode"]);
            student.SAT = (reader["SAT"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDecimal(reader["GPA"]);
            student.MajorID = (reader["MajorID"] == System.DBNull.Value) ? (int?)null : Convert.ToInt32(reader["MajorID"]);
            if (student.MajorID is null) { //if major in sql is null, converts to c# null and returns student without major
                student.Major = null;
                reader.Close();
                SqlConnection.Close();
                return student;
            }
            var major = new Major();//if major not null, will do the rest of this to fill out major info
            major.ID = Convert.ToInt32(reader["MajorPK"]);
            major.Code = Convert.ToString(reader["Code"]);
            major.Description = Convert.ToString(reader["Description"]);
            major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

            student.Major = major;


            reader.Close();
            SqlConnection.Close();
            return student;

        }
        public bool AddStudent(Student student) { //would create new empty instance of student, fill it with data, then pass the instance into this method
            //check that connection is established
            var sql = "INSERT STUDENT" + " (Firstname, Lastname, StateCode, SAT, GPA, MajorID)" //must have () around column lists, sql requirement, 
                + " VALUES" + $"( '{student.Firstname}', '{student.Lastname}', '{student.StateCode}', {student.SAT}, {student.GPA}, {student.MajorID})"; //sql ' ' around strings
            //be mindful to make sure there are spaces between concatenated pieces in above
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Insert failed :(");
            }
            Console.WriteLine("Student successfully added");
            return true;
        }
        public bool UpdateStudent(Student student) {
            var sql = "UPDATE STUDENT SET"
                + $" Firstname = '{student.Firstname}', "
                + $"Lastname = '{student.Lastname}', "
                + $"StateCode = '{student.StateCode}', "
                + $"SAT = {student.SAT}, "
                + $"GPA ={student.GPA}, "
                + $"MajorID ={student.MajorID} "
                + $" where id = {student.ID}";

            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Insert failed :(");
            }
            Console.WriteLine("Student successfully updated");
            return true;
        }
        public bool RemoveStudent(int studentID) {
            var sql = $"Delete student where id = {studentID}";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) { throw new Exception("Remove student failed"); }
            Console.WriteLine("Student deleted");
            return true;
        }
#pragma warning disable
        public StudentController(string ServerInstance, string Database) {
            ConnectionString = $"server ={ServerInstance};" + $"database ={Database};" + "trustservercertificate=true;" + "trusted_connection = true;";
        }
    }
}