using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour {

    public Button startButton;
    public Dropdown levelSelect;
    public Dropdown player1Faction;
    public Dropdown player2Faction;
    public Dropdown opponentType;

    public void ButtonPressed(){
        Account.CreateAccount();
        switch (opponentType.value){
            case 0:
                Account.instance.opponentType = PlayerType.AI;
                break;
            case 1:
                Account.instance.opponentType = PlayerType.Local;
                break;
        }

        Account.AssignFaction(0,player1Faction.value);
        Account.AssignFaction(1,player2Faction.value);

        switch (levelSelect.value){
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;
        }
    }
}
