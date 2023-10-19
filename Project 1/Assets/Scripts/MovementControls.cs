using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour
{
    public PlayerControls playerControls;
    private InputAction move;
    private InputAction fire;

    [SerializeField]
    float speed = 8.0f;
    Vector3 objectPosition;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    //Boundaries
    Vector2 screenBounds;
    Vector3 viewPos;
    float objectWidth;
    float objectHeight;

    [SerializeField]
    BulletManager bulletManager;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        direction = move.ReadValue<Vector2>();
        velocity = speed * Time.deltaTime * direction;
        objectPosition += velocity;
        //transform.position = objectPosition;

        if (objectPosition.x < -9) //Screen stopping
        {
            objectPosition.x = -9;
        }
        if (objectPosition.y < -4.3f)
        {
            objectPosition.y = -4.3f;
        }
        if (objectPosition.x > 9)
        {
            objectPosition.x = 9;
        }
        if (objectPosition.y > 4.3f)
        {
            objectPosition.y = 4.3f;
        }

        transform.position = objectPosition;

        //viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);

        //transform.position = viewPos; 
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (direction * 2));
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        this.SetDirection(context.ReadValue<Vector2>());
    }

    public void Fire(InputAction.CallbackContext context)
    {
        //shooty stuff goes here
        if (context.performed)
        {
            bulletManager.SpawnNewBullet(transform.position, Vector3.right, true, false);
        }
    }

   //public bool AABBCheckPlayer(GameObject bullet)
   //{
   //    SpriteInfo bulletBounds = bullet.GetComponent<SpriteInfo>();
   //    SpriteInfo playerBounds = gameObject.GetComponent<SpriteInfo>();
   //
   //    if (playerBounds.RectMin.x < bulletBounds.RectMax.x &&
   //        playerBounds.RectMax.x > bulletBounds.RectMin.x &&
   //        playerBounds.RectMax.y > bulletBounds.RectMin.y &&
   //        playerBounds.RectMin.y < bulletBounds.RectMax.y)
   //    {
   //            return true;
   //    }
   //
   //    return false;
   //}
}
