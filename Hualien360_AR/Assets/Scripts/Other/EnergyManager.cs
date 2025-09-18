using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class EnergyManager : MonoBehaviour
{

    [Header("Dependce")]
    public List<EnergyValue> energy_Values;

    [SerializeField]
    private bool showColorText;
    [SerializeField]
    private bool showLerpImg;

    [SerializeField]
    private UnityEvent Energy_Event;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Energy_Event.Invoke();
        }
    }
    private void Start()
    {
        if (PAC_Score.instance != null && PAC_Score.instance.GetResult==false)
        {
            PAC_Score.instance.GetResult = true;
            PAC_Score.instance.GetReultScore();
        }

        List<int> scores = PAC_Score.instance.Reslut_score;

        if (showColorText == true)
        {
            for (int i = 0; i < energy_Values.Count; i++)
            {
                energy_Values[i].InitScoreColor(scores[i]);
            }
        }
        else if (showLerpImg == true)
        {
            for (int i = 0; i < energy_Values.Count; i++)
            {
                energy_Values[i].InitLerpScoreEightyColor(scores[i]);
            }
            Energy_Event?.Invoke();
        }
        else
        {
            for (int i = 0; i < energy_Values.Count; i++)
            {
                energy_Values[i].InitScore(scores[i]);
            }
        }
    }



    public void UpdateValue()
    {

        List<int> scores = PAC_Score.instance.Reslut_score;

        if (showColorText == true)
        {
            for (int i = 0; i < energy_Values.Count; i++)
            {
                energy_Values[i].InitScoreColor(scores[i]);
            }
        }
        else
        {
            for (int i = 0; i < energy_Values.Count; i++)
            {
                energy_Values[i].InitScore(scores[i]);
            }
        }

    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (energy_Values != null && energy_Values.Count == 0)
        {
            energy_Values = GetComponentsInChildren<EnergyValue>().ToList();
        }
    }

#endif
}
