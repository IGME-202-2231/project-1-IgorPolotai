using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private bool firedFromPlayer;

    public Vector3 ShootDir
    {
        get { return shootDir; }
    } 

    public bool FiredFromPlayer
    {
        get { return firedFromPlayer; }
    }

    public void Setup(Vector3 shootDir, bool fired)
    {
        this.shootDir = shootDir;
        this.firedFromPlayer = fired;
        //Destroy(gameObject, 5.0f);
    }
}
