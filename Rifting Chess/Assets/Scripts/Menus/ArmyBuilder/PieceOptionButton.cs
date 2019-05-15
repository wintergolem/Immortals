using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceOptionButton : MonoBehaviour {

    public Text text;
    public Text buttonText;
    PieceInfo piece;
    ArmyListBuilder armyListBuilder;
    public enum OptionState { Add, Remove };
    public OptionState state = OptionState.Add;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Init( ArmyListBuilder builder , PieceInfo info , OptionState state){
        piece = info;
        armyListBuilder = builder;

        text.text = info.displayName;

        this.state = state;
        buttonText.text = state == OptionState.Add ? "Add" : "Remove";
    }

    public void ButtonPressed(){
        switch (state){
            case OptionState.Add:
                if (armyListBuilder.OptionAdd(piece))
                {
                    return;
                }
                break;
            case OptionState.Remove:
                armyListBuilder.OptionRemove(piece);
                Destroy(gameObject);
                break;
        }
    }

    public void OnDestroy()
    {
        armyListBuilder.OptionRemove(piece);
    }
}
