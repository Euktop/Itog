using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool a= File.Exists(@"Resources\\TextFile1");
            bool b = File.Exists(@"C:\Users\studkab8\Desktop\ISp_1-23v\Трунов\Itog\ConsoleApp1\Resources\TextFile1.txt");
            Console.WriteLine(a);
            Console.WriteLine(b);

            string pathToMyFile = "Resources\\TextFile1.txt";

            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), pathToMyFile);

            bool ae=true;
            using (StreamReader reader = new StreamReader(pathFile))
            {
                if (reader == null) ae = false;
            }
            Console.WriteLine(ae);
        }
        public static string LoadEmbeddedResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException($"Ресурс '{resourceName}' не найден.", nameof(resourceName));
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}
