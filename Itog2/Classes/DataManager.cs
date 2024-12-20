using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Itog2.Classes
{
    public static class DataManager
    {
        private static readonly string _basePath = Path.Combine(AppContext.BaseDirectory, "..", "Resources");

        public static List<T> LoadEntities<T>(string fileName, Func<string, T> createEntity) where T : class
        {
            var filePath = Path.Combine(_basePath, fileName + ".txt");
            var entities = new List<T>();

            if (!File.Exists(filePath))
            {
                // Create an empty file if it doesn't exist.
                try
                {
                    string path_ = Path.GetDirectoryName(filePath);
                    Directory.CreateDirectory(path_);
                    File.Create(filePath).Close(); // Create and close the file.
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating file {filePath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return entities;
            }

            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    entities.Add(createEntity(line));
                }

                if (entities.Count > 0)
                {
                    var lastId = 0;

                    if (typeof(T) == typeof(Author))
                    {
                        var temp = entities.Last() as Author;
                        lastId = int.Parse(temp.Id);
                    }
                    if (typeof(T) == typeof(Book))
                    {
                        var temp = entities.Last() as Book;
                        lastId = int.Parse(temp.Id);
                    }
                    if (typeof(T) == typeof(Genre))
                    {
                        var temp = entities.Last() as Genre;
                        lastId = int.Parse(temp.Id);
                    }
                    if (typeof(T) == typeof(Publisher))
                    {
                        var temp = entities.Last() as Publisher;
                        lastId = int.Parse(temp.Id);
                    }
                    if (typeof(T) == typeof(Employee))
                    {
                        var temp = entities.Last() as Employee;
                        lastId = int.Parse(temp.Id);
                    }
                    if (typeof(T) == typeof(Reader))
                    {
                        var temp = entities.Last() as Reader;
                        lastId = int.Parse(temp.Id);
                    }

                    BaseEntity.IdCounter = lastId;
                }

                return entities;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading data for {fileName}: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<T>();
            }
        }

        public static void SaveEntities<T>(string fileName, List<T> entities) where T : class
        {
            var filePath = Path.Combine(_basePath, fileName + ".txt");
            if (!File.Exists(filePath))
            {
                // Create an empty file if it doesn't exist.
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    File.Create(filePath).Close(); // Create and close the file.
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating file {filePath}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var entity in entities)
                    {
                        writer.WriteLine(entity.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error saving data for {fileName}: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}