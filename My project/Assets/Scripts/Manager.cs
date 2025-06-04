using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum gameStatus
{
    next, play, gameover, win, yourLose
}


public class Manager : Loader<Manager>
{
    public bool IsGameStarted { get; private set; } = false;
    [SerializeField]
    int totalWaves = 10;
    [SerializeField]
    Text totalMoneyLabel;
    [SerializeField]
    Text currentWave;
    [SerializeField]
    Text totalEscapedLabel;
    [SerializeField]
    Text playBtnLabel;
    [SerializeField]
    Button playBtn;
    [SerializeField]
    GameObject spawnPoint;
    [SerializeField]
    Enemy[] enemies;
    [SerializeField]
    Text totalHeartsLabel;

    [SerializeField]
    int totalEnemies = 5;
    [SerializeField]
    int enemiesPerSpawn;

    int waveNumber = 0;
    int totalMoney = 10;
    int totalHearts = 5;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled = 0;
    int whichEnemiesToSpawn = 0;
    int enemiesToSpawn = 0;


    public int WaveNumber
    {
        get { return waveNumber; }
    }

    gameStatus currentState = gameStatus.play;

    public List<Enemy> EnemyList = new List<Enemy>();



    const float spawnDelay = 0.5f;


    int spawnedEnemiesCount = 0;


    public int TotalEscaped
    {
        get
        {
            return totalEscaped;
        }
        set
        {
            totalEscaped = value;
        }
    }

    public int RoundEscaped
    {
        get
        {
            return roundEscaped;
        }
        set
        {
            roundEscaped = value;
        }
    }

    public int TotalKilled
    {
        get
        {
            return totalKilled;
        }
        set
        {
            totalKilled = value;
        }
    }

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            totalMoneyLabel.text = TotalMoney.ToString();
        }
    }


    public int TotalHearts
    {
        get
        {
            return totalHearts;

        }
        set
        {
            totalHearts = value;
            totalHeartsLabel.text = TotalHearts.ToString();
        }
    }

    void Start()
    {
        playBtn.gameObject.SetActive(false);
        ShowMenu();

        // Проверка массива врагов
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                Debug.LogError($"❗ Враг с индексом {i} в массиве 'enemies' не задан (null). Проверь инспектор.");
            }
        }
    }


    private void Update()
    {
        HandleEscape();


    }


    IEnumerator Spawn()
    {
        while (spawnedEnemiesCount < totalEnemies)
        {
            int enemiesToSpawn = Mathf.Min(enemiesPerSpawn, totalEnemies - spawnedEnemiesCount);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    // Выбираем случайного врага
                    Enemy enemyToSpawn = enemies[Random.Range(0, enemies.Length)];

                    if (enemyToSpawn == null)
                    {
                        Debug.LogError("❌ ОШИБКА: Один из врагов в массиве enemies[] равен null. Проверь инспектор.");
                        continue;
                    }

                    // Спавним врага
                    Enemy newEnemy = Instantiate(enemyToSpawn);
                    newEnemy.transform.position = spawnPoint.transform.position;

                    float healthMultiplier = Mathf.Pow(1.5f, waveNumber - 1); // волна 1 = x1, 2 = x1.5, 3 = x2.25 и т.д.
                    newEnemy.ScaleHealth(healthMultiplier);

                    spawnedEnemiesCount++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }





    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);

    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);

    }

    public void DestroyEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }


        EnemyList.Clear();

    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }
    public void subtractMoney(int amount)
    {
        TotalMoney = Mathf.Max(0, TotalMoney - amount);
    }


    public void minusHeart(int amount)
    {
        TotalHearts -= amount;
        if (TotalHearts <= 0)
        {
            TotalHearts = 0; // Чтобы не было отрицательных
            SetCurrentGameState();
            ShowMenu(); // Показать кнопку “PLAY AGAIN!”
            DestroyEnemies(); // Можно добавить, чтобы очистить врагов
        }
    }



    public void IsWaveOver()
    {
        totalEscapedLabel.text = "Escaped" + TotalEscaped + "/ 10";

        if ((RoundEscaped + TotalKilled) == totalEnemies)
        {
            if (waveNumber <= enemies.Length)
            {
                enemiesToSpawn = waveNumber;

            }
            SetCurrentGameState();
            ShowMenu();
        }
    }

    public void SetCurrentGameState()
    {
        if (TotalHearts <= 0 || totalEscaped >= 10)
        {
            currentState = gameStatus.yourLose;
        }
        else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
    }


    public void PlayButtonPressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies = 5 + waveNumber * 2;
                break;

            default:
                waveNumber = 1;
                totalEnemies = 5;
                TotalEscaped = 0;
                TotalMoney = 10;
                TotalHearts = 5;
                enemiesToSpawn = 0;
                TowerManager.Instance.DestroyAllTower();
                TowerManager.Instance.RenameTagBuildSite();
                totalMoneyLabel.text = TotalMoney.ToString();
                totalHeartsLabel.text = TotalHearts.ToString();
                break;
        }

        IsGameStarted = true;


        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        spawnedEnemiesCount = 0;
        totalEscapedLabel.text = "Escaped " + TotalEscaped + "/ 10";
        currentWave.text = "Wave " + waveNumber;

        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }


    public void ShowMenu()
    {

        switch (currentState)
        {
            case gameStatus.gameover:
                playBtnLabel.text = "PLAY AGAIN!";

                break;

            case gameStatus.next:
                playBtnLabel.text = "NEXT WAVE";

                break;

            case gameStatus.play:
                playBtnLabel.text = "PLAY GAME";

                break;

            case gameStatus.win:
                playBtnLabel.text = "YOUR WIN!";

                break;

            case gameStatus.yourLose: // новый статус
                playBtnLabel.text = "YOUR LOSE";

                break;
        }
        playBtn.gameObject.SetActive(true);
    }
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDrag();
            TowerManager.Instance.towerBtnPressed = null;

        }
    }
}