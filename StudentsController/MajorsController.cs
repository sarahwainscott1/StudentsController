using Microsoft.Data.SqlClient;

namespace StudentsController {
    public class MajorController {

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
        //get all majors
        public List<Major> GetMajors() {
            var majors = new List<Major>();
            var sql = "select * from major";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                var major = new Major();
                major.ID = Convert.ToInt32(reader["ID"]);
                major.Code = Convert.ToString(reader["Code"]);
                major.Description = Convert.ToString(reader["Description"]);
                major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

                majors.Add(major);
            }
            reader.Close();
           
            return majors;
        }
        //get by major id
        public Major GetMajorByID(int majorid) {
            var major = new Major();
            var sql = $"select * from major where id = {majorid}";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Read();
            if (!reader.HasRows) {
                reader.Close();
                
                return null;
            }
            major.ID = Convert.ToInt32(reader["ID"]);
            major.Code = Convert.ToString(reader["Code"]);
            major.Description = Convert.ToString(reader["Description"]);
            major.MinSAT = Convert.ToInt32(reader["MinSAT"]);

            reader.Close();
           
            return major;
        }

        public bool AddMajor(Major major) {
            var sql = "insert major" + "(Code,Description, minSat)" + "Values " + $"('{major.Code}', '{major.Description}', '{major.MinSAT}')";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected < 1) {
                throw new Exception("Insert failed");
            }
            Console.WriteLine("Major successfully added");
            return true;
        }
        public bool UpdateMajor(Major major) {
            var sql = "update major set" + $" Code ='{major.Code}', Description ='{major.Description}', minSat ='{major.MinSAT}' " +
                $"where id = {major.ID}";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected < 1) {
                throw new Exception("Insert failed");
            }
            Console.WriteLine("Major successfully added");
            return true;
        }
        public bool RemoveMajor(int majorid) {
            var sql = $"Delete student where id = {majorid}";
            var cmd = new SqlCommand(sql, SqlConnection);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1) { throw new Exception("Remove major failed"); }
            Console.WriteLine("Major deleted");
            return true;
        }
#pragma warning disable
        public MajorController(string ServerInstance, string Database) {
            ConnectionString = $"server ={ServerInstance};" + $"database ={Database};" + "trustservercertificate=true;" + "trusted_connection = true;";
        }
    }
}
