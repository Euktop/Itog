using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog.Class
{
    public class DataManager

    {
        public BookRepository BookRepository { get; private set; }
        public AuthorRepository AuthorRepository { get; private set; }
        public GenreRepository GenreRepository { get; private set; }
        public PublisherRepository PublisherRepository { get; private set; }
        public EmployeeRepository EmployeeRepository { get; private set; }
        public ReaderRepository ReaderRepository { get; private set; }
        public IssueRecordRepository IssueRecordRepository { get; private set; }

        public DataManager(string dataDirectory= "Resources")
        {
            BookRepository = new BookRepository(System.IO.Path.Combine(dataDirectory, "books.txt"));
            AuthorRepository = new AuthorRepository(System.IO.Path.Combine(dataDirectory, "authors.txt"));
            GenreRepository = new GenreRepository(System.IO.Path.Combine(dataDirectory, "genres.txt"));
            PublisherRepository = new PublisherRepository(System.IO.Path.Combine(dataDirectory, "publishers.txt"));
            EmployeeRepository = new EmployeeRepository(System.IO.Path.Combine(dataDirectory, "employees.txt"));
            ReaderRepository = new ReaderRepository(System.IO.Path.Combine(dataDirectory, "readers.txt"));
            IssueRecordRepository = new IssueRecordRepository(System.IO.Path.Combine(dataDirectory, "issues.txt"));
            LoadAllData();
        }

        public void LoadAllData()
        {
            BookRepository.Load();
            AuthorRepository.Load();
            GenreRepository.Load();
            PublisherRepository.Load();
            EmployeeRepository.Load();
            ReaderRepository.Load();
            IssueRecordRepository.Load();
        }
        public void SaveAllData()
        {
            BookRepository.Save();
            AuthorRepository.Save();
            GenreRepository.Save();
            PublisherRepository.Save();
            EmployeeRepository.Save();
            ReaderRepository.Save();
            IssueRecordRepository.Save();
        }
    }
}
