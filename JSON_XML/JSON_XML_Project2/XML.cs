using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public class XML : IBuilder
    {
        public List<XMLBranch> branches = new List<XMLBranch>();
        private XMLBranch _lastOpenedBranch;
        public XML()
        {
            BuildBranch("root");
            _lastOpenedBranch = branches[0];
        }
        public void BuildBranch(string name)
        {
            if (!branches.Any())
            {
                branches.Add(new XMLBranch(name));
            }
            else
            {
                XMLBranch newBranch = new XMLBranch(name);
                branches[branches.Count - 1].AddChild(newBranch);
                branches.Add(newBranch);
                _lastOpenedBranch = branches[branches.Count - 1];
            }
        }

        public void BuildLeaf(string name, string content)
        {
            if (!branches.Any())
            {
                branches.Add(new XMLBranch(name));
            }
            else
            {
                XMLLeaf newLeaf = new XMLLeaf(name, content);
                branches[branches.Count - 1].AddChild(newLeaf);
                _lastOpenedBranch = branches[branches.Count - 1];
            }
        }

        public void CloseBranch()
        {
            _lastOpenedBranch = branches[branches.Count - 2];
            List<XMLBranch> ogBranches = branches;
            List<XMLBranch> reversedBranches = branches;
            reversedBranches.Reverse();
            branches = ogBranches;
            for (int i = 0; i < branches.Count; i++)
            {
                branches.Add(reversedBranches[i]);
            }

        }

        public IComposite GetDocument()
        {
            return branches[0];
        }
    }
}
