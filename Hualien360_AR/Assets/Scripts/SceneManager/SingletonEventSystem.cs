/*************************************************
  * 名稱：SingletonEventSystem
  * 作者：RyanHsu
  * 功能說明：在換場景的情況下，保持唯一EventSystem
  * ***********************************************/
using UnityEngine;
using UnityEngine.EventSystems;

public class SingletonEventSystem : MonoBehaviour
{
    private static EventSystem instance;

    public static void Enable(bool io) { if (instance != null) instance.enabled = io; }

    void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<EventSystem>();
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
    }
}
