using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActiveWhenAwake : MonoBehaviour
{
    [SerializeField] UnityEvent onAwake;
    [SerializeField] UnityEvent onEnable;
    [SerializeField] UnityEvent onDisable;
    [SerializeField] UnityEvent onDestroy;

    void Awake()
    {
        onAwake.Invoke();
    }

    public void OnEnable()
    {
        onEnable.Invoke();
    }

    public void OnDisable()
    {
        onDisable.Invoke();
    }

    public void OnDestroy()
    {
        onDestroy.Invoke();
    }
}
