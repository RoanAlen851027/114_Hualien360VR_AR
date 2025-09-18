/*************************************************
  * 名稱：Extensions
  * 作者：RyanHsu
  * 功能說明：附加運算集
  * ***********************************************/
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Linq;
using System.Text;
using System.Reflection;


public static class LayerMaskExtensions
{

    /// <summary> 檢查Layer是否涵蓋在LayerMask中 </summary>
    public static bool Contains(this LayerMask layerMask, int layerIndex)
    {
        int mask = layerMask.value;
        return (mask & (1 << layerIndex)) != 0;
    }
}

public static class TransformExtensions
{
    public static void RemoveAllChild(this Transform transform)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
            //transform.DetachChildren();
        }
    }

    /// <summary> 在transform下新增prefab </summary>
    /// <param name="prefab">prefab物件</param>
    /// <param name="name">GameObject Name</param>
    /// <returns>回傳GameObject</returns>
    public static GameObject AddChild(this Transform transform, GameObject prefab, string name = "")
    {
        GameObject instance = UnityEngine.Object.Instantiate(prefab, transform);
        if (name != "") instance.name = name;
        return instance;
    }
    /// <summary> 在transform下新增prefab </summary>
    /// <param name="prefab">prefab物件</param>
    /// <param name="name">GameObject Nam</param>
    /// <returns>回傳"<T>"</returns>
    public static T AddChild<T>(this Transform transform, GameObject prefab, string name = "") => AddChild(transform, prefab, name).GetComponent<T>();

    /// <summary> 搜出直接子集合中，包含inactive的類別 </summary>
    public static T[] GetComponents<T>(this Transform transform, bool includeInactive)
    {
        if (includeInactive)
        {
            T[] array = transform.GetComponentsInChildren<Transform>(true)
                .Where(m => m.parent == transform)
                .Select(m => m.GetComponent<T>())
                .Where(m => m != null)
                .ToArray();
            return array;
        } else
        {
            return transform.GetComponentsInChildren<T>();
        }
    }

    /// <summary> 搜出直接子集合，包含disable </summary>
    public static Transform[] GetAllChild(this Transform transform)
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform tf in transform)
        {
            list.Add(tf);
        }
        return list.ToArray();
    }

    /// <summary> transform.gameObject.SetActive </summary>
    public static void SetActive(this Transform transform, bool io)
    {
        transform.gameObject.SetActive(io);
    }

    #region 以下簡易Pool操作

    /// <summary> 獲取count數量的Pool實例 </summary>
    public static Transform[] GetPoolItem(this Transform transform, GameObject poolPrefab, int count)
    {
        Transform[] tfs = transform.GetComponentsInChildren<Transform>(true)
            .Where(m => m.name == poolPrefab.name && m.gameObject.activeSelf == false)
            .Take(count).ToArray();
        if (tfs.Length < count)
        {
            int lessCount = count - tfs.Length;
            for (int i = 0; i < lessCount; i++)
            {
                transform.AddChild(poolPrefab, poolPrefab.name).SetActive(false);
            }
            return GetPoolItem(transform, poolPrefab, count);
        } else
        {
            tfs.ForEach(m => m.gameObject.SetActive(true));
            return tfs;
        }
    }

    public static void PoolClear(this Transform transform, GameObject poolPrefab)
    {
        Transform[] tfs = transform.GetComponentsInChildren<Transform>(true)
            .Where(m => m.name == poolPrefab.name).ToArray();
        tfs.ForEach(m => m.gameObject.SetActive(false));
    }
    #endregion
}

public static class GameObjectExtensions
{
    public static GameObject ReName(this GameObject gameObject, string name)
    {
        gameObject.name = name;
        return gameObject;
    }

    // Radio 開關：激活指定的 SiblingIndex，停用其它，並返回激活的 GameObject
    public static GameObject Radio(this GameObject gameObject, int index)
    {
        Transform tf = gameObject.transform;

        // 確保父物件是激活狀態，否則無法操作子物件
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("The parent GameObject is inactive.");
            return null;
        }

        // 確保給定的 index 在子物件範圍內
        if (index < 0 || index >= tf.childCount)
        {
            //關閉所有子物件
            for (int i = 0; i < tf.childCount; i++)
            {
                tf.GetChild(i).gameObject.SetActive(false);
            }
            return null; // 如果 index 超出範圍，返回 null
        }

        // 遍歷所有子物件
        for (int i = 0; i < tf.childCount; i++)
        {
            // 激活指定 index 的子物件，停用其他子物件
            tf.GetChild(i).gameObject.SetActive(i == index);
        }

        // 返回激活的子物件
        return tf.GetChild(index).gameObject;
    }
    public static GameObject Radio(this GameObject gameObject, string name)
    {
        return gameObject.Radio(gameObject?.transform.Find(name)?.GetSiblingIndex() ?? -1);
    }


    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
    {
        Transform tf = gameObject.transform;
        component = gameObject.GetComponentInChildren<T>();
        return component != null;
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] component)
    {
        Transform tf = gameObject.transform;
        component = gameObject.GetComponentsInChildren<T>();
        return component != null;
    }

}

public static class ArrayExtensions
{
    /// <summary>ForEach</summary>
    public static void ForEach<T>(this T[] array, Action<T> action)
    {
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(array[i]);
        }
    }

    /// <summary>帶編號的ForEach</summary>
    public static void ForEach<T>(this T[] array, Action<int, T> action)
    {
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(i, array[i]);
        }
    }

    /// <summary>取雙值的ForEach</summary>
    public static void ForEach<T1, T2>(this T1[] array1, T2[] array2, Action<T1, T2> action)
    {
        int count1 = array1.Length;
        int count2 = array2.Length;
        for (int i = 0; i < count1 && i < count2; i++)
        {
            action.Invoke(array1[i], array2[i]);
        }
    }
    public static void ForEach<T1, T2>(this T1[] array1, List<T2> list2, Action<T1, T2> action)
    {
        array1.ForEach(list2.ToArray(), action);
    }
}

public static class ListExtensions
{
    /// <summary>將 List 泛型 T 輸出為字串</summary>
    public static string ToString<T>(this List<T> list)
    {
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.Append("[ ");

        for (int i = 0; i < list.Count; i++)
        {
            if (i > 0)
            {
                strBuilder.Append(" , ");
            }
            strBuilder.Append(i).Append(":").Append(list[i].ToString());
        }

        strBuilder.Append(" ]");
        return strBuilder.ToString();
    }

    /// <summary>帶編號的ForEach</summary>
    public static void ForEach<T>(this List<T> list, Action<int, T> action)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(i, list[i]);
        }
    }

    /// <summary>取雙值的ForEach</summary>
    public static void ForEach<T1, T2>(this List<T1> list1, List<T2> list2, Action<T1, T2> action)
    {
        int count1 = list1.Count;
        int count2 = list2.Count;
        for (int i = 0; i < count1 && i < count2; i++)
        {
            action.Invoke(list1[i], list2[i]);
        }
    }
    public static void ForEach<T1, T2>(this List<T1> list1, T2[] array2, Action<T1, T2> action)
    {
        list1.ForEach(array2.ToList(), action);
    }
}

public static class stringExtensions
{
    /// <summary>將string去除空格與換行</summary>
    public static string format(this string str)
    {
        if (str == null)
            throw new ArgumentNullException(nameof(str));

        return str.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "");
    }

    /// <summary>將Object轉為JsonString</summary>
    public static string ToJsonString<T>(this T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        return Newtonsoft.Json.JsonConvert.SerializeObject(value);
    }

    /// <summary>將JsonString轉為Object</summary>
    public static T FromJsonString<T>(this string value, Newtonsoft.Json.JsonSerializerSettings settings = null)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (settings != null)
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, settings);
        else
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
    }

    /// <summary>將string轉為boolArray</summary>
    public static bool[] ConvertBinaryStringToBoolArray(this string binaryString)
    {
        bool[] selections = new bool[binaryString.Length];
        for (int i = 0; i < binaryString.Length; i++)
        {
            selections[i] = binaryString[i] == '1';
        }
        return selections;
    }

    // 檢查字符串是否為有效的二進位制表示
    public static bool IsBinary(this string text)
    {
        if (string.IsNullOrEmpty(text)) return false;

        foreach (char c in text)
        {
            if (c != '0' && c != '1')
            {
                return false;
            }
        }
        return true;
    }
}

public static class BoolArrayExtensions
{
    /// <summary>將string轉為boolArray</summary>
    public static string ConvertBoolArrayToBinaryString(this bool[] boolArray)
    {
        System.Text.StringBuilder binaryStringBuilder = new System.Text.StringBuilder();

        foreach (bool value in boolArray)
        {
            binaryStringBuilder.Append(value ? '1' : '0');
        }

        return binaryStringBuilder.ToString();
    }
}

public static class intExtensions
{
    public static string ToStringWithSign(this int number, string color = "")
    {
        if (color != "")
        {
            string[] colors = color.Split(':');
            if (colors.Length == 2)
            {
                string sign = number >= 0 ? "+" : "";
                string c = number >= 0 ? colors[0] : colors[1];
                return $"<color={c}>{sign}{number.ToString("N0")}</color>";
            }
        }

        return (number >= 0 ? "+" : "") + number.ToString("N0");
    }
}

public static class floatExtensions
{
    public static TweenerCore<float, float, FloatOptions> To(this float value, float endValue, float duration, Action<float> action)
    {
        return DOTween.To(() => value, x => value = x, endValue, duration).OnUpdate(() => action.Invoke(value)).OnComplete(() => action.Invoke(value));
    }

    /// <summary>
    /// 將浮點數四捨五入到指定的小數位數
    /// </summary>
    /// <param name="value">要進行四捨五入的浮點數值</param>
    /// <param name="decimalPlaces">小數點後保留的位數</param>
    /// <returns>四捨五入後的浮點數值</returns>
    public static float RoundToDecimalPlaces(this float value, int decimalPlaces)
    {
        // 計算 10 的指定次方
        float factor = Mathf.Pow(10, decimalPlaces);
        // 乘以 factor，進行四捨五入，再除以 factor
        return Mathf.Round(value * factor) / factor;
    }

    public static string ToStringWithSign(this float number)
    {
        return (number >= 0 ? "+" : "") + number.ToString("N0");
    }
}

public static class GUIStyleExtensions
{
    public static GUIStyle FontSize(this GUIStyle style, int size)
    {
        style.fontSize = size;
        return style;
    }
    public static GUIStyle FontColor(this GUIStyle style, Color32 color)
    {
        style.normal.textColor = color;
        return style;
    }
}

public static class MonoBehaviourExtensions
{
    /// <summary>使用反射取得變量名稱</summary>
    static string GetMethodName(Delegate del) => del.Method.Name;
    public static void Invoke(this MonoBehaviour mono, Action method, float time)
    {
        mono.Invoke(GetMethodName(method), time);
    }
    public static void InvokeRepeating(this MonoBehaviour mono, Action method, float time, float repeatRate)
    {
        mono.InvokeRepeating(GetMethodName(method), time, repeatRate);
    }
    public static void CancelInvoke(this MonoBehaviour mono, Action method)
    {
        mono.CancelInvoke(GetMethodName(method));
    }
}

public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator anim, string param_name)
    {
        bool io = false;
        AnimatorControllerParameter[] parameters = anim.parameters;
        foreach (var item in parameters)
        {
            if (item.name == param_name)
            {
                io = true;
                break;
            }
        }
        return io;
    }
    public static AnimatorControllerParameter GetParameter(this Animator anim, string param_name)
    {
        AnimatorControllerParameter param = null;
        AnimatorControllerParameter[] parameters = anim.parameters;
        foreach (var item in parameters)
        {
            if (item.name == param_name)
            {
                param = item;
                break;
            }
        }
        return param;
    }

    public static AnimatorControllerParameterType? GetParameterType(this Animator anim, string param_name)
    {
        AnimatorControllerParameter param = null;
        AnimatorControllerParameter[] parameters = anim.parameters;
        foreach (var item in parameters)
        {
            if (item.name == param_name)
            {
                param = item;
                break;
            }
        }
        return param == null ? null : param.type;
    }
}

public static class QuaternionExtensions
{
    public static Vector4 ToVector4(this Quaternion quat) => new Vector4(quat.x, quat.y, quat.z, quat.w);
}

public static class Vector4Extensions
{
    public static Quaternion ToQuaternion(this Vector4 v4) => new Quaternion(v4.x, v4.y, v4.z, v4.w);
}

public static class Vector3Extensions
{
    public static Vector3 VectorIO(this Vector3 v1, Vector3 v2, bool3 b) =>
         new Vector3(b.x ? v2.x : v1.x, b.y ? v2.y : v1.y, b.z ? v2.z : v1.z);

    public static Vector3 SetValue(this Vector3 value, float v) => new Vector3(v, v, v);

    public static Vector3 SetX(this Vector3 v1, float value) =>
        new Vector3(value, v1.y, v1.z);
    public static Vector3 SetY(this Vector3 v1, float value) =>
        new Vector3(v1.x, value, v1.z);
    public static Vector3 SetZ(this Vector3 v1, float value) =>
        new Vector3(v1.x, v1.y, value);
}

public static class UnityObject
{
    /// <summary>
    /// 利用反射獲取Getter多層欄位值
    /// </summary>
    public static object eval(this object currentObject, string fieldName)
    {
        string[] fieldPath = fieldName.Split('.'); // 支援多層級，如 wallet.cash

        foreach (string field in fieldPath)
        {
            if (currentObject == null)
            {
                Debug.LogError($"當前對象為 null，無法繼續獲取 {field} 欄位值！");
                return null;
            }

            FieldInfo fieldInfo = currentObject.GetType().GetField(field, BindingFlags.Public | BindingFlags.Instance);
            if (fieldInfo == null)
            {
                Debug.LogError($"欄位 {field} 不存在於 {currentObject.GetType().Name} 中！");
                return null;
            }

            currentObject = fieldInfo.GetValue(currentObject); // 取得下一層的值
        }

        return currentObject;
    }

    /// <summary>
    /// 利用反射設置Setting多層欄位值
    /// </summary>
    public static bool eval(this object currentObject, string fieldName, object value)
    {
        string[] fieldPath = fieldName.Split('.'); // 支援多層級，如 wallet.cash

        for (int i = 0; i < fieldPath.Length; i++)
        {
            string field = fieldPath[i];
            FieldInfo fieldInfo = currentObject.GetType().GetField(field, BindingFlags.Public | BindingFlags.Instance);

            if (fieldInfo == null)
            {
                Debug.LogError($"欄位 {field} 不存在於 {currentObject.GetType().Name} 中！");
                return false;
            }

            if (i == fieldPath.Length - 1)
            {
                // 最後一層，設置值
                if (fieldInfo.FieldType.IsValueType || fieldInfo.FieldType.IsAssignableFrom(value.GetType()))
                {
                    fieldInfo.SetValue(currentObject, value);
                    return true;
                } else
                {
                    Debug.LogError($"無法將 {value.GetType().Name} 賦值給 {fieldInfo.FieldType.Name}！");
                    return false;
                }
            }

            // 進入下一層，對 struct 必須特殊處理
            currentObject = fieldInfo.GetValue(currentObject);
        }

        return false;
    }
}
