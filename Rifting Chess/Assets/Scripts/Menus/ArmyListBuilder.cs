using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyListBuilder : MonoBehaviour {

    public GameObject pieceOptionButton;
    public GameObject selectedPiecesDisplay;
    public GameObject availablePiecesDisplay;
    public Dropdown factionSelector;
    public InputField nameInput;
    public Button saveButton;

    List<PieceInfo> pieces;
    FactionType activeFaction;
    List<GameObject> createdButtons;
    ArmyList list;

    void Start () {
        pieces = PieceList.allPieces;
        activeFaction = FactionType.Zombies;
        createdButtons = new List<GameObject>();
        list = new ArmyList   {
            faction = new Phalanx(),
            displayName = ""
        };
        saveButton.enabled = false;
        CreateButtons();
    }

    void CreateButtons(){
        List<PieceInfo> byFaction = new List<PieceInfo>(pieces);
        byFaction.RemoveAll( piece => piece.factionType != activeFaction);

        foreach (PieceInfo info in byFaction) {
            var button = Instantiate(pieceOptionButton);
            button.GetComponent<PieceOptionButton>().Init(this,info);
            button.transform.SetParent(availablePiecesDisplay.transform);
            createdButtons.Add(button);
        }
    }

    void CheckSaveButton(){
        saveButton.enabled |= list.Complete();
    }

    public void FactionSelectorChanged( ) {
        activeFaction = (FactionType)factionSelector.value;
        createdButtons.ForEach( button => Destroy(button) );
        CreateButtons();
    }

    public bool OptionAdd(PieceInfo info) {
        if (list.AttemptAdd(info)){
            CheckSaveButton();
            return true;
        }
        return false;
    }

    public void OptionRemove(PieceInfo info){
        list.Remove(info.pieceType);
    }

    public void SaveButtonPressed(){
        //get name
        list.displayName = string.IsNullOrEmpty(nameInput.text) ? "Default" : nameInput.text;

        switch (activeFaction){
            case FactionType.Phalanx:
                list.faction = new Phalanx();
                break;
            case FactionType.Priest:
                list.faction = new Priest();
                break;
            case FactionType.Zombies:
                list.faction = new Zombies();
                break;
            default:
                print("No value for " + activeFaction.ToString() + " in SaveButtonPressed()");
                break;
        }

        //TODO: save list to disk
        Account.AssignList(list,0);

        LoadManager.LoadScene(LoadManager.SceneList.GameCreate);
    }
}
