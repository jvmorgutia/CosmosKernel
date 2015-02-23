using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
   public class Directory
    {
        public string name { get; set; }
        public Directory parent { get; set; }
        public List<Directory> children { get; set; }


        public Directory(String name, Directory parent)
        {
            children = new List<Directory>();
            this.name = name;
            this.parent = parent;
        }

        public void addFolder(Directory newDirectory){
            this.children.Add(newDirectory);
        }
        public void addFolder(String name, Directory parent)
        {
            Directory newDirectory = new Directory(name, parent);
            this.children.Add(newDirectory);

        }
        public String ToString()
        {
            return name + "";
        }

    }
}
