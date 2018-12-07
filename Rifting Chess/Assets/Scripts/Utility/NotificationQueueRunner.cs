using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotificationQueueRunner {
    List<Action> actionList = new List<Action>();
    Action endAction;

    public bool running = false;

    public NotificationQueueRunner()
    {
    }

    public void AddAction( Action action) {
        actionList.Add(action);
    }

    public void SetEndAction( Action action){
        endAction = action;
    }

    public void  RunNext(){
        if (actionList.Count > 0) {
            var task = actionList[0];
            actionList.RemoveAt(0);
            task();
        }  else {
            running = false;
            actionList = new List<Action>();
            GameManager.instance.hasFocus = true;
            if (endAction != null) endAction();
        }
    }
}
