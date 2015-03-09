using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public class Variable
    {
        public const int INT = 0;
        public const int CHAR = 1;
        public const int STRING = 2;
        public const int NULL = -1;

        public List<string> varNames;
        public List<string> values;
        public List<int> varType;

        public Variable()
        {
            varNames = new List<string>();
            values = new List<string>();
            varType = new List<int>();
        }

        public int VarExist(string name)
        {
            for (int i = 0; i < varNames.Count; i++)
            {
                if (StringCompare(varNames[i], name))
                {
                    return i;
                }
            }
            return -1;
        }

        public string GetVarValue(string a)
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


        public void AddVar(string name, string valueOf, int type)
        {
            int idxOfVar = VarExist(name);
            if (idxOfVar == -1)
            {
                varNames.Add(name);
                values.Add(valueOf);
                varType.Add(type);
                
            }
            else
            {
                //Console.WriteLine("OldValue: " + values[idxOfVar]);
                values[idxOfVar] = valueOf;
                varType[idxOfVar] = type;
                //Console.WriteLine("NewValue: " + values[idxOfVar] + "Should be: " + valueOf);
            }

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

        public int GetType(int idx)
        {
            if (idx == -1)
            {
                return NULL;
            }
            return varType[idx];
        }
        public bool ValidVarName(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (i == 0 && (name[i] < 'A' || name[i] > 'Z') && (name[i] < 'a' || name[i] > 'z')) return false;

                if ((name[i] < 'A' || name[i] > 'Z') && (name[i] < 'a' || name[i] > 'z')
                    && (name[i] <= '0' || name[i] >= '9'))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
