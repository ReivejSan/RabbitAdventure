using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemySpawner : MonoBehaviour
{
    public enum SpawnState{SPAWNING, WAITING, COUNTING};
    
    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] wave;
    private int nextWave = 0;
    
    public float timeBetweenWave = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start() {
        waveCountdown = timeBetweenWave;
    }

    void Update() {
        if(state == SpawnState.WAITING) {
            if(!EnemyIsAlive()) {
                WaveCompleted();
            }
            else {
                return;
            }
        }
        
        if(waveCountdown <= 0) {
            if(state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave(wave[nextWave]));
            }
        }
        else {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted() {
        Debug.Log("Wave Complete");
        
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWave;

        if(nextWave+1 > wave.Length - 1) {
            nextWave = 0;
            Debug.Log("All Wave Complete");
            SceneManager.LoadScene("ChooseStage");
        }

        nextWave++;
    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0) {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawn wave" +_wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.spawnRate);
        }

        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemy(Transform _enemy) {
        Debug.Log("Spawn Enemy");
        Instantiate(_enemy, transform.position, transform.rotation);
    }
}
