/*************************************************
  * 名稱：DirtyMethod
  * 作者：RyanHsu
  * 功能說明：髒運算元
  * ***********************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>泛型Dirty</summary>
[Serializable]
public class Dirty<T>
{
    private T dirty;
    public T defaultValue { set { dirty = value; } }

    public Dirty(T initialValue)
    {
        dirty = CloneValue(initialValue);
    }

    /// <summary>檢查新值是否改變，並更新內部狀態。</summary>
    public bool IsDirty(T newValue)
    {
        if (!EqualityComparer<T>.Default.Equals(dirty, newValue))
        {
            dirty = CloneValue(newValue);
            return true;
        }
        return false;
    }

    /// <summary>嘗試複製值</summary>
    private T CloneValue(T value)
    {
        if (value is ICloneable cloneable)
        {
            return (T)cloneable.Clone();
        }
        return value; // 對於非 ICloneable 的類型，直接返回值（對於值類型這是安全的）
    }
}

/// <summary>驗證Method指針</summary>
[Serializable]
public class DirtyMethod<T> where T : class
{
    T dirty = default(T);

    public DirtyMethod(T value) { dirty = value; }

    public T defaultValue { set => dirty = value; }

    public bool isDirty(T value)
    {
        if (dirty != value)
        {
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }
}

/// <summary>驗證Cloneable或Equatable型式的內容dirty</summary>
public class DirtyEqual<T> where T : ICloneable, IEquatable<T>
{
    T dirty = default(T);

    public DirtyEqual(T value) { dirty = value; }

    public T defaultValue { set => dirty = (T)value.Clone(); get => dirty; }

    public bool isDirty(T value)
    {
        if (!dirty.Equals(value))
        {
            defaultValue = value;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class DirtyTransform
{
    Transform dirty;
    Transform obsolete;//存放舊指針
    bool isInitlize = true;

    public DirtyTransform(Transform value) { dirty = value; }

    public Transform obsoleteValue { get { return obsolete; } }
    public Transform defaultValue { set { dirty = value; } }

    /// <summary>因為Transform的null是有意義的，所以obsolete需要多一個判斷是否尚未有初值</summary>
    public bool TryGetObsolete(out Transform value)
    {
        value = obsolete;
        return !isInitlize;
    }

    public bool isDirty(Transform value)
    {
        if (dirty != value)
        {
            obsolete = dirty;
            dirty = value;
            isInitlize = false;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class DirtyBool
{
    bool dirty;

    public DirtyBool(bool value) { dirty = value; }

    public bool defaultValue { set { dirty = value; } }

    public bool isDirty(bool value)
    {
        if (dirty != value)
        {
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class Dirtyfloat
{
    float dirty;
    float obsolete;//存放舊指針，用以判斷enter/release

    public Dirtyfloat(float value) { dirty = value; }

    public float obsoleteValue { get { return obsolete; } }
    public float defaultValue { set { dirty = value; } }

    public bool isDirty(float value)
    {
        if (dirty != value)
        {
            obsolete = dirty;
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class DirtyInt
{
    int dirty;
    int obsolete;//存放舊指針，用以判斷enter/release

    public DirtyInt(int value) { dirty = value; }

    public int obsoleteValue { get { return obsolete; } }
    public int defaultValue { set { dirty = value; } }

    public bool isDirty(int value)
    {
        if (dirty != value)
        {
            obsolete = dirty;
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class DirtyStr
{
    string dirty;
    string obsolete;//存放舊指針，用以判斷enter/release

    public DirtyStr(string value) { dirty = value; }

    public string obsoleteValue { get { return obsolete; } }
    public string defaultValue { set { dirty = value; } }

    public bool isDirty(string value)
    {
        if (dirty != value)
        {
            obsolete = dirty;
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }
}

[Serializable]
public class DirtyVector
{
    Vector4 dirty;
    Vector4 obsolete;//存放舊指針，用以判斷enter/release

    public DirtyVector(Vector4 value) { dirty = value; }

    public Vector4 obsoleteValue { get { return obsolete; } }
    public Vector4 defaultValue { set { dirty = value; } }

    public bool isDirty(Vector4 value)
    {
        if (!dirty.Equals(value))
        {
            obsolete = dirty;
            dirty = value;
            return true;
        } else
        {
            return false;
        }
    }

}

[Serializable]
public class DirtyParams<T> where T : struct
{
    T[] dirty = default(T[]);

    public DirtyParams(T[] value) { dirty = value; }

    public T[] defaultValue { set { dirty = value; } }

    public bool isDirty(params T[] value)
    {
        bool io = false;
        int i = 0;
        if (dirty == null || dirty.Length != value.Length)
        {
            dirty = value;
            io = true;
        } else
        {
            foreach (T m in value)
            {
                if (!Equals(dirty[i], m))
                {
                    dirty = value;
                    io = true;
                    break;
                }
                i++;
            };
        }
        return io;
    }
}

