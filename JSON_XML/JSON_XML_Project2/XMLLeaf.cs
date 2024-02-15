using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public class XMLLeaf : IComposite
    {
        private string _name;
        private string _content;

        public XMLLeaf(string name, string content)
        {
            _name = name;
            _content = content;
        }
        public string GetName()
        {
            return _name;
        }
        public string Print()
        {
            string result = "";
            result = $"<{_name}>";
            result += $"{_content}\n";
            Console.WriteLine(result);
            result += "\n";
            return result;
        }
    }
}
