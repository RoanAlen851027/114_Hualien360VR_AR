using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class BossBoardGenerator : MonoBehaviour
{
    public BossClickGameControl boss_GameControl;
    public List<BossAttributeTile> tile;
    public BossAttributeTile[,] attributeTiles_Text;
    public string[,] attributes = new string[3, 6];

    public int rows = 0;
    public int cols = 0;
    public BossBoardDataSO board_DataSO;

    [Header("測試用")]
    public int Counter;

    private void SetBoard(int Count)
    {
        
        for (int i = 0; i < tile.Count; i++)
        {
            board_DataSO.Board_Data[Count].SendValue();
            tile[i].attribute = board_DataSO.Board_Data[Count].Value[i];
            //Debug.Log($"參數 {tile[i].attribute} = {board_DataSO.Board_Data[0].Value[i]} + {i}");
        }
    }

    public void SendBoardAttribute(int EnergyType)
    {
        SetBoard(EnergyType);

        for (int i = 0; i < tile.Count; i++)
        {
            tile[i].IsFadeIn();
        }

        rows = attributes.GetLength(0);
        cols = attributes.GetLength(1);
        attributes = new string[3, 6]
{
                    { tile[0].attribute, tile[1].attribute, tile[2].attribute, tile[3].attribute, tile[4].attribute, tile[5].attribute },
                    { tile[6].attribute, tile[7].attribute, tile[8].attribute, tile[9].attribute, tile[10].attribute, tile[11].attribute },
                    { tile[12].attribute, tile[13].attribute, tile[14].attribute, tile[15].attribute, tile[16].attribute, tile[17].attribute }
};
        // 確保 tile 列表有正確的元素
        if (tile.Count != rows * cols)
        {
            Debug.LogError($"Tile count mismatch! Expected {rows * cols}, but got {tile.Count}");
            return;
        }

        attributeTiles_Text = new BossAttributeTile[rows, cols];

        // 填充二維陣列並設置每個 TestAttributeTile
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int index = r * cols + c; // 計算對應的 tile 索引
                if (index < tile.Count)
                {
                    // 設置每個 tile 的屬性
                    tile[index].SetAttribute(attributes[r, c], r, c);
                    attributeTiles_Text[r, c] = tile[index];
                }
                else
                {
                    Debug.LogError($"Tile at index {index} is out of range!");
                }
            }
        }

        // 註冊 board
        boss_GameControl.RegisterBoard(attributeTiles_Text);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SendBoardAttribute(Counter);


            Debug.Log("我按下PPPPP");
        }
    }
}
