using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;


public class GameControllerScript : MonoBehaviour
{
    public int cellCount;

    int[] pathID = { 59, 49, 39, 29, 19, 18, 17, 16, 26, 36, 35, 34, 33, 32, 22, 12 };

    List<CellSCR> AllCells = new List<CellSCR>();

    public GameObject cellPref;
    public Transform cellGroup;

    void Start()
    {
        // Create the cells at the start of the game
        CreateCells();
        CreatePath();
    }
    void CreateCells()
    {
        for(int i = 0; i < cellCount; i++)
        {
            GameObject tmpCell = Instantiate(cellPref);
            tmpCell.transform.SetParent(cellGroup, false);
            tmpCell.GetComponent<CellSCR>().id = i + 1;
            tmpCell.GetComponent<CellSCR>().SetState(0);
            AllCells.Add(tmpCell.GetComponent<CellSCR>());
        }
    }

    void CreatePath()
    {
        for(int i = 0; i < pathID.Length; i++)
        {
         AllCells[pathID[i] - 1].SetState(1);
        }
    }

}
