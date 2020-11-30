using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

    public int UniqueID { get; private set; } = -1; //should also be equal to it's position in the array

    public Piece piece;
    //public Map map;
    public Square[] neighbors = new Square[8];
    //[7]  [0]  [1] 
    //[6]  [X]  [2] 
    //[5]  [4]  [3] 


    public int spawnValue = 0;
    public string baseSpawn = ""; //set in editor, hacking/cheating, for standard chess so the autodeployer knows what to put here

    #region private
    readonly bool[] bThreated = new bool[2];

    #endregion

    #region calculated variables 
    public bool IsEmpty {
        get {
            return piece == null;
        }
    }

    public bool Threatened(int player)
    {
        return bThreated[player];
    }
    public bool IsSpawnPoint(int playerNumber)
    {
        return spawnValue == playerNumber+1; //add one cause playernumber indexes from 0 instead of 1
    }

    #endregion


    public void Start()
    {
    }

    public void RemovePiece() {
        piece = null;
    }

    public void AddPiece(Piece_Base piece) {
        if( !IsEmpty )
            throw new System.Exception("There is already a piece here" );

        this.piece = piece as Piece;
        piece.square = this;
    }

    public void AddThreat(int playerNumber)
    {
        bThreated[playerNumber] = true;
    }

    public void RemoveThreat(){
        bThreated[0] = false;
        bThreated[1] = false;
    }

    public void SetUID( int UID ){ UniqueID = UID; }


    #region appearance

    GameObject highlight;
    bool highlightActive = false;
    GameObject moveIndicator;
    bool moveIndicatorActive = false;
    GameObject attackIndicator;
    bool attackIndicatorActive = false;

    public void Highlight()
    {
        if (highlight == null) highlight = Instantiate(TileAppearance.tileHighlightPrefab, transform.position, Quaternion.identity, transform);
        if( !highlightActive)
        {
            highlightActive = true;
            highlight.SetActive(true);
        }
    }

    public void UnHighlight()
    {
        if (highlight)
        { 
            highlightActive = false;
            highlight.SetActive(false);
        }
    }

    public void ShowMoveIndicator()
    {
        if (moveIndicator == null) moveIndicator = Instantiate(TileAppearance.moveLocationPrefab, transform.position, Quaternion.identity, transform);
        if( !moveIndicatorActive)
        {
            moveIndicator.SetActive(true);
            moveIndicatorActive = true;
        }
    }

    public void HideMoveIndicator()
    {
        if (moveIndicator != null)
        {
            moveIndicatorActive = false;
            moveIndicator.SetActive(false);
        }
    }

    public void ShowAttackIndicator()
    {
        if( attackIndicator == null) attackIndicator = Instantiate(TileAppearance.attackLocationPrefab, transform.position, Quaternion.identity, transform);
        if( !attackIndicatorActive)
        {
            attackIndicatorActive = true;
            attackIndicator.SetActive(true);
        }
    }

    public void HideAttackIndicator()
    {
        if (attackIndicator != null)
        {
            attackIndicatorActive = false;
            attackIndicator.SetActive(false);
        }
    }

    #endregion
}
