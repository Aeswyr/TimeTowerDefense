using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject clockCanvas;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private ModeHandler playerMode;
    [SerializeField] private ClockController clockController;
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private LevelController currentLevel;
    public long tickDiff {
        get {return currentLevel.tickDiff;}
    }

    public bool lossFlag {
        get {return currentLevel.lossFlag;}
        set {currentLevel.lossFlag = value;}
    }
    private Mode gamemode;
    public Mode Gamemode {
        get {return gamemode;}
        set {
            if (gamemode == Mode.TIME && value != Mode.TIME)
                DeactivateTime();
            else if (value == Mode.TIME && gamemode != Mode.TIME)
                ActivateTime();
            gamemode = value;
        }
    }

    private int ammo, parts;

    public Grid GetLevelGrid() {
        return currentLevel.GetLevelGrid();
    }

    public Vector3 GetGoal() {
        return currentLevel.GetGoal();
    }

    public List<EnemyController> GetEnemies() {
        return currentLevel.GetEnemies();
    }

    public void RemoveEnemy(EnemyController ctrl) {
        currentLevel.RemoveEnemy(ctrl);
    }

    public void UnspawnEnemy(EnemyController ctrl) {
        currentLevel.UnspawnEnemy(ctrl);
    }

    public void SetVictoryState(bool state) {
        gameStateText.gameObject.SetActive(true);
        if (state)
            gameStateText.text = "Win!!";
        else
            gameStateText.text = "Lose...";
    }

    public bool TrySpendAmmo(int count) {
        if (count > ammo)
            return false;
        ammo -= count;
        HUDController.Instance.DisplayAmmo(ammo);
        return true;
    }

    public bool TrySpendParts(int count) {
        if (count > parts)
            return false;
        parts -= count;
        HUDController.Instance.DisplayParts(parts);
        return true;
    }

    public void AddAmmo(int count) {
        ammo += count;
        HUDController.Instance.DisplayAmmo(ammo);
    }

    public void AddParts(int count) {
        parts += count;
        HUDController.Instance.DisplayParts(parts);
    }

    public void ActivateTime() {
        clockCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        Time.timeScale = 0;
        clockController.OpenClock();
    }
    public void DeactivateTime() {
        clockCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void SyncPlayerMode() {
        playerMode.SyncMode();
    }

    public void StartRollback(long newTick) {
        currentLevel.StartRollback(newTick);
    }

    public void ResetLevelData() {
        HUDController.Instance.ForceTickDiff(0);
    }

}

public enum Mode {
    DEFAULT, PLACE, MOVE, TIME, 
}
