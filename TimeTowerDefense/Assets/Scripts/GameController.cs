using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Grid levelGrid;
    public Mode Gamemode {
        get;
        set;
    }
    public Grid GetLevelGrid() {
        return levelGrid;
    }

}

public enum Mode {
    DEFAULT, PLACE, MOVE, TIME, 
}
