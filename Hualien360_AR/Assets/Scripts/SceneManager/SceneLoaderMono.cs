/*************************************************
  * 名稱：SceneLoaderMono
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderMono : MonoBehaviour
{
    public string sceneName;
    public void Load() => SceneLoader.StartSceneLoad(sceneName);
}