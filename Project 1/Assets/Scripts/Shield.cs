using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject enemy;
    Vector3 enemySpeed;

    public GameObject Enemy
    {
        get { return enemy;}
        set { enemy = value; }  
    }

    public Vector3 EnemySpeed
    {
        get { return enemySpeed; }
        set { enemySpeed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool Update()
    {
        if (enemy == null)
        {
            Destroy(this);
            return true;
        }
        else
        {
            this.transform.position += enemySpeed * Time.deltaTime;
            return false;
        }
    }

    public Shield(GameObject enemyInput, Vector3 enemySpeedInput)
    {
        enemy = enemyInput;
        enemySpeed = enemySpeedInput;
    }
}
