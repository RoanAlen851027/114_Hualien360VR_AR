/*************************************************
  * 名稱：VersionPrint
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;

[DisallowMultipleComponent]
public class VersionPrint : MonoBehaviour
{
    void Start() {
        Debug.Log("Version:" + Application.version); 
    }
}