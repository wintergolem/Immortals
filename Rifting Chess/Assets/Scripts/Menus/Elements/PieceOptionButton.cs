using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceOptionButton : MonoBehaviour {

    public Text text;
    public Text buttonText;
    PieceInfo piece;
    ArmyListBuilder armyListBuilder;

    private enum OptionState {Add, Remove};
    OptionState state = OptionState.Add;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init( ArmyListBuilder builder , PieceInfo info){
        piece = info;
        armyListBuilder = builder;

        text.text = info.displayName;
    }

    public void ButtonPressed(){
        switch (state){
            case OptionState.Add:
                if (armyListBuilder.OptionAdd(piece)){
                    SwitchState(OptionState.Remove);
                }
                break;
            case OptionState.Remove:
                armyListBuilder.OptionRemove(piece);
                SwitchState(OptionState.Add);
                break;
        }
    }

    void SwitchState( OptionState option ) {
        if (option == OptionState.Add){
            transform.SetParent(armyListBuilder.availablePiecesDisplay.transform);
            buttonText.text = "Add";
        } else {
            transform.SetParent(armyListBuilder.selectedPiecesDisplay.transform);
            buttonText.text = "Remove";
        }
        state = option;
    }

}
