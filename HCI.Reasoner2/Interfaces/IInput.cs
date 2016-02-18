using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserModeling
{
    public interface IInput
    {
        void Load(object obj);
        void UnLoad(object obj);

        void Load (object obj, params object[] parameters);
        void UnLoad (object obj, params object[] parameters);
    }
}
