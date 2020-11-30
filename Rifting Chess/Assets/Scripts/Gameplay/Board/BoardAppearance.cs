using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardAppearance : MonoBehaviour
{
    public GameObject selectedParticle;
    public GameObject tileHighlightPrefab;
    public GameObject moveLocationPrefab;
    public GameObject attackLocationPrefab;

    Square highlightedSquare;

    Square[] Board
    {
        get
        {
            return GameManager.instance.boardLogic.map.board;
        }
    }

    // - METHODS
    #region Setup
    public void Start()
    {
        TileAppearance.attackLocationPrefab = attackLocationPrefab;
        TileAppearance.moveLocationPrefab = moveLocationPrefab;
        TileAppearance.tileHighlightPrefab = tileHighlightPrefab;

        selectedParticle.SetActive(false);
    }

    public GameObject AddPiece(GameObject piece, Square square){
        GameObject newPiece = Instantiate(piece, Vector3.zero, Quaternion.identity);
        newPiece.transform.position = square.transform.position;
        return newPiece;
    }
    #endregion
    #region Basic Changes
    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void SelectPiece(GameObject piece)
    {
        selectedParticle.SetActive(true);
        selectedParticle.transform.position = piece.transform.position;

        //MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        //renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        selectedParticle.SetActive(false);
        //MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        // renderers.material = defaultMaterial;
    }

    public void MouseOver(int square_id)
    {
        if( highlightedSquare != null)
        {
            Debug.LogWarning("Highlighted Square was not null");
            highlightedSquare.UnHighlight();
            highlightedSquare = null;
        }
        Board[square_id].Highlight();
        highlightedSquare = Board[square_id];
    }

    public void RemoveMouseOver()
    {
        if (highlightedSquare == null)
        {
            Debug.LogWarning("Highlighted Square was null");
        }
        else
        {
            highlightedSquare.UnHighlight();
            highlightedSquare = null;
        }
    }

    public void FlipBoard() {

    }

    #endregion
    #region Move Highlights
    public void PlaceMoveIndicators(List<int> moveLocations)
    {
        foreach (int id in moveLocations)
        {
            Board[id].ShowMoveIndicator();
        }
    }

    public void PlaceThreatIndicators(List<int> locations)
    {
        foreach (int id in locations)
        {
            Board[id].ShowAttackIndicator();
        }
    }
    public void RemoveAllIndicators()
    {
       foreach( Square square in Board)
        {
            square.HideAttackIndicator();
            square.HideMoveIndicator();
        }
    }

    public void RemoveIndicatorSpecific( List<int> locations)
    {
        foreach ( int id in locations)
        {
            Board[id].HideMoveIndicator();
            Board[id].HideAttackIndicator();
        }
    }
    #endregion

    void OnDestroy()
    {
       
    }
}
