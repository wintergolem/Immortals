using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Account {

    public static Account instance = new Account();

    public PlayerType opponentType = PlayerType.Local;
    public ArmyList selectedList;
    public ArmyList opponentList;

    public List<ArmyList> savedLists;

    private Account()
    {
        savedLists = new List<ArmyList>();
        CreateAccount();
    }

    void CreateAccount(){
        selectedList = ArmyList.BuildBasicWarrior();
        opponentList = ArmyList.BuildBasicZombie();
        
    }

    public ArmyList GetArmyList( int player ) {
        if (selectedList == null) {
            selectedList = ArmyList.BuildBasicWarrior();
            opponentList = ArmyList.BuildBasicZombie();
        }
        return player == 0 ? selectedList : opponentList;
    }

    public static void AssignList( ArmyList list, int player){
        switch( player ) {
            case 0:
                instance.selectedList = list;
                break;
            case 1:
                instance.opponentList = list;
                break;
        }
    }
}
