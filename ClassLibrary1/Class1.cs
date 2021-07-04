using System;

namespace ClassLibrary1
{
    public class Class1
    {
        public string Name;

        public bool CheckName(string nameImput)
        {
            if (Name != nameImput)
                return false;
            else
                return true;
        }
    }
}
