using StudentsController;
using MajorsController;
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

//select all majors

/*var majorcontroller = new MajorController(@"localhost\sqlexpress", "EdDb"); //creates new instance of class
majorcontroller.OpenConnection();

var majors = majorcontroller.GetMajors();
foreach (var major in majors) {
    Console.WriteLine(major);
}
majorcontroller.CloseConnection();
*/
//select by major id
var majorcontroller = new MajorController(@"localhost\sqlexpress", "EdDb"); //creates new instance of class
majorcontroller.OpenConnection();

var major = majorcontroller.GetMajorByID(10);
Console.WriteLine(major);




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
