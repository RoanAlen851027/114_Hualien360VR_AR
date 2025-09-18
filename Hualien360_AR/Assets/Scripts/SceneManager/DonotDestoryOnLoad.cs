/*************************************************
  * 名稱：DonotDestoryOnLoad
  * 作者：RyanHsu
  * 功能說明：DonotDestoryOnLoad
  * ***********************************************/
using UnityEngine;

public class DonotDestoryOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}