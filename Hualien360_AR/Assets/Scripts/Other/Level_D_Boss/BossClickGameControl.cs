using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
/********************************
---------------------------------
著作者：RoanAlen
用途：
---------------------------------
*********************************/
public class BossClickGameControl : MonoBehaviour
{
    private BossAttributeTile[,] board;
    private int rows = 3;
    private int cols = 6;

    public bool OnSelect;
    [HideInInspector]
    public bool hasLink = false;

    [Header("當前選擇")]
    public GemValue gemValue;
    public int gemCount;

    public AudioPlayScriptable OnSelect_SFX;
    public AudioPlayScriptable OffSelect_SFX;

    public void RegisterBoard(BossAttributeTile[,] tiles)
    {
        board = tiles;
    }

    public void Correct()
    {
        OnSelect = false;
        hasLink = false;
        gemValue = GemValue.None;
        gemCount = 0;
        if (board != null)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j].IsLink==true)
                    {
                        board[i, j].IsCorrect();
                    }
                    
                }
            }
        }
    }

    public void ResetSelect()
    {
        OnSelect = false;
        hasLink = false;
        gemValue = GemValue.None;
        gemCount = 0;

        if (board != null)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j].IsLink = false;
                    board[i, j].IsSelect(); // 這裡會呼叫 breathingScale.Turn(IsLink)
                }
            }
        }
    }

    public void CheckConnectedAttribute(int r, int c)
    {
        if (board == null) return;

        BossAttributeTile clickedTile = board[r, c];
        string targetAttr = clickedTile.attribute;

        // 先執行 flood fill，找出相連的 tiles
        bool[,] visited = new bool[rows, cols];
        List<BossAttributeTile> connectedTiles = FloodFill(r, c, targetAttr, visited);

        // 取得這一群的原始狀態（以點擊的那一格為主）
        bool newState = !clickedTile.IsLink;

        // 重設所有 tile 的 IsLink 為 false（要在之後）
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board[i, j].IsLink = false;
                board[i, j].IsSelect();
            }
        }
        // 再對這一群設定新的狀態
        foreach (var tile in connectedTiles)
        {
            tile.IsLink = newState;
            tile.IsSelect();
        }
        // 檢查是否有任何 tile 被選中
        hasLink = false;
        for (int i = 0; i < rows && !hasLink; i++)
        {
            for (int j = 0; j < cols && !hasLink; j++)
            {
                if (board[i, j].IsLink)
                {
                    hasLink = true;
                }

            }
        }
        OnSelect = hasLink;
        //當我選擇情況下
        if (OnSelect == true)
        {
            switch (targetAttr)
            {
                case "None":
                    gemValue = GemValue.None;
                    break;
                case "CP":
                    gemValue = GemValue.CP;
                    break;
                case "NP":
                    gemValue = GemValue.NP;
                    break;
                case "A":
                    gemValue = GemValue.A;
                    break;
                case "FC":
                    gemValue = GemValue.FC;
                    break;
                case "AC":
                    gemValue = GemValue.AC;
                    break;
            }
            gemCount = connectedTiles.Count;
            OnSelect_SFX.Play();
        }
        else
        {
            OffSelect_SFX.Play();
            gemValue = GemValue.None;
            gemCount = 0;
        }

        Debug.Log($"{targetAttr} x {connectedTiles.Count}");
    }

    List<BossAttributeTile> FloodFill(int x, int y, string target, bool[,] visited)
    {
        List<BossAttributeTile> result = new List<BossAttributeTile>();

        if (x < 0 || x >= rows || y < 0 || y >= cols) return result;
        if (visited[x, y]) return result;
        if (board[x, y].attribute != target) return result;

        visited[x, y] = true;
        result.Add(board[x, y]);

        result.AddRange(FloodFill(x + 1, y, target, visited));
        result.AddRange(FloodFill(x - 1, y, target, visited));
        result.AddRange(FloodFill(x, y + 1, target, visited));
        result.AddRange(FloodFill(x, y - 1, target, visited));

        return result;
    }
}