/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

namespace RoanAlen.Attribute
{
    public class RandomButtonAttribute : PropertyAttribute
    {
        public float Min;
        public float Max;

        public RandomButtonAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}
