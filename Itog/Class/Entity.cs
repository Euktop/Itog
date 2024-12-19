using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog.Class
{
    public abstract class Entity
    {
        public int Id { get; set; }
    }
    public class Book : Entity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public string ISBN { get; set; }
        public Book() { }
        public Book(int id, string title, int authorId, int genreId, int publisherId, string isbn)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
            GenreId = genreId;
            PublisherId = publisherId;
            ISBN = isbn;
        }
    }
    public class Author : Entity
    {
        public string Name { get; set; }
        public Author() { }
        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class Genre : Entity
    {
        public string Name { get; set; }
        public Genre() { }
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class Publisher : Entity
    {
        public string Name { get; set; }
        public Publisher() { }
        public Publisher(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Employee : Entity
    {
        public string Name { get; set; }
        public Employee() { }
        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Reader : Entity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Reader() { }
        public Reader(int id, string lastName, string firstName, string middleName, int age, string phoneNumber, string email)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Age = age;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }

    public enum IssueType
    {
        Issue,
        Return
    }

    public class IssueRecord
    {
        public int ReaderId { get; set; }
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public int EmployeeId { get; set; }
        public IssueType IssueType { get; set; }
        public DateTime IssueDate { get; set; }
        public IssueRecord() { }
        public IssueRecord(int readerId, int bookId, string isbn, int employeeId, IssueType issueType, DateTime issueDate)
        {
            ReaderId = readerId;
            BookId = bookId;
            ISBN = isbn;
            EmployeeId = employeeId;
            IssueType = issueType;
            IssueDate = issueDate;
        }
    }


}
