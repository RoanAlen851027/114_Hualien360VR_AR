/*************************************************
  * 名稱：RayCast
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;

public class RaycastClick : MonoBehaviour
{
    DispatchEvent dispatcher = DispatchEvent.GetInstance();

    private Collider m_collider = null;

    [SerializeField]
    private LayerMask layerMask; // 用於設定要檢測的 Layer

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // 使用指定的 layerMask
            {
                m_collider = hit.collider;

                if (dispatcher.OnPointerDown3D != null)
                {
                    dispatcher.OnPointerDown3D.Invoke(hit.collider);
                }

                if (m_collider.TryGetComponent(out IPointerDown3D item))
                {
                    item.OnPointerDown3D(m_collider);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && m_collider != null)
        {
            if (dispatcher.OnPointerClick3D != null)
            {
                dispatcher.OnPointerClick3D.Invoke(m_collider);
            }

            if (m_collider.TryGetComponent(out IPointerClick3D item))
            {
                item.OnPointerClick3D(m_collider);
            }
            m_collider = null;
        }
    }

#if UNITY_EDITOR
    // 在編輯器中繪製射線
    private void OnDrawGizmos()
    {
        // 檢查攝影機是否可用
        if (Camera.main != null)
        {
            // 繪製射線
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red; // 設定射線顏色
            Gizmos.DrawRay(ray.origin, ray.direction * 100); // 繪製射線，長度為100單位
        }
    }
#endif
}
