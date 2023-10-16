using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

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
                bulletList[i].transform.position += 5.0f * Time.deltaTime * bulletList[i].GetComponent<Bullet>().ShootDir;

                if (Enemy.AABBcheck(bulletList[i]))
                {
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public void SpawnNewBullet(Vector3 position, Vector3 shootDir)
    {
        Debug.Log("Fire!");
        GameObject temp = Instantiate(bullet, new Vector3(position.x, position.y, 0), Quaternion.identity);
        temp.GetComponent<Bullet>().Setup(shootDir);
        bulletList.Add(temp);

        //Transform bulletTransform = Instantiate(bullet, position, Quaternion.identity;
        //bulletTransform.GetComponent<Bullet>().Setup(Vector3.right);
    }
}
