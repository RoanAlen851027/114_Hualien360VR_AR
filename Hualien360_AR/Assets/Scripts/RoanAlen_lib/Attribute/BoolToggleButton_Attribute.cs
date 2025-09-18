/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class BoolToggleButton_Attribute : PropertyAttribute
    {
        public string TrueLabel { get; private set; }
        public string FalseLabel { get; private set; }

        public BoolToggleButton_Attribute(string trueLabel = "ON", string falseLabel = "OFF")
        {
            TrueLabel = trueLabel;
            FalseLabel = falseLabel;
        }
    }
}