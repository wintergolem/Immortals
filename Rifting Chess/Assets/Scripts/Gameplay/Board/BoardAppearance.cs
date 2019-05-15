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

    private GameObject tileHighlight;
    public List<GameObject> locationHighlights = new List<GameObject>();
    private List<GameObject> attackHighlights = new List<GameObject>();

    // - METHODS
    #region Setup
    public void Start()
    {
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
        selectedParticle.SetActive(false);

        for (int i = 0; i < 20; i++)
        {
            locationHighlights.Add( Instantiate(moveLocationPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform) );
            locationHighlights[i].SetActive(false);
        }
        for (int i = 0; i < 10; i++ )
        {
            attackHighlights.Add(Instantiate(attackLocationPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform));
            attackHighlights[i].SetActive(false);
        }
    }

    public GameObject AddPiece(GameObject piece, int col, int row){
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Vector3.zero, Quaternion.identity);
        newPiece.transform.position = Geometry.PointFromGrid(gridPoint);
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

    public void MouseOver(Vector2Int gridPoint)
    {
        tileHighlight.SetActive(true);
        tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void RemoveMouseOver()
    {
        tileHighlight.SetActive(false);
    }

    public void FlipBoard() {

    }

    #endregion
    #region Move Highlights
    public void PlaceMoveHighlights(List<Vector2Int> moveLocations)
    {
        foreach (Vector2Int loc in moveLocations)
        {
            foreach (GameObject tile in locationHighlights)
            {
                if (!tile.activeSelf)
                {
                    tile.SetActive(true);
                    tile.transform.position = Geometry.PointFromGrid(loc);
                    break;
                }
            }
        }
    }

    public void PlaceThreatenHighlights(List<Vector2Int> locations)
    {
        foreach (Vector2Int loc in locations)
        {
            foreach (GameObject tile in attackHighlights)
            {
                if (!tile.activeSelf)
                {
                    tile.SetActive(true);
                    tile.transform.position = Geometry.PointFromGrid(loc);
                    break;
                }
            }
        }
    }
    public void RemoveMoveHighlights()
    {
        foreach (GameObject highlight in locationHighlights)
        {
            highlight.SetActive(false);
        }
        foreach (GameObject highlight in attackHighlights)
        {
            highlight.SetActive(false);
        }
    }

    public void RemoveMoveHighlights( List<Vector2Int> locations)
    {
        foreach (GameObject highlight in locationHighlights)
        {
            if (locations.Contains( Geometry.GridFromPoint(highlight.transform.position)))
            {
                highlight.SetActive(false);
            }
        }
        foreach (GameObject highlight in attackHighlights)
        {
            if (locations.Contains(Geometry.GridFromPoint(highlight.transform.position)))
            {
                highlight.SetActive(false);
            }
        }
    }
#endregion
}
