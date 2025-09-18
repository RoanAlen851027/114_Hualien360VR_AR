/*************************************************
  * 名稱：Bindable
  * 作者：RyanHsu
  * 功能說明：這是一個加入可比較項的Binding函數，
  * 其中value會顯示在Inspector內做設定，且OnValueChanged
  * 監聽其改變
  * ***********************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public interface Invokeable
{
    void Invoke();
}

[Serializable]
public class Bindable<T> : Invokeable, IEquatable<T>
{
    [SerializeField] T value;

    public Action<T> OnChanged;

    public Bindable(T initialValue = default)
    {
        value = initialValue;
    }

    public T Get() => value;

    public T Set(T value)
    {
        if (!EqualityComparer<T>.Default.Equals(this.value, value))
        {
            this.value = value;
            if (OnChanged != null) OnChanged.Invoke(value);
        }
        return this.value;
    }

    public T Override(T value)
    {
        if (!EqualityComparer<T>.Default.Equals(this.value, value))
        {
            this.value = value;
        }
        return this.value;
    }

    public Type type => typeof(T);

    public void Invoke()
    {
        OnChanged?.Invoke(value);
    }

    public bool Equals(T other)
    {
        return EqualityComparer<T>.Default.Equals(value, other);
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        if (obj is Bindable<T> bindable)
        {
            return Equals(bindable.value);
        }
        return Equals(obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(value);
    }

    public static bool operator ==(Bindable<T> left, Bindable<T> right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(Bindable<T> left, Bindable<T> right)
    {
        return !(left == right);
    }

    #region Static

    static Dictionary<string, Bindable<T>> dic = new Dictionary<string, Bindable<T>>();

    public static Bindable<T> SetData(string id, Bindable<T> bindable)
    {
        dic[typeof(T).ToString() + "." + id] = bindable;
        return bindable;
    }

    public static Bindable<T> SetData(string id, T value)
    {
        Bindable<T> bindable = GetData(id);
        bindable.value = value;
        return bindable;
    }

    public static Bindable<T> GetData(string id)
    {
        if (dic.TryGetValue(typeof(T).ToString() + "." + id, out Bindable<T> bindable))
        {
            return bindable;
        } else
        {
            return SetData(id, new Bindable<T>());
        }
    }

    #endregion
}


