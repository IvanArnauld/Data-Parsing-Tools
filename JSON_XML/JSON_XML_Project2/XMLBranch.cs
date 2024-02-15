using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public class XMLBranch : IComposite
    {
        private string _name;
        private List<IComposite> _children = new List<IComposite>();
        public XMLBranch(string name)
        {
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
        public void AddChild(IComposite child)
        {
            _children.Add(child);
        }

        public string Print()
        {
            string result = "";
            result = $"<{_name}>\n";
            foreach (var child in _children)
            {
                result += child.Print();
                Console.WriteLine(result);
            }
            return result;
        }
    }
}
