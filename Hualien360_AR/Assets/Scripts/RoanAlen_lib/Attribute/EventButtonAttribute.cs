/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class EventButtonAttribute : PropertyAttribute
    {
        public string ButtonLabel;
        public string FieldToTrigger;

        public EventButtonAttribute(string label, string fieldToTrigger)
        {
            ButtonLabel = label;
            FieldToTrigger = fieldToTrigger;
        }
    }
}