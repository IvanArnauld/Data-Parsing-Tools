using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public interface IBuilder
    {
        void BuildBranch(string name);
        void BuildLeaf(string name, string content);
        void CloseBranch();
        IComposite GetDocument();
    }
}
