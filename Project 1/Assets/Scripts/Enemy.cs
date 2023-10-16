using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject cow;

    static List<GameObject> cowList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNewEnemy", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cowList.Count; i++)
        {
            cowList[i].transform.position += Vector3.left * 3.0f * Time.deltaTime;

            if (cowList[i].transform.position.x < -13)
            {
                Destroy(cowList[i]);
                cowList.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnNewEnemy()
    {
        float posX = Random.Range(12f, 14f);
        float posY = Random.Range(-4f, 4f);

        cowList.Add(Instantiate(cow, new Vector3(posX, posY, 0), Quaternion.identity));
    }

    public static bool AABBcheck(GameObject bullet) 
    {
        SpriteInfo bulletBounds = bullet.GetComponent<SpriteInfo>();

        for (int i = 0; i < cowList.Count; i++)
        {
            SpriteInfo cowBounds = cowList[i].GetComponent<SpriteInfo>();

            if (cowBounds.RectMin.x < bulletBounds.RectMax.x &&
                cowBounds.RectMax.x > bulletBounds.RectMin.x &&
                cowBounds.RectMax.y > bulletBounds.RectMin.y &&
                cowBounds.RectMin.y < bulletBounds.RectMax.y)
            {
                Destroy(cowList[i]);
                cowList.RemoveAt(i);
                return true;
            }
        }

        return false;
    }
}
