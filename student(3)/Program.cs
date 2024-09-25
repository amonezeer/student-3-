using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Student : IComparable<Student>, ICloneable
{
    private string lastName;
    private string firstName;
    private string middleName;
    private DateTime birthDate;
    private string homeAddress;
    private string phoneNumber;

    private List<int> homeworkGrades;
    private List<int> courseGrades;
    private List<int> examGrades;

    public Student(string lastName, string firstName, string middleName, DateTime birthDate, string homeAddress, string phoneNumber)
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.middleName = middleName;
        this.birthDate = birthDate;
        this.homeAddress = homeAddress;
        this.phoneNumber = phoneNumber;
        this.homeworkGrades = new List<int>();
        this.courseGrades = new List<int>();
        this.examGrades = new List<int>();
    }

    public string LastName => lastName;
    public string FirstName => firstName;
    public DateTime BirthDate => birthDate;

    public double GetAverageGrade()
    {
        var allGrades = homeworkGrades.Concat(courseGrades).Concat(examGrades).ToList();
        return allGrades.Count > 0 ? allGrades.Average() : 0;
    }

    public int CompareTo(Student other)
    {
        if (other == null) return 1;
        return this.GetAverageGrade().CompareTo(other.GetAverageGrade());
    }

    public object Clone()
    {
        return new Student(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
        {
            homeworkGrades = new List<int>(this.homeworkGrades),
            courseGrades = new List<int>(this.courseGrades),
            examGrades = new List<int>(this.examGrades)
        };
    }
    public class CompareByLastName : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return string.Compare(x.LastName, y.LastName);
        }
    }

    public class CompareByBirthDate : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return DateTime.Compare(x.BirthDate, y.BirthDate);
        }
    }

    public void DisplayStudentInfo()
    {
        Console.WriteLine($"Фамилия: {lastName}, Имя: {firstName}, Средний балл: {GetAverageGrade()}");
    }
}
public class Group : IEnumerable<Student>, ICloneable
{
    private List<Student> students;

    public Group(List<Student> students)
    {
        this.students = students;
    }
    public IEnumerator<Student> GetEnumerator()
    {
        return students.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public object Clone()
    {
        return new Group(new List<Student>(this.students.Select(s => (Student)s.Clone())));
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("|♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥|");
        Student student1 = new Student("Демидов", "Артур", "Иванович", new DateTime(2004, 3, 15), "ул Преображенская 4", "380-97-39-55-211");
        Student student2 = new Student("Черенков", "Петр", "Архипович", new DateTime(1999, 4, 11), "ул Гвардейская 11а", "380-66-53-22-423");
        Student student3 = new Student("Джульпаев", "Равшан", "Суренович", new DateTime(2001, 3, 3), "ул Генеузька 12/2", "380-63-11-55-123");

        List<Student> studentList = new List<Student> { student1, student2, student3 };
        Group group = new Group(studentList);

        studentList.Sort(new Student.CompareByLastName());
        Console.WriteLine("Сортировка по фамилии:");
        foreach (var student in studentList)
        {
            student.DisplayStudentInfo();
        }

        Console.WriteLine("\nСтуденты в группе:");
        foreach (var student in group)
        {
            student.DisplayStudentInfo();
        }
        Console.WriteLine("|♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥|");
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
}
