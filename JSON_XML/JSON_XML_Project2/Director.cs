using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public class Director 
    {
        public IBuilder builder;
        public IComposite composite;
        public string _name;
        public string _content;
        
        public Director(string type)
        {
            if(type == "JSON")
            {
                builder = new JSON();
            }
            if(type == "XML")
            {
                builder = new XML();
            }
        }
        public void BuildBranch()
        {
            builder.BuildBranch(_name);
            //builder.CloseBranch();
            composite = builder.GetDocument();
        }
        public void BuildLeaf()
        {
            builder.BuildLeaf(_name, _content);
            //builder.CloseBranch();
            composite = builder.GetDocument();
        }
        public void CloseBranch()
        {
            builder.CloseBranch();
            composite = builder.GetDocument();
        }
    }
}
