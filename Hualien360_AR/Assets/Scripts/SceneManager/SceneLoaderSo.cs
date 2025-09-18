/*************************************************
  * 名稱：SceneLoaderSo
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "SceneLoaderSo", menuName = "Custom/SceneLoaderSo")]
public class SceneLoaderSo : ScriptableObject
{
    [SerializeField] string sceneName;

    public void Load()
    {
        SceneLoader.StartSceneLoad(sceneName);
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
        string assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

        if (!string.IsNullOrEmpty(assetName) && string.IsNullOrEmpty(sceneName))
        {
            sceneName = assetName;
        }
    }
#endif

}

