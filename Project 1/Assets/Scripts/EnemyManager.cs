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
    GameObject player;

    [SerializeField]
    BulletManager bulletManager;

    [SerializeField]
    GameObject burger;

    static List<GameObject> cowList = new List<GameObject>();
    static List<string> cowNames = new List<string>();
    static List<Vector3> movementList = new List<Vector3>();
    static List<int> cowHealth = new List<int>(); 
    static List<float> movementSpeed = new List<float>();

    // firing speed
    static List<float> fireRate = new List<float>();
    static List<float> currentFire = new List<float> ();

    static List<GameObject> deathBurgers = new List<GameObject> ();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cowList.Count; i++)
        {
            //cowList[i].transform.position += Vector3.left * 3.0f * Time.deltaTime;
            cowList[i].transform.position += movementList[i] * movementSpeed[i] * Time.deltaTime;

            if (cowList[i].transform.position.x < -13)
            {
                Destroy(cowList[i]);
                cowList.RemoveAt(i);
                cowNames.RemoveAt(i);
                movementList.RemoveAt(i);
                cowHealth.RemoveAt(i);
                fireRate.RemoveAt(i);
                currentFire.RemoveAt(i);
                movementSpeed.RemoveAt(i);
                i--;
            }
            else if (currentFire[i] >= fireRate[i]) // Enemy fire logic
            {
                if (cowNames[i] == "armor")
                {
                    Vector2 armorShootDir = new Vector2(player.transform.position.x - cowList[i].transform.position.x,
                                                        player.transform.position.y - cowList[i].transform.position.y);
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, armorShootDir, false, false);
                }
                else if (cowNames[i] == "radioactive")
                {
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, Vector3.left, false, true);
                }
                else
                {
                    bulletManager.SpawnNewBullet(cowList[i].transform.position, Vector3.left, false, false);
                }
                
                currentFire[i] = 0.0f;
            }
            else
            {
                currentFire[i] += Time.deltaTime;
            }
        }
    }

    public void SpawnNewEnemy()
    {
        float posX = Random.Range(12f, 14f);
        float posY = Random.Range(-4f, 4f);

        float selectCow = Random.value;

        if (selectCow <= 0.4f) //Spawns a normal cow
        {
            cowList.Add(Instantiate(cow, new Vector3(posX, posY, 0), Quaternion.identity)); //creates cow, chooses random location offscreen
            cowNames.Add("cow");
            movementList.Add(new Vector3(Random.Range(-3.0f, -2.0f), Random.Range(-1.0f, 1.0f), 0)); //chooses vector of movement. Increase y to give bigger angles
            cowHealth.Add(1); //Each bullet does two damage
            fireRate.Add(Random.Range(3.0f, 5.0f)); //How frequently each cow shoots
            currentFire.Add(2.0f); //How quickly the cow starts to shoot
            movementSpeed.Add(1f); //The multiplier for speed
        }
        else if (selectCow < 0.5f) //Spawns a soldier cow
        {
            cowList.Add(Instantiate(cowsoldier, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("soldier");
            movementList.Add(new Vector3(Random.Range(-3.5f, -2.5f), Random.Range(-0.75f, 0.75f), 0));
            cowHealth.Add(3);
            fireRate.Add(Random.Range(1f, 4.0f));
            currentFire.Add(2.0f);
            movementSpeed.Add(2f);
        }
        else if (selectCow < 0.6f) //Spawns a commando cow
        {
            cowList.Add(Instantiate(cowcommando, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("commando");
            movementList.Add(new Vector3(Random.Range(-4.0f, -3.0f), Random.Range(-0.5f, 0.5f), 0));
            cowHealth.Add(5);
            fireRate.Add(Random.Range(0.5f, 2.0f));
            currentFire.Add(2.0f);
            movementSpeed.Add(3.0f);
        }
        else if (selectCow < 0.7f) //Spawns a scout cow
        {
            cowList.Add(Instantiate(cowscout, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("scout");
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(1);
            fireRate.Add(1.0f);
            currentFire.Add(0.0f);
            movementSpeed.Add(0.2f);
        }
        else if (selectCow < 0.8f) //Spawns an armor cow
        {
            cowList.Add(Instantiate(cowarmor, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("armor");
            movementList.Add(new Vector3(-2.0f, Random.Range(-0.25f, 0.25f), 0));
            cowHealth.Add(13);
            fireRate.Add(Random.Range(1.0f, 3.0f));
            currentFire.Add(0.75f);
            movementSpeed.Add(1.0f);
        }        
        else if (selectCow < 0.9f) //Spawns a radioactive cow
        {
            cowList.Add(Instantiate(cowradioactive, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("radioactive");
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(5);
            fireRate.Add(1.0f);
            currentFire.Add(0.9f);
            movementSpeed.Add(0.75f);
        }
        else //Spawns a medic cow
        {
            cowList.Add(Instantiate(cowmedic, new Vector3(posX, posY, 0), Quaternion.identity));
            cowNames.Add("medic");
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(3);
            fireRate.Add(100.0f); //Doesn't fire
            currentFire.Add(0.0f);
            movementSpeed.Add(1.5f);
        }
    }

    public static bool AABBcheck(GameObject bullet)
    {
        SpriteInfo bulletBounds = bullet.GetComponent<SpriteInfo>();

        for (int i = 0; i < cowList.Count; i++)
        {
            SpriteInfo cowBounds = cowList[i].GetComponent<SpriteInfo>();

            if (CollisionManager.AABBCheck(bulletBounds, cowBounds))
            {
                if (cowHealth[i] <= 0)
                {
                    SpawnHamburger(cowList[i].transform.position.x, cowList[i].transform.position.y);

                    Destroy(cowList[i]);
                    cowList.RemoveAt(i);
                    cowNames.RemoveAt(i);
                    movementList.RemoveAt(i);
                    cowHealth.RemoveAt(i);
                    fireRate.RemoveAt(i);
                    currentFire.RemoveAt(i);
                    movementSpeed.RemoveAt(i);
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

    public static void SpawnHamburger(float x, float y)
    {
        //deathBurgers.Add(Instantiate(burger, new Vector3(x, y, 0), Quaternion.identity));
    }
}
