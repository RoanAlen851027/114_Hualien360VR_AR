/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class ModeBoolAttribute : PropertyAttribute
    {
        public string[] Labels { get; private set; }

        public ModeBoolAttribute(params string[] labels)
        {
            Labels = labels;
        }
    }
}

