using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Player Notication Center

public enum EventToTrigger { StartOfTurn , EndOfTurn , FriendlyCaptured , EnemyCaptured  };

//public delegate void StartOfTurnDelegate(GameManager manager);
//public delegate void EndOfTurnDelegate(GameManager manager);
//public delegate void FriendlyPieceCaptured(GameManager manager);
//public delegate void EnemyPieceCaptured(GameManager manager);

public delegate void PowerButtonPressed(GameManager manager);

public class PlayerNoticationCenter  {
    public static PlayerNoticationCenter playerOne = new PlayerNoticationCenter();
    public static PlayerNoticationCenter playerTwo = new PlayerNoticationCenter();

    private PlayerNoticationCenter(){
    }

    public  List<Action> TurnStart = new List<Action>();
    public List<Action> TurnEnd = new List<Action>();
    public List<Action> FriendlyCaptured = new List<Action>();
    public List<Action> EnemyCaptured = new List<Action>();

    public NotificationQueueRunner runner = new NotificationQueueRunner();

    public  void TriggerEvent( EventToTrigger trigger){
        switch (trigger)  {
            case EventToTrigger.EndOfTurn:
                SetRunner(TurnEnd.ToArray());
                break;
            case EventToTrigger.StartOfTurn:
                SetRunner(TurnStart.ToArray());
                break;
            case EventToTrigger.FriendlyCaptured:
                SetRunner(FriendlyCaptured.ToArray());
                break;
            case EventToTrigger.EnemyCaptured:
                Debug.Log("Enemy Captured called");
               SetRunner(EnemyCaptured.ToArray());
                break;
        }
    }
    void SetRunner( Action[] actions) {
        foreach (Action a in actions){
            runner.AddAction(a);
        }

        if (!runner.running){
            runner.running = true;
            GameManager.instance.hasFocus = false;
            runner.RunNext();
        }
    }
}


#endregion

#region Game Notication Center

public enum GameEventTrigger{ ClickedOnSquare, HoverSquare , RemoveHover ,
    PowerPressed, SwitchingPlayers, RightClick}; 

//public delegate void HoverOverSquare(Vector2Int gridpoint);
//public delegate void ClickedOnSquare(Vector2Int gridpoint);
//public delegate void SwitchingPlayers();
//public delegate void RemoveHover();
//public delegate void RightClicked();

public class GameNoticationCenter {
    public static GameNoticationCenter instance = new GameNoticationCenter();

    public  List<Action> HoverSquare = new List<Action>();
    public  List<Action> ClickedSquare = new List<Action>();
    public  List<Action> RemoveHover = new List<Action>();
    public  List<Action> SwitchingPlayers = new List<Action>();
    public  List<Action> RightClick = new List<Action>();
    public List<Action> PowerPressed = new List<Action>();

    public NotificationQueueRunner runner= new NotificationQueueRunner() ;

    public static void TriggerEvent( GameEventTrigger trigger){
        switch (trigger){
            case GameEventTrigger.ClickedOnSquare:
                if (instance.ClickedSquare.Count != 0)
                    instance.SetRunner(instance.ClickedSquare.ToArray());
                else
                    GameManager.instance.LeftMouseClick();
                break;
            case GameEventTrigger.HoverSquare:
                if (instance.HoverSquare != null) //should be fine to just run this, some functions need to be on main thread
                    instance.HoverSquare.ForEach( x => {
                    x();
                } );
                //instance.SetRunner(instance.HoverSquare.ToArray());
                break;
            case GameEventTrigger.RemoveHover:
                if (instance.RemoveHover != null) //should be fine to just run this, some functions need to be on main thread
                    instance.RemoveHover.ForEach(x => {
                        x();
                    });
                break;
            case GameEventTrigger.SwitchingPlayers:
               // instance.SetRunner(instance.SwitchingPlayers.ToArray());
                break;
            case GameEventTrigger.RightClick:
                instance.SetRunner(instance.RightClick.ToArray());
                break;
            case GameEventTrigger.PowerPressed:
                instance.SetRunner(instance.PowerPressed.ToArray());
                break;
        }
    }

    void SetRunner(Action[] actions)  {
        foreach (Action a in actions){
            runner.AddAction(a);
        }

        if (!runner.running) {
            runner.running = true;
            GameManager.instance.hasFocus = false;
            runner.RunNext();
        }
    }
    #endregion
}