using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_XML_Project2
{
    public class JSON : IBuilder
    {
        public List<Branch> branches = new List<Branch>();
        private Branch _lastOpenedBranch;
        public JSON()
        {
            BuildBranch("{");
            _lastOpenedBranch = branches[0];
        }
        public void BuildBranch(string name)
        {
            if (!branches.Any())
            {
                branches.Add(new Branch(name));
            }
            else
            {
                Branch newBranch = new Branch(name);
                branches[branches.Count - 1].AddChild(newBranch);
                branches.Add(newBranch);
                _lastOpenedBranch = branches[branches.Count - 1];
            }
        }

        public void BuildLeaf(string name, string content)
        {
            if (!branches.Any())
            {
                branches.Add(new Branch(name));
            }
            else
            {
                Leaf newLeaf = new Leaf(name, content);
                branches[branches.Count - 1].AddChild(newLeaf);
                _lastOpenedBranch = branches[branches.Count - 1];
            }
        }

        public void CloseBranch()
        {
            _lastOpenedBranch = branches[branches.Count - 2];
            List<Branch> ogBranches = branches;
            List<Branch> reversedBranches = branches;
            reversedBranches.Reverse();
            branches = ogBranches;
            for(int i = 0; i < branches.Count; i++)
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
