using UnityEngine;
using System.Collections.Generic;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/

[CreateAssetMenu(fileName = "BossBoardData", menuName = "ScriptableObjects/Board_Data", order = 1)]
public class BossBoardDataSO : ScriptableObject
{
    public List<BoardData> Board_Data;
}

[System.Serializable]
public class BoardData
{
    public string AttributeName;
    public List<GemValue> gemValues;
    [HideInInspector]
    public List<string> Value;

    public void SendValue()
    {
        for (int i = 0; i < gemValues.Count; i++)
        {
            switch (gemValues[i])
            {
                case GemValue.None:
                    Value[i] = "None";
                    break;
                case GemValue.CP:
                    Value[i] = "CP";
                    break;
                case GemValue.NP:
                    Value[i] = "NP";
                    break;
                case GemValue.A:
                    Value[i] = "A";
                    break;
                case GemValue.FC:
                    Value[i] = "FC";
                    break;
                case GemValue.AC:
                    Value[i] = "AC";
                    break;
            }
        }
    }
}
