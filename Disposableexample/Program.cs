using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposableexample
{
    class Program
    {
        static void Main(string[] args)
        {
            // burada biz dispose etmeliyiz.
            disposeexample ip = new disposeexample();
            Console.WriteLine("File content");
            Console.WriteLine(ip.ReadFile());
            Console.ReadLine();

            //// burası auto dispose
            //using (disposeexample ip = new disposeexample())
            //{
            //    Console.WriteLine("File content");
            //    Console.WriteLine(ip.ReadFile());
            //    Console.ReadLine();
            //}
        }
    }
    public class disposeexample : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        private FileStream filestream;
        private StreamReader streamreader;
        public List<string> disposableList = null;
        public string ReadFile()
        {
            Console.WriteLine("start reading file");
            try
            {
                disposableList = new List<string>();
                for (int i = 0; i < 100; i++)
                {
                    disposableList.Add(i.ToString());
                }

                filestream = new FileStream(Path.Combine(Environment.CurrentDirectory, "TextFile1.txt"), FileMode.Open, FileAccess.Read);
                streamreader = new StreamReader(filestream);
                Console.WriteLine("reading file completed");
                return streamreader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                streamreader.Close();
                filestream.Close();
                this.Dispose();
            }

        }
        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Calleing dispose method");
            if (disposed)
                return;

            if (disposing)
            {
                filestream.Dispose();
                streamreader.Dispose();
            }
            Console.WriteLine("object disposed");
            disposableList = null;
            disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
