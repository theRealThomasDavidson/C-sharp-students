using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace HelloWorld
{
    class Student
    {
        public Student(int id, string name, double grade, HashSet<string> courses, DateTime date_of_birth)
        {
            this.id = id;
            this.name = name;
            this.grade = grade;
            this.courses = courses;
            this.date_of_birth = date_of_birth;
        }

        public int  id {get; set;}
        public string name { get; set; }
        public double grade { get; set; }
        public HashSet<string> courses { get; set; }
        public DateTime date_of_birth { get; set; }
        public string ToString()
        {
            return String.Format("name: {0}\tid: {1}\t dob: {2}\ncourses: {3}\ngrade: {4}", this.name, this.id, this.date_of_birth.ToString(), string.Join(", ",courses), grade);
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            static int inputInt(string message = "Enter an integer")
            {
                int nt;
                string str;
                Console.WriteLine(message);
                do
                {
                    str = Console.ReadLine();
                } while (!int.TryParse(str, out nt));
                return int.Parse(str);
            }
            static double inputGFloat(string message = "Enter an number between 0 and 4")
            {
                double flt;
                string str;
                Console.WriteLine(message);
                do
                {
                    do
                    {
                        str = Console.ReadLine();
                    } while (!double.TryParse(str, out flt));
                    flt = double.Parse(str);
                } while (!(0 <= flt && flt <= 4));
                return flt;
            }
            static DateTime inputDOB(string message = "Enter Date of Birth")
            {
                Console.WriteLine(message);
                Console.WriteLine("Enter new date of birth for student:");
                int year;
                do
                {
                    year = inputInt("Enter birth year:");
                } while (!(year <= DateTime.Today.Year));
                int month;
                do
                {
                    month = inputInt("Enter birth month by number(1 to 12:");
                } while (!(1 <= month && month <= 12));
                int day;
                do
                {
                    day = inputInt("Enter birth day:");
                } while (!(1 <= day && day <= DateTime.DaysInMonth(year, month)));
                return new DateTime(year, month, day);
            }
            static Student showStudentById(Dictionary<int, Student> students)
            {
                int id;
                do
                {
                    id = inputInt("Enter Student Id number:");
                    if (!students.ContainsKey(id))
                    {
                        Console.WriteLine("Student not found");
                    }
                } while (!students.ContainsKey(id));
                Console.WriteLine(String.Format("--------------------------\n{0}\n--------------------------", students[id].ToString()));
                return students[id];
            }
            static void modifyStudentById(Dictionary<int, Student> students)
            {
                Student temp = showStudentById(students);
                Console.WriteLine("To modify an Item below put the number of that choice:");
                Console.WriteLine(String.Format("1:\tStudent name: {0}", temp.name));
                Console.WriteLine(String.Format("2:\tGrade: {0}", temp.grade));
                Console.WriteLine(String.Format("3:\tCourses:", string.Join(", ",temp.courses)));
                Console.WriteLine(String.Format("4:\tDate of birth", temp.date_of_birth));
                int choice = inputInt("Enter your Choice:");
                string str, str2;
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter new name for student:");
                        str = Console.ReadLine();
                        Console.WriteLine(string.Format("Confirm changing name from {0} to {1} (y to change)", temp.name, str));
                        str2 = Console.ReadLine();
                        if (!"yY".Contains(str2.Substring(0,1))){
                            Console.WriteLine("noting will change");
                            return;
                        }
                        Console.WriteLine("Updating");
                        temp.name = str;
                        Console.WriteLine(temp.ToString());
                        return;
                        break;
                    case 2:
                        Console.WriteLine("Enter new grade for student:");
                        double ngrade = inputGFloat();
                        Console.WriteLine(string.Format("Confirm changing name from {0} to {1} (y to change)", temp.grade, ngrade));
                        str2 = Console.ReadLine();
                        if (!"yY".Contains(str2.Substring(0, 1)))
                        {
                            Console.WriteLine("noting will change");
                            return;
                        }
                        Console.WriteLine("Updating");
                        temp.grade = ngrade;
                        Console.WriteLine(temp.ToString());
                        return;
                        break;
                    case 3:
                        Console.WriteLine("Enter class to change for student:");
                        str = Console.ReadLine();
                        if (temp.courses.Contains(str))
                        {
                            Console.WriteLine(string.Format("Confirm removing {0} course (y to remove)", str));
                            str2 = Console.ReadLine();
                            if (!"yY".Contains(str2.Substring(0, 1)))
                            {
                                Console.WriteLine("noting will change");
                                return;
                            }
                            Console.WriteLine("Updating");
                            temp.courses.Remove(str);
                            Console.WriteLine(temp.ToString());
                            return;
                        }
                        Console.WriteLine(string.Format("Confirm adding {0} course (y to add)", str));
                        str2 = Console.ReadLine();
                        if (!"yY".Contains(str2.Substring(0, 1)))
                        {
                            Console.WriteLine("noting will change");
                            return;
                        }
                        Console.WriteLine("Updating");
                        temp.courses.Add(str);
                        Console.WriteLine(temp.ToString());
                        return;
                        break;
                    case 4:

                        DateTime DOB = inputDOB();//
                        Console.WriteLine(string.Format("Confirm changing DOB from {0} to {1} (y to change)", temp.date_of_birth, DOB));
                        str2 = Console.ReadLine();
                        if (!"yY".Contains(str2.Substring(0, 1)))
                        {
                            Console.WriteLine("noting will change");
                            return;
                        }
                        Console.WriteLine("Updating");
                        temp.date_of_birth = DOB;
                        Console.WriteLine(temp.ToString());
                        return;
                        break;
                    default:
                        Console.WriteLine("please only enter values that are present.");
                        break;
                }
            }
            static int getStudentCountByClass(Dictionary<int, Student> students)
            {
                int count = 0; 
                Console.WriteLine("Enter class name to get attendance:");
                string cName = Console.ReadLine();

                foreach (var student in students.Values)
                {
                    count += student.courses.Contains(cName)? 1:0;
                }
                Console.WriteLine(String.Format("Class {0}, has {1} Students",cName, count));
                return count;
            }
            static int showStudentAge(Dictionary<int, Student> students)
            {
                Student temp = showStudentById(students);
                DateTime now = DateTime.Today;
                int age = (now.Year - temp.date_of_birth.Year);
                age += temp.date_of_birth > now.AddYears(-age)? -1: 0;
                Console.WriteLine(String.Format("Student is {1} years old.",temp.ToString(), age)); 
                return age;
            }
            static Student addStudent(Dictionary<int, Student> students, int id)
            {
                Console.WriteLine("Enter name for new student:");
                string name = Console.ReadLine();
                Student temp = new Student(id, name, inputGFloat("New Students current grade:"), new HashSet<string>(),  inputDOB("Enter Date Of Birth For student:"));
                name = "";
                do
                {
                    Console.WriteLine("Enter course for the new student(enter 0 to exit):");
                    name = Console.ReadLine();
                    if (name.Substring(0, 1) == "0") { break; }
                    temp.courses.Add(name);
                }while (name.Substring(0, 1) != "0");
                Console.WriteLine(string.Format("Confirm adding\n{0}\nstudent(y to add)", temp.ToString()));
                name = Console.ReadLine();
                if (!"yY".Contains(name.Substring(0, 1)))
                {
                    Console.WriteLine("noting will change");
                    return null;
                }
                students.Add(id, temp);
                return temp;
            }
            // startup
            Dictionary<int, Student> student_list = new Dictionary<int, Student>();
            Student s1 = new Student(1, "Alice Albertson", 3.2, new HashSet<string>(), new DateTime(1995, 3, 14));
            s1.courses.Add("Biology");
            s1.courses.Add("Sociology");
            student_list.Add(s1.id, s1);
            s1 = new Student(2, "Bob Barker", 3.4, new HashSet<string>(), new DateTime(1996, 4, 15));
            s1.courses.Add("Math");
            s1.courses.Add("Photography");
            student_list.Add(s1.id, s1);
            s1 = new Student(3, "Carol Clarkson", 3.6, new HashSet<string>(), new DateTime(1997, 5, 16));
            s1.courses.Add("History");
            s1.courses.Add("Klingon Literature");
            student_list.Add(s1.id, s1);
            int iterid = 4;
            int choice;
            // mainloop
            Console.WriteLine("Welcome to our student app!");
            do
            {
                Console.WriteLine("");
                Console.WriteLine("Choices listed below:");
                Console.WriteLine("1:\tGet Student by id:");
                Console.WriteLine("2:\tModify Student info");
                Console.WriteLine("3:\tGet number of students in a class:");
                Console.WriteLine("4:\tFind Student's Age");
                Console.WriteLine("5:\tNew Student");
                Console.WriteLine("0:\tExit program");
                choice = inputInt("Enter your Choice:");
                switch (choice)
                {

                    case 1:
                        showStudentById(student_list);
                        break;
                    case 2:
                        modifyStudentById(student_list);
                        break;
                    case 3:
                        getStudentCountByClass(student_list);
                        break;
                    case 4:
                        showStudentAge(student_list);
                        break;
                    case 5:
                        addStudent(student_list, iterid);
                        iterid += 1;
                        break;
                }

            } while (choice != 0);
            // cleanup
        }
    }
}
