using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyListBuilder : MonoBehaviour {

    public GameObject pieceOptionButton;
    public GameObject pieceBorder;
    public GameObject selectedPiecesDisplay;
    public GameObject availablePiecesDisplay;
    public Dropdown factionSelector;
    public InputField nameInput;
    public Button saveButton;
    public Text pointsIndicator;

    List<PieceInfo> pieces;
    FactionType activeFaction;
    List<GameObject> createdBorders = new List<GameObject>();
    List<GameObject> createdButtons = new List<GameObject>();
    ArmyList list;

    void Start () {
        pieces = PieceList.allPieces;
        PieceBorder.armyBuild = this;
        if (Account.instance.selectedList == null)
        {
            activeFaction = FactionType.Zombies;
            list = new ArmyList
            {
                faction = new Zombies(),
                displayName = ""
            };
            saveButton.enabled = false;
        }
        else
        {
            list = Account.instance.selectedList;
            Account.instance.savedLists.Remove(list);
            activeFaction = list.faction.type;
            factionSelector.value = (int)(list.faction.type);
            nameInput.text = list.displayName;

            foreach (PieceInfo info in list.pieces)
            {
                var button = Instantiate(pieceOptionButton);
                button.GetComponent<PieceOptionButton>().Init(this, info, PieceOptionButton.OptionState.Remove);
                button.transform.SetParent(selectedPiecesDisplay.transform);
            }
        }
        PieceBorder.armyBuild = this;
        CreateButtons();
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
        var border = Instantiate(pieceBorder);
        var script = border.GetComponent<PieceBorder>();
        List<PieceInfo> infos = pieceCollection.FindAll((obj) => obj.pieceType == pieceType);
        script.Startup(infos, "-" + pieceType.ToString());
        border.transform.SetParent(availablePiecesDisplay.transform);
        script.CreateButtons(activeFaction);
        createdBorders.Add(border);
    }

    void CheckSaveButton(){
        saveButton.enabled = list.HasKing;
    }

    public void FactionSelectorChanged( ) {
        activeFaction = (FactionType)factionSelector.value;
        createdBorders.ForEach( button => Destroy(button) );
        createdButtons.ForEach(button => Destroy(button));
        CreateButtons();
    }

    public bool OptionAdd(PieceInfo info) {
        if (list.AttemptAdd(info)){
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
        list.Remove(info);
        UpdatePointsIndicator();
    }

    public void SaveButtonPressed(){
        //get name
        if (!string.IsNullOrEmpty(nameInput.text))
            list.displayName = nameInput.text;
        else
            if (string.IsNullOrEmpty(list.displayName))
                list.displayName = "UnNamed";

        switch (activeFaction)
        {
            case FactionType.Phalanx:
                list.faction = new Phalanx();
                break;
            case FactionType.Priest:
                list.faction = new Priest();
                break;
            case FactionType.Zombies:
                list.faction = new Zombies();
                break;
            case FactionType.Warrior:
                list.faction = new Warrior();
                break;
            default:
                print("No value for " + activeFaction.ToString() + " in SaveButtonPressed()");
                break;
        }

        //TODO: save list to disk
        Account.AssignList(list, 0);
        Account.instance.savedLists.Add(list);
        LoadManager.SaveAllLists();
        LoadManager.LoadAllLists();

        LoadManager.LoadScene(LoadManager.SceneList.ArmyViewer);
    }

    public void UpdatePointsIndicator()
    {
        pointsIndicator.text = list.ListPointsUsed + " /  " + list.maxPoints;
    }

    public void BackButton()
    {
        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
