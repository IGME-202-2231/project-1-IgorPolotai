using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private bool firedFromPlayer;
    private bool firedFromRadioactive;
    private float bulletSpeed;

    public Vector3 ShootDir
    {
        get { return shootDir; }
    } 

    public bool FiredFromPlayer
    {
        get { return firedFromPlayer; }
    }

    public bool FiredFromRadioactive
    {
        get { return firedFromRadioactive;  }
    }

    public float BulletSpeed 
    {
        get { return bulletSpeed; }
    }

    public void Setup(Vector3 shootDir, bool fired, bool radioactive, float bulletSpeed)
    {
        this.shootDir = shootDir;
        this.firedFromPlayer = fired;
        this.firedFromRadioactive = radioactive;
        this.bulletSpeed = bulletSpeed;
        //Destroy(gameObject, 5.0f);
    }
}
