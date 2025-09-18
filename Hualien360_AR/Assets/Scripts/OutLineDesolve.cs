/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
using UnityEngine;

public class OutLineDesolve : MonoBehaviour
{
    public float floatValue;

    public Material material;

    void Start()
    {
    }

    void Update()
    {
        material.SetFloat("_SourceGlowDissolveFade", floatValue);
    }
}
