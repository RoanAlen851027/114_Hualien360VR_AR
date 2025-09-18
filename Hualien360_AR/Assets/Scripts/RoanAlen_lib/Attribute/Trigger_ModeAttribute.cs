/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class Trigger_ModeAttribute : PropertyAttribute
    {
        public string TriggerFieldName { get; private set; }

        public Trigger_ModeAttribute(string triggerFieldName)
        {
            TriggerFieldName = triggerFieldName;
        }
    }
}

