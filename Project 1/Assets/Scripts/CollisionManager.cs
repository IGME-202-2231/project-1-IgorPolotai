using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static bool AABBCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        return spriteB.RectMin.x < spriteA.RectMax.x &&
                spriteB.RectMax.x > spriteA.RectMin.x &&
                spriteB.RectMax.y > spriteA.RectMin.y &&
                spriteB.RectMin.y < spriteA.RectMax.y;
    }
}
