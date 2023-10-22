using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject enemyBullet;

    [SerializeField]
    GameObject enemyRadioactiveBullet;

    [SerializeField]
    GameObject player;

    [SerializeField]
    MovementControls playerMovement;

    [SerializeField]
    EnemyManager manager;


    List<GameObject> bulletList = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] != null)
            {
                bulletList[i].transform.position += 7.5f * Time.deltaTime * bulletList[i].GetComponent<Bullet>().ShootDir;

                if (bulletList[i].transform.position.x <= -13 || bulletList[i].transform.position.x >= 13)
                {
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
                else if (bulletList[i].GetComponent<Bullet>().FiredFromPlayer == true && manager.AABBcheck(bulletList[i]))
                {
                    //Happens when player bullets connect to an enemy
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
                else if (bulletList[i].GetComponent<Bullet>().FiredFromPlayer == false &&
                    CollisionManager.AABBCheck(
                        bulletList[i].GetComponent<SpriteInfo>(),
                        player.GetComponent<SpriteInfo>()
                        ))
                {
                    //Happens when enemy bullets connect to a player

                    if (bulletList[i].GetComponent<Bullet>().FiredFromRadioactive)
                    {
                        playerMovement.MovementNerf = 0.05f;
                        manager.SpawnWarning(player.transform.position.x, player.transform.position.y);
                    }
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void SpawnNewBullet(Vector3 position, Vector3 shootDir, float bulletSpeed, bool firedFromPlayer, bool firedFromRadioactive)
    {
        //Debug.Log("Fire!");

        GameObject temp = null;

        if (firedFromPlayer)
        {
            temp = Instantiate(bullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
        }
        else if (firedFromRadioactive)
        {
            temp = Instantiate(enemyRadioactiveBullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
        }
        else
        {
            temp = Instantiate(enemyBullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
        }
       
        temp.GetComponent<Bullet>().Setup(shootDir, firedFromPlayer, firedFromRadioactive, bulletSpeed);
        bulletList.Add(temp);        
        
        //Transform bulletTransform = Instantiate(bullet, position, Quaternion.identity;
        //bulletTransform.GetComponent<Bullet>().Setup(Vector3.right);
    }
}
