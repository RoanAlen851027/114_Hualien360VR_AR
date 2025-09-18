/*************************************************
  * 名稱：ReplaceMaterials
  * 作者：RyanHsu
  * 功能說明：取代底下全部(指定layer)的Material
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//禁止多載         [DisallowMultipleComponent]
//#if UNITY_EDITOR
//執行階段執行     [ExecuteAlways]
//#endif
//依賴項           [RequireComponent(typeof CLASS)]
public class ReplaceMaterials : MonoBehaviour
{
    public Material m_Material = default;
    public LayerMask m_Layers;

    void Start()
    {
        if (m_Material == default) return;

        MeshRenderer[] renderers = transform.GetComponentsInChildren<MeshRenderer>().Where(m => m_Layers.Contains(m.gameObject.layer)).ToArray();

        foreach (var renderer in renderers)
        {
            int length = renderer.materials.Length;
            List<Material> materials = new List<Material>();
            for (int i = 0; i <= length; i++)
            {
                materials.Add(m_Material);
            }
            renderer.materials = materials.ToArray();
        }
    }

}