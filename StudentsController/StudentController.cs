using Microsoft.Data.SqlClient;

namespace StudentsController {
    public class StudentController {

        public string ConnectionString { get; set; }
        public SqlConnection SqlConnection { get; set; }

        public void OpenConnection() {
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
            if(SqlConnection.State != System.Data.ConnectionState.Open) {
                throw new Exception("Connection did not open :(");
            }
        }
        public void CloseConnection() {
            SqlConnection.Close();
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
        public StudentController(string ServerInstance, string Database) {
            ConnectionString = $"server ={ServerInstance};" + $"database ={Database};" + "trustservercertificate=true;" +"trusted_connection = true;";
        }
    }
}