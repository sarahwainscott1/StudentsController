using StudentsController;
using MajorsController;

/*var studentctrlr = new StudentController(@"localhost\sqlexpress","EdDb"); //creates new instance of class
studentctrlr.OpenConnection();

var student = new Student() 
{Firstname = "Graham", Lastname = "Kracker", StateCode = "CA", GPA = 3.0m, SAT = 1300, MajorID = 1};

//var resultcode = studentctrlr.AddStudent(student);
student.ID = 69;

var resultcode = studentctrlr.UpdateStudent(student);

studentctrlr.CloseConnection();
*/
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
