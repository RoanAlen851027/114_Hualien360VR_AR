/*************************************************
  * 名稱：ButtonInactiveDelay
  * 作者：RyanHsu
  * 功能說明：OnClick後短暫的button.inactive，避免重覆觸發
  * ***********************************************/
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button)), DisallowMultipleComponent]
public class ButtonInactiveDelay : MonoBehaviour
{
    [Header("Setting")]
    [Range(0f, 5f)] public float delayTime = 1f;
    public UnityEvent OnCompleted;

    [Header("Dispend")]
    public Button button;
    bool defaultRaycastTarget;

    void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        if (button == null) button = GetComponent<Button>();
        defaultRaycastTarget = button?.image?.raycastTarget ?? false;
    }

    private void OnDisable()
    {
        this.CancelInvoke();
        if (button != null && button.image != null)
            button.image.raycastTarget = defaultRaycastTarget;
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
        button = null;
    }

    void OnClick()
    {
        if (button != null && button.image != null)
        {
            defaultRaycastTarget = button.image.raycastTarget;
            button.image.raycastTarget = false;
            this.Invoke(() =>
            {
                button.image.raycastTarget = defaultRaycastTarget;
                if (OnCompleted != null) OnCompleted.Invoke();
            }, delayTime);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        button = GetComponent<Button>();
    }
#endif
}