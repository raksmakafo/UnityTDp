using UnityEngine;


public class GameControllerScript : MonoBehaviour
{
    public int cellCount;

    int[] pathID = { };

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
        }
    }

    void CreatePath()
    {

    }

}
