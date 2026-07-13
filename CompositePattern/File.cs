using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    public class FileItem : IFileSystem 
    { 
        private string _name;

        public FileItem(string name)
        {
            _name = name;
        }

        public void Display()
        {
            Console.WriteLine("File : " + _name);
        }
    }
}
