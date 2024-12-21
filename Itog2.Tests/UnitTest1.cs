using Microsoft.VisualStudio.TestTools.UnitTesting;
using Itog2.Classes;
using System.Collections.Generic;
using System.IO;
using Itog2.Windows;
using System.Linq;

namespace Itog2.Tests
{
    [TestClass]
    public class DataManagerTests
    {
        private const string TestResourcesPath = "TestResources"; // Create this folder in your test project for test files

        [TestInitialize]
        public void TestInitialize()
        {
            // Create test directory if it doesn't exist
            if (!Directory.Exists(TestResourcesPath))
            {
                Directory.CreateDirectory(TestResourcesPath);
            }
            // Clean up test directory before each test run
            foreach (var file in Directory.GetFiles(TestResourcesPath, "*.txt"))
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void TestFileReadingAndWriting()
        {
            var testFilePath = Path.Combine(TestResourcesPath, "test.txt");
            var testData = new List<string> { "101#Test1", "102#Test2", "103#Test3" };

            // Write test data
            File.WriteAllLines(testFilePath, testData);

            // Read test data
            var loadedData = File.ReadAllLines(testFilePath);

            // Assert data
            Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert.AreEqual(testData, loadedData, "File content does not match.");

            // Clear test file
            File.Delete(testFilePath);
        }
        [TestMethod]
        public void TestLoadAndSaveEntities()
        {

            var testData = new List<Author>
            {
                new Author {Id = "101", Name = "John", MiddleName = "Doe", Surname = "Smith" },
                new Author {Id ="102", Name = "Jane", MiddleName = "M", Surname = "Doe" }
            };
            var filePath = Path.Combine(TestResourcesPath, "TestAuthors.txt");

            DataManager.SaveEntities("TestAuthors", testData);
            var loadedEntities = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            NUnit.Framework.Assert.AreEqual(testData.Count, loadedEntities.Count, "The number of loaded entities does not match.");
            NUnit.Framework.Assert.AreEqual(testData[0].Name, loadedEntities[0].Name);
            NUnit.Framework.Assert.AreEqual(testData[1].Name, loadedEntities[1].Name);

        }


        [TestMethod]
        public void TestEditEntities()
        {

            var testData = new List<Author>
            {
                new Author { Id = "101", Name = "John", MiddleName = "Doe", Surname = "Smith" },
                new Author { Id = "102", Name = "Jane", MiddleName = "M", Surname = "Doe" }
            };
            var filePath = Path.Combine(TestResourcesPath, "TestAuthors.txt");
            DataManager.SaveEntities("TestAuthors", testData);

            var loadedEntities = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            // Modify an entity
            var editedEntity = loadedEntities.First(x => x.Id == "101");
            editedEntity.Name = "Updated Name";
            DataManager.SaveEntities("TestAuthors", loadedEntities);


            // Load again to check edit
            var reloadedEntities = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            var editedEntityReloaded = reloadedEntities.First(x => x.Id == "101");
            Assert.AreEqual(editedEntity.Name, editedEntityReloaded.Name, "The name of edited entity does not match");
        }
        [TestMethod]
        public void TestAddDeleteEntities()
        {
            var testData = new List<Author>
            {
                new Author {Id = "101", Name = "John", MiddleName = "Doe", Surname = "Smith" },
                new Author {Id ="102", Name = "Jane", MiddleName = "M", Surname = "Doe" }
            };
            var filePath = Path.Combine(TestResourcesPath, "TestAuthors.txt");
            DataManager.SaveEntities("TestAuthors", testData);

            var loadedEntities = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            //Add new entity
            var newEntity = new Author { Id = "103", Name = "New Name", MiddleName = "N", Surname = "Entity" };
            loadedEntities.Add(newEntity);
            DataManager.SaveEntities("TestAuthors", loadedEntities);

            var loadedEntitiesAfterAdd = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            Assert.AreEqual(3, loadedEntitiesAfterAdd.Count, "The new entity was not added");

            //Remove entity

            loadedEntitiesAfterAdd.Remove(loadedEntitiesAfterAdd.First(x => x.Id == "101"));
            DataManager.SaveEntities("TestAuthors", loadedEntitiesAfterAdd);
            var loadedEntitiesAfterRemove = DataManager.LoadEntities<Author>("TestAuthors", line =>
            {
                var parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
                };
            });

            Assert.AreEqual(2, loadedEntitiesAfterRemove.Count, "The new entity was not deleted");
        }
        [TestMethod]
        public void TestPopularBooksReport()
        {

            // Create Journal Test
            var testJournalData = new List<JournalEntry>
            {
                new JournalEntry { ReaderId = "1", BookId = "101", ISBN = "123", EmployeeId = "1", Type = "Issue" },
                new JournalEntry { ReaderId = "2", BookId = "101", ISBN = "123", EmployeeId = "1", Type = "Return" },
                new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "1", Type = "Issue" },
                 new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "1", Type = "Return" },
                 new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "1", Type = "Issue" }
            };

            DataManager.SaveEntities("Journal", testJournalData);
            var reportWindow = new ReportWindow(ReportType.PopularBooks);
            reportWindow.ShowDialog();
            var text = reportWindow.ReportTextBlock.Text;

            Assert.IsTrue(text.Contains("Книга: null, Выдано: 1, Возвращено: 1, Общее количество: 2"), "Report does not contains book id 101");
            Assert.IsTrue(text.Contains("Книга: null, Выдано: 2, Возвращено: 1, Общее количество: 3"), "Report does not contains book id 102");

        }

        [TestMethod]
        public void TestEmployeeRatingReport()
        {

            // Create Journal Test
            var testJournalData = new List<JournalEntry>
            {
                new JournalEntry { ReaderId = "1", BookId = "101", ISBN = "123", EmployeeId = "1", Type = "Issue" },
                new JournalEntry { ReaderId = "2", BookId = "101", ISBN = "123", EmployeeId = "1", Type = "Return" },
                 new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "2", Type = "Issue" },
                  new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "2", Type = "Return" },
                  new JournalEntry { ReaderId = "1", BookId = "102", ISBN = "124", EmployeeId = "2", Type = "Issue" }
            };
            var testEmployeeData = new List<Employee>
            {
                new Employee {Id = "1", Name = "John", MiddleName = "Doe", Surname = "Smith" },
                 new Employee {Id = "2", Name = "Jane", MiddleName = "M", Surname = "Doe" }
            };

            DataManager.SaveEntities("Journal", testJournalData);
            DataManager.SaveEntities("Employees", testEmployeeData);
            var reportWindow = new ReportWindow(ReportType.EmployeeRating);
            reportWindow.ShowDialog();

            var text = reportWindow.ReportTextBlock.Text;

            Assert.IsTrue(text.Contains("Сотрудник: John Doe Smith, Выдано: 1, Возвращено: 1, Всего: 2"), "Report does not contains employe 1");
            Assert.IsTrue(text.Contains("Сотрудник: Jane M Doe, Выдано: 2, Возвращено: 1, Всего: 3"), "Report does not contains employe 2");

        }
    }
}