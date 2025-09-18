/*************************************************
  * 名稱：GameObjectRadio
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class GameObjectRadio : MonoBehaviour
{
    public RadioGroup radioGroup; // 連結到 RadioGroup
    [Header("Events")]
    public UnityEvent onEnable;
    public UnityEvent onDisable;

    private void OnEnable()
    {
        if (radioGroup != null)
        {
            radioGroup.OnRadioSelected(this); // 當這個GameObject被啟用時通知RadioGroup
        }

        onEnable?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive); // 更改自己的顯示狀態
    }

    public void OnDestroy()
    {
        if (radioGroup != null && radioGroup.radios.Contains(this)) radioGroup.radios.Remove(this);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (radioGroup != null && !radioGroup.radios.Contains(this)) radioGroup.radios.Add(this);
    }
#endif
}
