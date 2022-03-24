using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Grid levelGrid;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject levelObjectParent;
    [SerializeField] private GameObject levelGoal;
    [SerializeField] private SpawnOrder spawnOrder;
    [SerializeField] private GameObject enemyPrefab;
    private List<SpawnData> toSpawn;
    private List<EnemyController> spawned = new List<EnemyController>();
    public bool lossFlag = false;
    private float timeMod;

    public long tickDiff {
        get;
        private set;
    }
    int id = 1;
    // Start is called before the first frame update
    void Start() {
        toSpawn = spawnOrder.GetSorted();
        GameController.Instance.AddParts(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tickDiff == 0 && toSpawn.Count > 0 && Time.time > toSpawn[0].spawnTime + timeMod) {
            GameObject enemy = Instantiate(enemyPrefab, levelObjectParent.transform);
            enemy.transform.position = spawnPoint.transform.position;
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.spawnData = toSpawn[0];
            if (controller.spawnData.id == 0) {
                controller.spawnData.id = id;
                id++;
            }
            spawned.Add(controller);
            toSpawn.RemoveAt(0);
        }
        if (tickDiff > 0)
            tickDiff--;

        if (toSpawn.Count == 0 && spawned.Count == 0 && !lossFlag) {
            GameController.Instance.SetVictoryState(true);
        } else if (lossFlag) {
            GameController.Instance.SetVictoryState(false);
        }
    }

    public Grid GetLevelGrid() {
        return levelGrid;
    }

    public Vector3 GetGoal() {
        return levelGoal.transform.position;
    }

    public List<EnemyController> GetEnemies() {
        return spawned;
    }

    public void RemoveEnemy(EnemyController ctrl) {
        spawned.Remove(ctrl);
    }

    public void UnspawnEnemy(EnemyController ctrl) {
        RemoveEnemy(ctrl);
        if (toSpawn.Count == 0) {
            toSpawn.Add(ctrl.spawnData);
        } else {
            int index = 0;
            while (index < toSpawn.Count && ctrl.spawnData.spawnTime >= toSpawn[index].spawnTime)
                index++;
            if (index >= toSpawn.Count)
                toSpawn.Add(ctrl.spawnData);
            else
                toSpawn.Insert(index, ctrl.spawnData);
        }
    }
    public void StartRollback(long newTick) {
        tickDiff = HUDController.Instance.ForceTickDiff(newTick);
        timeMod += tickDiff * Time.fixedDeltaTime * 2;
    }
}
