using UnityEngine;

namespace ARWT.Core{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {        
        public static T Instance { get; private set; } = default;

        public virtual void Awake()
        {
            var component = GetComponent<T>();
            if (Instance == default)
            {
                Instance = component;
                DontDestroyOnLoad(component);
            }
            else if (Instance != component)
            {
                Debug.Log("Instance verbosed. Destroy it.");
                Destroy(gameObject);
            }
        }
    }
}
