/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class DynamicHidenAttribute : PropertyAttribute
    {
        public string ConditionFieldName { get; private set; }

        public DynamicHidenAttribute(string conditionFieldName)
        {
            ConditionFieldName = conditionFieldName;
        }
    }
}
