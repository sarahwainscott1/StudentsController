using StudentsController;


//using LINQ to join
const string server = "localhost\\sqlexpress";
const string database = "EdDB";

//opens connections
var studentctrl = new StudentController(server, database);
studentctrl.OpenConnection();
var majorctrl = new MajorController(server, database);
majorctrl.OpenConnection();

//gets two lists, one of all majors and one of all students
var majors = majorctrl.GetMajors();
var students = studentctrl.GetAllStudents();

//linq join statement, must do students equals major
var studentsMajors = from s in students
                     join m in majors on s.MajorID equals m.ID //join statement
                     where s.StateCode == "OH" //optional where clause
                     orderby s.Lastname descending //optional orderby clause
                     select new { Fullname = s.Firstname + " " + s.Lastname, Major = m.Description }; //what data do we want to display? - select s just shows student data. for both all student and major do <- could do s.firstname etc for specific columns

foreach (var sm in studentsMajors) {
    Console.WriteLine($"{sm.Fullname} | {sm.Major}");
}
studentctrl.CloseConnection();
majorctrl.CloseConnection();


//select all students
/*
var studentctrlr = new StudentController(@"localhost\sqlexpress","EdDb"); //creates new instance of class
studentctrlr.OpenConnection();

var students = studentctrlr.GetAllStudents();


foreach (var student in students) {
Console.WriteLine(student);
}
studentctrlr.CloseConnection();
*/
//select all students using LINQ

//select all students
/*LINQ 
var studentctrlr = new StudentController(@"localhost\sqlexpress", "EdDb"); //creates new instance of class
studentctrlr.OpenConnection();

var students = studentctrlr.GetAllStudents();
var studentFromOhio = students.Where(s => s.StateCode == "OH" && s.GPA >= 3.00m).OrderBy(s => s.Lastname);

var studentQuery = from s in students where s.StateCode == "OH" && s.GPA >= 3.00m orderby s.Lastname select s;
var studentHighSAT = students.Where(s => s.StateCode == "OH" && s.SAT == (students.Max(s => s.SAT)));
var fullnameMajor = from s in students orderby s.Major.Description select new { Fullname = s.Firstname + " " + s.Lastname, MajorDesc = s.Major.Description };

foreach (var student in fullnameMajor) {
    Console.WriteLine($"Fullname: {student.Fullname} | Major: {student.MajorDesc}");
}

studentctrlr.CloseConnection();
*/

//select all majors
/*
var majorcontroller = new MajorController(@"localhost\sqlexpress", "EdDb"); //creates new instance of class
majorcontroller.OpenConnection();

var majors = majorcontroller.GetMajors();
foreach (var major in majors) {
    Console.WriteLine(major);
}
majorcontroller.CloseConnection();
*/

//select by major id
/*var majorcontroller = new MajorController(@"localhost\sqlexpress", "EdDb"); //creates new instance of class
majorcontroller.OpenConnection();

var major = majorcontroller.GetMajorByID(10);
Console.WriteLine(major);
*/



//var student = new Student() 
//{Firstname = "Graham", Lastname = "Kracker", StateCode = "CA", GPA = 3.0m, SAT = 1300, MajorID = 1};

//var resultcode = studentctrlr.AddStudent(student);
//student.ID = 69;

//var resultcode = studentctrlr.RemoveStudent(67);

//studentctrlr.CloseConnection();



/* add major
var majorctrl = new MajorController(@"localhost\sqlexpress", "EdDb");
majorctrl.OpenConnection();

var major = new Major() {
    Code = "GRDN",
    Description = "Gardening",
    MinSAT = 1000
};
var resultcode = majorctrl.AddMajor(major);
majorctrl.CloseConnection();
*/


/*update major
var majorctrl = new MajorController(@"localhost\sqlexpress", "EdDb");
majorctrl.OpenConnection();

var major = new Major() {
    Code = "PPU",
    Description = "Pencil Pushing",
    MinSAT = 100,
    ID = 6
};
var resultcode = majorctrl.UpdateMajor(major);
majorctrl.CloseConnection();
*/
