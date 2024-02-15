using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public interface IComposite
    {
        public virtual void AddChild(IComposite child) { }
        public abstract string Print();
    }
}
