using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject cow;

    [SerializeField]
    GameObject cowcommando;

    [SerializeField]
    GameObject cowscout;

    [SerializeField]
    GameObject cowsoldier;

    [SerializeField]
    GameObject cowarmor;

    [SerializeField]
    GameObject cowradioactive;

    [SerializeField]
    GameObject cowmedic;

    [SerializeField]
    GameObject cowboss;

    [SerializeField]
    GameObject player;

    [SerializeField]
    BulletManager bulletManager;

    [SerializeField]
    GameObject burger;

    [SerializeField]
    GameObject warning;

    [SerializeField]
    GameObject shield;

    List<GameObject> cowList = new List<GameObject>();
    List<string> cowNames = new List<string>();
    List<Vector3> movementList = new List<Vector3>();
    List<int> cowHealth = new List<int>();
    List<float> movementSpeed = new List<float>();

    // firing speed
    List<float> fireRate = new List<float>();
    List<float> currentFire = new List<float>();
    List<float> bulletSpeed = new List<float>();

    List<GameObject> deathBurgers = new List<GameObject>();
    List<Vector3> burgerVectors = new List<Vector3>();
    Vector3 gravity = new Vector3(0, -5, 0);
    float rotation = 0.0f;

    List<GameObject> shieldList = new List<GameObject>();
    List<Vector3> shieldVectors = new List<Vector3>();
    int medicCount = 0;

    List<GameObject> warningList = new List<GameObject>();

    List<float> spawnRates = new List<float>();
    List<bool> spawnCheck = new List<bool> { true, true, true, true, true };
    float gameTimer = 300.0f;
    int currentWave = 1;
    bool bossEntrance = true;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDifficulty(currentWave);
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer -= Time.deltaTime;

        CheckTimer(gameTimer);

        //for (int i = 0; i < shieldList.Count; i++)
        //{

        //    if (shieldList[i].GetComponent<Shield>().Update())
        //    {
        //        shieldList.RemoveAt(i);
        //        i--;
        //    }


        //shieldList[i].transform.position += shieldVectors[i] * Time.deltaTime;


        //if (shieldList[i].transform.position.x < -13 ||
        //    shieldList[i].transform.position.y < -6.5f ||
        //    shieldList[i].transform.position.y > 6.5)
        //{
        //    Destroy(shieldList[i]);
        //    shieldList.RemoveAt(i);
        //    shieldVectors.RemoveAt(i);
        //}
        // }

        for (int i = 0; i < warningList.Count; i++)
        {
            warningList[i].transform.position += new Vector3(0, 5, 0) * Time.deltaTime;

            if (warningList[i].transform.position.y > 10)
            {
                Destroy(warningList[i]);
                warningList.RemoveAt(i);
            }
        }

        for (int i = 0; i < deathBurgers.Count; i++)
        {
            deathBurgers[i].transform.position += burgerVectors[i] * Time.deltaTime;
            rotation += (Time.deltaTime * 20);
            deathBurgers[i].transform.eulerAngles = Vector3.forward * rotation;
            burgerVectors[i] += (gravity * Time.deltaTime);

            if (deathBurgers[i].transform.position.y < -10)
            {
                Destroy(deathBurgers[i]);
                deathBurgers.RemoveAt(i);
                burgerVectors.RemoveAt(i);
            }
        }

        for (int i = 0; i < cowList.Count; i++)
        {
            //cowList[i].transform.position += Vector3.left * 3.0f * Time.deltaTime;
            cowList[i].transform.position += movementList[i] * movementSpeed[i] * Time.deltaTime;

            //boss logic

            if (cowNames[i] == "boss") 
            {
                if (bossEntrance && cowList[i].transform.position.x < 10)
                {
                    movementList[i] = new Vector3(0, 3, 0);
                    bossEntrance = false;
                }
                if (cowList[i].transform.position.y < -4.0f)
                {
                    movementList[i] = new Vector3(0, 3, 0);
                }
                else if (cowList[i].transform.position.y > 4.0f)
                {
                    movementList[i] = new Vector3(0, -3, 0);
                }
            }

            if (cowList[i].transform.position.x < -13 ||
                cowList[i].transform.position.y < -6.5f ||
                cowList[i].transform.position.y > 6.5)
            {
                DestroyEnemy(i, false);
                i--;
            }
            else if (cowNames[i] != "boss" && CollisionManager.AABBCheck(cowList[i].GetComponent<SpriteInfo>(), player.GetComponent<SpriteInfo>()))
            {
                DestroyEnemy(i, true);
                i--;
            }
            else if (currentFire[i] >= fireRate[i]) // Enemy fire logic
            {
                if (cowNames[i] == "armor")
                {
                    Vector2 armorShootDir = new Vector2(player.transform.position.x - cowList[i].transform.position.x,
                                                        player.transform.position.y - cowList[i].transform.position.y);
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, armorShootDir.normalized, bulletSpeed[i], false, false);
                }
                else if (cowNames[i] == "radioactive")
                {
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, Vector3.left, bulletSpeed[i], false, true);
                }
                else if (cowNames[i] == "boss")
                {
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, (new Vector3(-2, Random.Range(-0.5f, 0.5f), 0)).normalized, bulletSpeed[i], false, false);
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, (new Vector3(-2, Random.Range(-0.5f, 0.5f), 0)).normalized, bulletSpeed[i], false, false);
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, (new Vector3(-2, Random.Range(-0.5f, 0.5f), 0)).normalized, bulletSpeed[i], false, false);
                }
                else
                {
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, Vector3.left, bulletSpeed[i], false, false);
                }

                currentFire[i] = 0.0f;
            }
            else
            {
                currentFire[i] += Time.deltaTime;
            }
        }
    }

    public void UpdateDifficulty(int waveNumber)
    {
        switch (waveNumber)
        {
            //cow, soldier, commando, scout, armour, radioactive
            case 1:
                //cow: 60%
                //sol: 30%
                //cmd: 10%
                //sct: 0%
                //arm: 0%
                //rad: 0%

                spawnRates = new List<float> { 0.6f, 0.9f, 1.0f, 0.0f, 0.0f };
                InvokeRepeating("SpawnNewEnemy", 0, 2.0f);
                break;
            case 2:
                //cow: 50%
                //sol: 25%
                //cmd: 15%
                //sct: 10%
                //arm: 0%
                //rad: 0%

                spawnRates = new List<float> { 0.5f, 0.75f, 0.9f, 1.0f, 0.0f };
                InvokeRepeating("SpawnNewEnemy", 0, 4.0f);
                break;
            case 3:
                //cow: 40%
                //sol: 20%
                //cmd: 20%
                //sct: 10%
                //arm: 10%
                //rad: 0%

                spawnRates = new List<float> { 0.4f, 0.6f, 0.8f, 0.9f, 1.0f };
                InvokeRepeating("SpawnNewEnemy", 0, 8.0f);
                break;
            case 4:
                //cow: 30%
                //sol: 20%
                //cmd: 20%
                //sct: 10%
                //arm: 10%
                //rad: 10%

                spawnRates = new List<float> { 0.3f, 0.5f, 0.7f, 0.8f, 0.9f };
                InvokeRepeating("SpawnNewEnemy", 0, 6.0f);
                break;
            case 5:
                //cow: 10%
                //sol: 15%
                //cmd: 20%
                //sct: 20%
                //arm: 15%
                //rad: 20%

                spawnRates = new List<float> { 0.1f, 0.25f, 0.45f, 0.65f, 0.8f };
                InvokeRepeating("SpawnNewEnemy", 0, 12.00f);
                break;
            case 6:
                //cow: 5%
                //sol: 20%
                //cmd: 40%
                //sct: 15%
                //arm: 10%
                //rad: 10%

                //boss: 100%

                spawnRates = new List<float> { 0.05f, 0.25f, 0.65f, 0.8f, 0.9f };
                InvokeRepeating("SpawnNewEnemy", 0, 25.00f);
                SpawnBoss();
                break;
        }
    }

    public void CheckTimer(float currentTimer)
    {
        if (spawnCheck[0] && currentTimer <= 240.0f) //starts wave 2
        {
            spawnCheck[0] = false;
            currentWave++;
            UpdateDifficulty(currentWave);
        }
        else if (spawnCheck[1] && currentTimer <= 180.0f) //starts wave 3
        {
            spawnCheck[1] = false;
            currentWave++;
            UpdateDifficulty(currentWave);
        }
        else if (spawnCheck[2] && currentTimer <= 120.0f) //starts wave 4
        {
            spawnCheck[2] = false;
            currentWave++;
            UpdateDifficulty(currentWave);
        }
        else if (spawnCheck[3] && currentTimer <= 60.0f) //starts wave 5
        {
            spawnCheck[3] = false;
            currentWave++;
            UpdateDifficulty(currentWave);
        }
        else if (spawnCheck[4] && currentTimer <= 0.0f)
        {
            spawnCheck[4] = false;
            currentWave++;
            UpdateDifficulty(currentWave);
        }
    }

    public void SpawnBoss()
    {
        float posX = Random.Range(13f, 15f);
        float posY = Random.Range(-3f, 3f);

        cowList.Add(Instantiate(cowboss, new Vector3(posX, posY, 0), Quaternion.identity)); 
        cowNames.Add("boss");
        movementList.Add(new Vector3(-3, 0, 0)); 
        cowHealth.Add(100);
        fireRate.Add(1.25f); 
        currentFire.Add(-2.0f); 
        movementSpeed.Add(1f);
        bulletSpeed.Add(12.0f); 
    }

    public void SpawnNewEnemy()
    {
        float posX = Random.Range(12f, 14f);
        float posY = Random.Range(-4f, 4f);

        float selectCow = Random.value; //Old values: 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f

        if (selectCow <= spawnRates[0])//Spawns a normal cow
        {
            cowList.Add(Instantiate(cow, new Vector3(posX, posY, 0), Quaternion.identity)); //creates cow, chooses random location offscreen
            cowNames.Add("cow");
            movementList.Add(new Vector3(Random.Range(-3.0f, -2.0f), Random.Range(-1.0f, 1.0f), 0)); //chooses vector of movement. Increase y to give bigger angles
            cowHealth.Add(1); //Each bullet does two damage
            fireRate.Add(Random.Range(3.0f, 5.0f)); //How frequently each cow shoots
            currentFire.Add(2.0f); //How quickly the cow starts to shoot
            movementSpeed.Add(1f); //The multiplier for speed
            bulletSpeed.Add(7.5f); //The speed of the enemy's bullets
        }
        else if (selectCow < spawnRates[1]) //Spawns a soldier cow
        {
            cowList.Add(Instantiate(cowsoldier, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("soldier");
            movementList.Add(new Vector3(Random.Range(-3.25f, -2.5f), Random.Range(-0.75f, 0.75f), 0));
            cowHealth.Add(3);
            fireRate.Add(Random.Range(1f, 4.0f));
            currentFire.Add(2.0f);
            movementSpeed.Add(2f);
            bulletSpeed.Add(8.0f);
        }
        else if (selectCow < spawnRates[2]) //Spawns a commando cow
        {
            cowList.Add(Instantiate(cowcommando, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("commando");
            movementList.Add(new Vector3(Random.Range(-3.5f, -3.0f), Random.Range(-0.5f, 0.5f), 0));
            cowHealth.Add(5);
            fireRate.Add(Random.Range(0.5f, 2.0f));
            currentFire.Add(2.0f);
            movementSpeed.Add(3.0f);
            bulletSpeed.Add(8.5f);
        }
        else if (selectCow < spawnRates[3]) //Spawns a scout cow
        {
            cowList.Add(Instantiate(cowscout, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("scout");
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(1);
            fireRate.Add(1.0f);
            currentFire.Add(0.0f);
            movementSpeed.Add(0.2f);
            bulletSpeed.Add(4.0f);
        }
        else if (selectCow < spawnRates[4]) //Spawns an armor cow
        {
            cowList.Add(Instantiate(cowarmor, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("armor");
            movementList.Add(new Vector3(-2.0f, Random.Range(-0.25f, 0.25f), 0));
            cowHealth.Add(13);
            fireRate.Add(Random.Range(1.0f, 3.0f));
            currentFire.Add(0.75f);
            movementSpeed.Add(1.0f);
            bulletSpeed.Add(0.1f);
        }
        else //Spawns a radioactive cow
        {
            cowList.Add(Instantiate(cowradioactive, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("radioactive");
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(5);
            fireRate.Add(4.0f);
            currentFire.Add(2.5f);
            movementSpeed.Add(0.75f);
            bulletSpeed.Add(5.0f);
        }
        //  else //Spawns a medic cow
        //{
        //    if (medicCount <= 2)
        //    {
        //        cowList.Add(Instantiate(cowmedic, new Vector3(posX, posY, 0), Quaternion.identity));
        //        cowNames.Add("medic");
        //        movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-0.5f, 0.5f), 0));
        //        cowHealth.Add(5);
        //        fireRate.Add(100.0f); //Doesn't fire
        //        currentFire.Add(0.0f);
        //        movementSpeed.Add(0.1f);
        //        bulletSpeed.Add(1.5f);
        //        medicCount++;
        //
        //        SpawnShield();
        //    }
        //    else //Spawns a normal cow if there are already three medic cows active
        //    {
        //        cowList.Add(Instantiate(cow, new Vector3(posX, posY, 0), Quaternion.identity));
        //        cowNames.Add("cow");
        //        movementList.Add(new Vector3(Random.Range(-3.0f, -2.0f), Random.Range(-1.0f, 1.0f), 0));
        //        cowHealth.Add(1);
        //        fireRate.Add(Random.Range(3.0f, 5.0f));
        //        currentFire.Add(2.0f);
        //        movementSpeed.Add(1f);
        //        bulletSpeed.Add(7.5f);
        //    }
        //}
    }

    public bool AABBcheck(GameObject bullet)
    {
        SpriteInfo bulletBounds = bullet.GetComponent<SpriteInfo>();

        for (int i = 0; i < cowList.Count; i++)
        {
            SpriteInfo cowBounds = cowList[i].GetComponent<SpriteInfo>();

            if (CollisionManager.AABBCheck(bulletBounds, cowBounds))
            {
                if (cowHealth[i] <= 0)
                {
                    DestroyEnemy(i, true);
                    return true;
                }
                else
                {
                    cowHealth[i]--;
                    return true;
                }
            }
        }

        return false;
    }

    public void SpawnHamburger(float x, float y)
    {
        deathBurgers.Add(Instantiate(burger, new Vector3(x, y, 0), Quaternion.identity));
        burgerVectors.Add(new Vector3(0, 5, 0));
    }

    public void SpawnWarning(float x, float y)
    {
        warningList.Add(Instantiate(warning, new Vector3(x, y, 0), Quaternion.identity));
    }

    public void SpawnShield()
    {
        int randomCow = Random.Range(0, cowList.Count);
        int loopEnder = 0;

        while ((cowNames[randomCow] == "medic" || cowList[randomCow].GetComponent<SpriteInfo>().CurrentShield != null) && loopEnder < 100)
        {
            randomCow = Random.Range(0, cowList.Count);

            if (medicCount * 2 >= cowList.Count)
            {
                SpawnNewEnemy();
            }

            loopEnder++;
        }

        if (loopEnder >= 100)
        {
            for (int i = 0; i < cowList.Count; i++)
            {
                if (cowNames[i] == "medic")
                {
                    medicCount--;

                    Destroy(cowList[i]);
                    cowList.RemoveAt(i);
                    cowNames.RemoveAt(i);
                    movementList.RemoveAt(i);
                    cowHealth.RemoveAt(i);
                    fireRate.RemoveAt(i);
                    currentFire.RemoveAt(i);
                    movementSpeed.RemoveAt(i);
                    bulletSpeed.RemoveAt(i);

                    i = cowList.Count;
                }
            }
        }
        else
        {
            //Shield temp = new Shield(cowList[randomCow], movementList[randomCow] * movementSpeed[randomCow])
            cowList[randomCow].GetComponent<SpriteInfo>().CurrentShield.Enemy = cowList[randomCow];
            cowList[randomCow].GetComponent<SpriteInfo>().CurrentShield.EnemySpeed = movementList[randomCow] * movementSpeed[randomCow];
            cowList[randomCow].GetComponent<SpriteInfo>().HasAShield = true;
            cowHealth[randomCow] = 500;
            shieldList.Add(Instantiate(shield, new Vector3(cowList[randomCow].transform.position.x, cowList[randomCow].transform.position.y, -1), Quaternion.identity));
            //shieldVectors.Add(movementList[randomCow] * movementSpeed[randomCow]);
        }

        // Check to see that health goes down if the enemy loses their shield

        //int count = 0;

        //foreach (int health in cowHealth)
        //{
        //    if (health > 250)
        //    {
        //        count++;
        //    }
        //}

        //if (count > medicCount)
        //{
        //    count = 0;

        //    for (int i = 0; i < shieldList.Count; i++)
        //    {
        //        Destroy(shieldList[i]);
        //        shieldList.RemoveAt(i);
        //shieldVectors.RemoveAt(i);
        //        i--;
        //   }



        //   for (int i = 0; i < cowList.Count; i++)
        //   {
        //       if (count <= medicCount &&
        //           cowList[i].GetComponent<SpriteInfo>().HasAShield &&
        //           cowHealth[i] >= 250)
        //       {
        //           cowList[i].GetComponent<SpriteInfo>().HasAShield = true;
        //           cowHealth[i] = 500;
        //           shieldList.Add(Instantiate(shield, new Vector3(cowList[i].transform.position.x, cowList[i].transform.position.y, -1), Quaternion.identity));
        //           shieldVectors.Add(movementList[i] * movementSpeed[i]);
        //
        //           count++;
        //       }
        //       else if (cowHealth[i] >= 250)
        //       {
        //           cowList[i].GetComponent<SpriteInfo>().HasAShield = false;
        //           cowHealth[i] -= 500;
        //
        //           if (cowHealth[i] <= 0)
        //           {
        //               cowHealth[i] = 1;
        //           }
        //       }
        //   }
    }

    public void DestroyEnemy(int i, bool spawnBurgers)
    {
        if (spawnBurgers)
        {
            SpawnHamburger(cowList[i].transform.position.x, cowList[i].transform.position.y);
        }

        if (cowList[i] != null && cowNames[i] == "medic")
        {
            for (int j = 0; j < cowList.Count; j++)
            {
                if (cowList[j].GetComponent<SpriteInfo>().CurrentShield != null && shieldList.Count >= 1)
                {
                    cowHealth[j] -= 500;

                    if (cowHealth[j] <= 0)
                    {
                        cowHealth[j] = 1;
                    }

                    for (int k = 0; k < shieldList.Count; k++)
                    {
                        if (cowList[j].GetComponent<SpriteInfo>().CurrentShield == shieldList[k].GetComponent<SpriteInfo>().CurrentShield)
                        {
                            shieldList[k].GetComponent<SpriteInfo>().CurrentShield.Enemy = null;
                            k = shieldList.Count;
                        }
                    }

                    //Destroy(shieldList[0]);
                    //shieldList.RemoveAt(0);
                    //shieldVectors.RemoveAt(0);

                    medicCount--;
                    j = cowList.Count;
                }
            }
        }

        if (medicCount > 0 && cowList[i].GetComponent<SpriteInfo>().CurrentShield != null)
        {
            //  GameObject temp = null;
            //  Vector2 min = new Vector2(1000, 1000);
            //
            //  foreach (GameObject shield in shieldList)
            //  {
            //      Vector2 current = new Vector2(shield.transform.position.x - cowList[i].transform.position.x,
            //                                    shield.transform.position.y - cowList[i].transform.position.y);
            //      if (min.magnitude > current.magnitude)
            //      {
            //          temp = shield;
            //          min = current;
            //      }
            //  }
            //
            //  int index = shieldList.IndexOf(temp);
            //
            //  Destroy(shieldList[index]);
            //  shieldList.RemoveAt(index);
            //  shieldVectors.RemoveAt(index);

            for (int k = 0; k < shieldList.Count; k++)
            {
                if (cowList[i].GetComponent<SpriteInfo>().CurrentShield == shieldList[k].GetComponent<SpriteInfo>().CurrentShield)
                {
                    shieldList[k].GetComponent<SpriteInfo>().CurrentShield.Enemy = null;
                    k = shieldList.Count;
                }
            }

            SpawnShield();

        }

        Destroy(cowList[i]);
        cowList.RemoveAt(i);
        cowNames.RemoveAt(i);
        movementList.RemoveAt(i);
        cowHealth.RemoveAt(i);
        fireRate.RemoveAt(i);
        currentFire.RemoveAt(i);
        movementSpeed.RemoveAt(i);
        bulletSpeed.RemoveAt(i);
    }

}