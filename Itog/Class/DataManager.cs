using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog.Class
{
    public class BookRepository : Repository<Book>
    {
        public BookRepository(string filePath) : base(filePath)
        {
        }

        public override void Load()
        {
            if (!File.Exists(_filePath)) return;

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 6)
                {
                    if (int.TryParse(parts[0], out int id) && int.TryParse(parts[2], out int authorId) && int.TryParse(parts[3], out int genreId) && int.TryParse(parts[4], out int publisherId))
                    {
                        _entities.Add(new Book(id, parts[1], authorId, genreId, publisherId, parts[5]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(book => $"{book.Id}#{book.Title}#{book.AuthorId}#{book.GenreId}#{book.PublisherId}#{book.ISBN}");
            File.WriteAllLines(_filePath, lines);
        }
        public List<Author> GetAuthors()
        {
            string filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_filePath), "authors.txt");
            var authorRepository = new AuthorRepository(filePath);
            authorRepository.Load();
            return authorRepository.GetAll();
        }
        public List<Genre> GetGenres()
        {
            string filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_filePath), "genres.txt");
            var genreRepository = new GenreRepository(filePath);
            genreRepository.Load();
            return genreRepository.GetAll();
        }
        public List<Publisher> GetPublishers()
        {
            string filePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(_filePath), "publishers.txt");
            var publisherRepository = new PublisherRepository(filePath);
            publisherRepository.Load();
            return publisherRepository.GetAll();
        }
    }

    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(string filePath) : base(filePath)
        {
        }

        public override void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int id))
                    {
                        _entities.Add(new Author(id, parts[1]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(author => $"{author.Id}#{author.Name}");
            File.WriteAllLines(_filePath, lines);
        }
    }

    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(string filePath) : base(filePath)
        {
        }
        public override void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int id))
                    {
                        _entities.Add(new Genre(id, parts[1]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(genre => $"{genre.Id}#{genre.Name}");
            File.WriteAllLines(_filePath, lines);
        }
    }

    public class PublisherRepository : Repository<Publisher>
    {
        public PublisherRepository(string filePath) : base(filePath)
        {
        }
        public override void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int id))
                    {
                        _entities.Add(new Publisher(id, parts[1]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(publisher => $"{publisher.Id}#{publisher.Name}");
            File.WriteAllLines(_filePath, lines);
        }
    }

    public class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(string filePath) : base(filePath)
        {
        }
        public override void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int id))
                    {
                        _entities.Add(new Employee(id, parts[1]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(employee => $"{employee.Id}#{employee.Name}");
            File.WriteAllLines(_filePath, lines);
        }
    }

    public class ReaderRepository : Repository<Reader>
    {
        public ReaderRepository(string filePath) : base(filePath)
        {
        }
        public override void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 7)
                {
                    if (int.TryParse(parts[0], out int id) && int.TryParse(parts[4], out int age))
                    {
                        _entities.Add(new Reader(id, parts[1], parts[2], parts[3], age, parts[5], parts[6]));
                        _nextId = id >= _nextId ? id + 1 : _nextId;
                    }
                }
            }
        }
        public override void Save()
        {
            var lines = _entities.Select(reader => $"{reader.Id}#{reader.LastName}#{reader.FirstName}#{reader.MiddleName}#{reader.Age}#{reader.PhoneNumber}#{reader.Email}");
            File.WriteAllLines(_filePath, lines);
        }
    }
    public class IssueRecordRepository
    {
        private List<IssueRecord> _issueRecords;
        private string _filePath;
        public IssueRecordRepository(string filePath)
        {
            _filePath = filePath;
            _issueRecords = new List<IssueRecord>();
        }
        public void Load()
        {
            if (!File.Exists(_filePath)) return;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('#');
                if (parts.Length == 6)
                {
                    if (int.TryParse(parts[0], out int readerId) &&
                         int.TryParse(parts[1], out int bookId) &&
                         int.TryParse(parts[3], out int employeeId) &&
                         Enum.TryParse(parts[4], out IssueType issueType) &&
                        DateTime.TryParse(parts[5], out DateTime issueDate))
                    {
                        _issueRecords.Add(new IssueRecord(readerId, bookId, parts[2], employeeId, issueType, issueDate));
                    }
                }
            }
        }
        public void Save()
        {
            var lines = _issueRecords.Select(record => $"{record.ReaderId}#{record.BookId}#{record.ISBN}#{record.EmployeeId}#{record.IssueType}#{record.IssueDate}");
            File.WriteAllLines(_filePath, lines);
        }
        public void Add(IssueRecord record)
        {
            _issueRecords.Add(record);
            Save();
        }
        public List<IssueRecord> GetAll()
        {
            return _issueRecords;
        }
    }
}
