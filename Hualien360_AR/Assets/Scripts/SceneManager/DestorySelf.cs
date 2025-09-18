/*************************************************
  * 名稱：DestorySelf
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/

using UnityEngine;

public class DestorySelf : MonoBehaviour
{
    [SerializeField] float destorySec = 10f;

    void OnEnable()
    {
        this.Invoke(() => { DestroyImmediate(gameObject); }, destorySec);
    }
}