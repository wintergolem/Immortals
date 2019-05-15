using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceBorder : MonoBehaviour
{
    public bool isHidden = false;

    public List<GameObject> pieceButtons;
    public List<PieceInfo> pieceInfos;
    public Button collapseButton;
    public Text labelText;

    public GameObject pieceOptionButton;
    public static ArmyListBuilder armyBuild;

    public void Startup( List<PieceInfo> pieceInfos , string pieceTypeName)
    {
       this.pieceInfos = pieceInfos;
        labelText.text = pieceTypeName;
        labelText.fontStyle = FontStyle.BoldAndItalic;
    }

    public void CreateButtons( FactionType activeFaction)
    {
        List<PieceInfo> byFaction = new List<PieceInfo>(pieceInfos);
        byFaction.RemoveAll(piece => piece.factionType != activeFaction);

        foreach (PieceInfo info in byFaction)
        {
            var button = Instantiate(pieceOptionButton);
            button.GetComponent<PieceOptionButton>().Init(armyBuild, info, PieceOptionButton.OptionState.Add);
            button.transform.SetParent(armyBuild.availablePiecesDisplay.transform);
            pieceButtons.Add(button);
        }
    }

    public void CollapseButtonPressed()
    {
        SwitchHidden();
    }

    void SwitchHidden()
    {
        pieceButtons.ForEach((obj) =>
        {
            obj.gameObject.SetActive(isHidden);
        });
        isHidden = !isHidden;
    }

   public void OnDestroy()
    {
        pieceButtons.ForEach((GameObject obj) =>
        {
            Destroy(obj);  
        });
    }
}
