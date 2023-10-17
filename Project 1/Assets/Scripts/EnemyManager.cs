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
    BulletManager bulletManager;

    static List<GameObject> cowList = new List<GameObject>();
    static List<Vector3> movementList = new List<Vector3>();
    static List<int> cowHealth = new List<int>(); 
    static List<float> movementSpeed = new List<float>();

    // firing speed
    static List<float> fireRate = new List<float>();
    static List<float> currentFire = new List<float> ();

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
                movementList.RemoveAt(i);
                cowHealth.RemoveAt(i);
                fireRate.RemoveAt(i);
                currentFire.RemoveAt(i);
                movementSpeed.RemoveAt(i);
                i--;
            }
            else if (currentFire[i] >= fireRate[i]) // Enemy fire logic
            {
                bulletManager.SpawnNewBullet(cowList[i].transform.position, Vector3.left, false);
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

        int selectCow = Random.Range(0, 10);

        if (selectCow < 2) //Spawns a commando cow
        {
            cowList.Add(Instantiate(cowcommando, new Vector3(posX, posY, 0), Quaternion.identity));
            movementList.Add(new Vector3(Random.Range(-3.0f, -1.0f), Random.Range(-0.5f, 0.5f), 0));
            cowHealth.Add(5);
            fireRate.Add(Random.Range(0.5f, 2.0f));
            currentFire.Add(0.0f);
            movementSpeed.Add(3.0f);
        }
        else if (selectCow < 4) //Spawns a scout cow
        {
            cowList.Add(Instantiate(cowscout, new Vector3(posX, posY, 0), Quaternion.identity));
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(1);
            fireRate.Add(1.0f);
            currentFire.Add(0.0f);
            movementSpeed.Add(0.2f);
        }
        else //Spawns a normal cow
        {
            cowList.Add(Instantiate(cow, new Vector3(posX, posY, 0), Quaternion.identity));
            movementList.Add(new Vector3(Random.Range(-5.0f, -3.0f), Random.Range(-1.0f, 1.0f), 0));
            cowHealth.Add(3);
            fireRate.Add(Random.Range(3.0f, 5.0f));
            currentFire.Add(2.0f);
            movementSpeed.Add(1f);
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
                    Destroy(cowList[i]);
                    cowList.RemoveAt(i);
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
}
