using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAppearance : MonoBehaviour
{
    public GameObject selectedParticle;
    public GameObject tileHighlightPrefab;
    public GameObject moveLocationPrefab;
    public GameObject attackLocationPrefab;

    private BoardLogic logic;
    private GameObject tileHighlight;
    private List<GameObject> locationHighlights;

    // - METHODS
    #region Setup
    public void Start()
    {
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
        logic = GetComponent<BoardLogic>();
        selectedParticle.SetActive(false);
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

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
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
        locationHighlights = new List<GameObject>();
        foreach (Vector2Int loc in moveLocations)
        {
            GameObject highlight;
            if (logic.PieceAtGrid(loc)){
                highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            } else {
                highlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(loc), Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }
    }
    public void RemoveMoveHighlights()
    {
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
    }
#endregion
}
