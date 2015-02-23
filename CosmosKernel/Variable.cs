using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public class Variable
    {
        public List<string> varNames { get; set; }
        public List<object> values { get; set; }

        public Variable()
        {
            varNames = new List<string>();
            values = new List<object>();
        }
        
        public object VarExist(string a)
        {
            for (int i = 0; i < varNames.Count; i++)
            {
                if (StringCompare(varNames[i], a))
                {
                    return values[i];
                }
            }
            return null;
        }
        
        public void AddVar(string name, object valueOf)
        {
            varNames.Add(name);
            values.Add(valueOf);
        }

        public bool StringCompare(string a, string b)
        {
            if (a.Length != b.Length) return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }
}
