using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploySelectDisplay : MonoBehaviour
{

    public GameObject dialog;
    public GameObject scrollContent;
    public GameObject pieceDeployButton;
    public List<GameObject> createdButtons = new List<GameObject>();
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateButtons(List<GameObject> pieces)
    {
        if (createdButtons.Count != 0)
        {
            foreach (GameObject button in createdButtons)
            {
                Destroy(button);
            }
        }

        for (int i = 0; i < pieces.Count; i++)
        {
            var button = Instantiate(pieceDeployButton);
            string displayName = pieces[i].GetComponent<Piece>().displayName;
            //Debug.Log(displayName);
            button.GetComponent<DeployPieceButton>().Init(displayName, i);
            button.transform.SetParent(scrollContent.transform);
            createdButtons.Add(button);
        }
    }

    public void ButtonPressed(int pieceIndex)
    {
        DeploymentRunner.instance.PieceSelected(pieceIndex);
        dialog.SetActive(false);
    }
}
