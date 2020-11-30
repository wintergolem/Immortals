using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyListBuilder : MonoBehaviour {

    public GameObject pieceOptionButton;
    public GameObject pieceBorder;
    public GameObject selectedPiecesDisplay;
    public GameObject availablePiecesDisplay;
    //public Dropdown factionSelector;  //TODO: replace with textfield
    public Text factionText;
    public InputField nameInput;
    public Button saveButton;
    public Text pointsIndicator;

    List<PieceInfo> pieces;
    FactionType activeFaction;
    List<GameObject> createdBorders = new List<GameObject>();
    List<GameObject> createdButtons = new List<GameObject>();
    ArmyList armyList;

    void Start () {
        pieces = PieceList.allPieces;
        PieceBorder.armyBuild = this;
        if (Account.instance.selectedList == null)
        {
            armyList = new ArmyList
            {
                faction = FactionType.Undead,
                displayName = ""
            };
            saveButton.enabled = false;
        }
        else
        {
            armyList = Account.instance.selectedList;
            Account.instance.savedLists.Remove(armyList); //TODO : make opinional
            activeFaction = armyList.faction;
            factionText.text = activeFaction.ToString();
            nameInput.text = armyList.displayName;

            foreach (PieceInfo info in armyList.pieces)
            {
                var button = Instantiate(pieceOptionButton, selectedPiecesDisplay.transform , false);
                button.GetComponent<PieceOptionButton>().Init(this, info, PieceOptionButton.OptionState.Remove);
            }
        }
        PieceBorder.armyBuild = this;
        CreateButtons();
        //LayoutRebuilder.MarkLayoutForRebuild(availablePiecesDisplay.transform as RectTransform);
    }

    void CreateButtons(){
        List<PieceInfo> allPieces = new List<PieceInfo>(pieces);

        CreateBorder(PieceType.King, allPieces);
        CreateBorder(PieceType.Queen, allPieces);
        CreateBorder(PieceType.Bishop, allPieces);
        CreateBorder(PieceType.Knight, allPieces);
        CreateBorder(PieceType.Rook, allPieces);
        CreateBorder(PieceType.Pawn, allPieces);
    }

    void CreateBorder(PieceType pieceType, List<PieceInfo> pieceCollection)
    {
        var border = Instantiate(pieceBorder, availablePiecesDisplay.transform, false);
        var script = border.GetComponent<PieceBorder>();
        List<PieceInfo> infos = pieceCollection.FindAll((obj) => obj.pieceType == pieceType);
        script.Startup(infos, "-" + pieceType.ToString());
        script.CreateButtons(armyList.faction);
        createdBorders.Add(border);
    }

    void CheckSaveButton(){
        saveButton.enabled = armyList.HasKing;
    }

    public bool OptionAdd(PieceInfo info) {
        if (armyList.AttemptAdd(info)){
            CheckSaveButton();
            var button = Instantiate(pieceOptionButton);
            button.GetComponent<PieceOptionButton>().Init(this, info, PieceOptionButton.OptionState.Remove);
            button.transform.SetParent(selectedPiecesDisplay.transform);
            createdButtons.Add(button);
            UpdatePointsIndicator();
            return true;
        }
        return false;
    }

    public void OptionRemove(PieceInfo info){
        armyList.Remove(info);
        UpdatePointsIndicator();
    }

    public void SaveButtonPressed(){
        //get name
        if (!string.IsNullOrEmpty(nameInput.text))
            armyList.displayName = nameInput.text;
        else
            if (string.IsNullOrEmpty(armyList.displayName))
                armyList.displayName = "Default Name";

        //TODO: save armyList to disk
        Account.AssignList(armyList, 0);
        Account.instance.savedLists.Add(armyList);
        LoadManager.SaveAllLists();
        LoadManager.LoadAllLists();

        LoadManager.LoadScene(LoadManager.SceneList.ArmyViewer);
    }

    public void UpdatePointsIndicator()
    {
        pointsIndicator.text = armyList.ListPointsUsed + " /  " + armyList.maxPoints;
    }

    public void BackButton()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
