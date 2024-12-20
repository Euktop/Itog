using Itog2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Itog2.Windows
{
    public enum ReportType
    {
        PopularBooks,
        EmployeeRating
    }
    public partial class ReportWindow : Window
    {
        private ReportType _reportType;
        public string GetText() => ReportTextBlock.Text;
        // REMOVE THIS LINE: internal TextBlock ReportTextBlock;
        public ReportWindow(ReportType type)
        {
            InitializeComponent();
            _reportType = type;
            PreviewReport();
        }

        private void ButtonGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport();
            DialogResult = true;
        }
        private void PreviewReport()
        {
            ReportTextBlock.Text = "";
            switch (_reportType)
            {
                case ReportType.PopularBooks:
                    ReportTextBlock.Text = GeneratePopularBooksPreview();
                    break;
                case ReportType.EmployeeRating:
                    ReportTextBlock.Text = GenerateEmployeeRatingPreview();
                    break;
            }

        }
        private void GenerateReport()
        {
            ReportTextBlock.Text = "";
            switch (_reportType)
            {
                case ReportType.PopularBooks:
                    ReportTextBlock.Text = GeneratePopularBooksReport();
                    break;
                case ReportType.EmployeeRating:
                    ReportTextBlock.Text = GenerateEmployeeRatingReport();
                    break;
            }
        }
        private string GeneratePopularBooksPreview()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Предпросмотр отчета 'Популярные книги':\n");
            sb.AppendLine("Этот отчет покажет самые популярные и наименее популярные книги.");
            return sb.ToString();
        }
        private string GenerateEmployeeRatingPreview()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Предпросмотр отчета 'Рейтинг сотрудников':\n");
            sb.AppendLine("Этот отчет покажет рейтинг сотрудников по количеству выданных и возвращенных книг.");
            return sb.ToString();
        }
        private string GeneratePopularBooksReport()
        {
            var journalEntries = DataManager.LoadEntities<JournalEntry>("Journal", line =>
            {
                var parts = line.Split('#');
                return new JournalEntry
                {
                    ReaderId = parts[0],
                    BookId = parts[1],
                    ISBN = parts[2],
                    EmployeeId = parts[3],
                    Type = parts[4]
                };
            });

            var bookCounts = journalEntries
                .GroupBy(entry => entry.BookId)
                .Select(group => new
                {
                    BookId = group.Key,
                    IssueCount = group.Count(entry => entry.Type == "Issue"),
                    ReturnCount = group.Count(entry => entry.Type == "Return"),
                    TotalCount = group.Count()
                })
                .OrderByDescending(x => x.TotalCount)
                .ToList();
            var books = DataManager.LoadEntities<Book>("Books", line =>
            {
                var parts = line.Split('#');
                return new Book
                {
                    Id = parts[0],
                    AuthorIda = parts[1],
                    GenreIda = parts[2],
                    PublisherIda = parts[3],
                    Name = parts[4]
                };
            });

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Отчет 'Популярные книги':\n");
            sb.AppendLine("Самые популярные книги (по общему количеству выдач и возвратов):\n");

            foreach (var item in bookCounts.Take(5))
            {
                var book = books.FirstOrDefault(x => x.Id == item.BookId);
                sb.AppendLine($"Книга: {book?.Name}, Выдано: {item.IssueCount}, Возвращено: {item.ReturnCount}, Общее количество: {item.TotalCount}");
            }
            sb.AppendLine("\nНаименее популярные книги (по общему количеству выдач и возвратов):\n");

            foreach (var item in bookCounts.OrderBy(x => x.TotalCount).Take(5))
            {
                var book = books.FirstOrDefault(x => x.Id == item.BookId);
                sb.AppendLine($"Книга: {book?.Name}, Выдано: {item.IssueCount}, Возвращено: {item.ReturnCount}, Общее количество: {item.TotalCount}");
            }

            return sb.ToString();
        }
        private string GenerateEmployeeRatingReport()
        {
            var journalEntries = DataManager.LoadEntities<JournalEntry>("Journal", line =>
            {
                var parts = line.Split('#');
                return new JournalEntry
                {
                    ReaderId = parts[0],
                    BookId = parts[1],
                    ISBN = parts[2],
                    EmployeeId = parts[3],
                    Type = parts[4]
                };
            });
            var employees = DataManager.LoadEntities<Employee>("Employees", line =>
            {
                var parts = line.Split('#');
                return new Employee
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            var employeeRatings = journalEntries
               .GroupBy(entry => entry.EmployeeId)
               .Select(group => new
               {
                   EmployeeId = group.Key,
                   IssueCount = group.Count(entry => entry.Type == "Issue"),
                   ReturnCount = group.Count(entry => entry.Type == "Return"),
                   TotalCount = group.Count()
               })
               .OrderByDescending(x => x.TotalCount)
               .ToList();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Отчет 'Рейтинг сотрудников':\n");
            foreach (var rating in employeeRatings)
            {
                var employee = employees.FirstOrDefault(x => x.Id == rating.EmployeeId);
                sb.AppendLine($"Сотрудник: {employee?.Name} {employee?.MiddleName} {employee?.Surname}, Выдано: {rating.IssueCount}, Возвращено: {rating.ReturnCount}, Всего: {rating.TotalCount}");
            }

            return sb.ToString();
        }
    }
}