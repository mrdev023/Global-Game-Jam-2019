using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogres : MonoBehaviour
{
    public GameObject player;
    public GameObject fire;
    private float SPEED = 1;

    private Direction currentDirection = Direction.DOWN;
    private Animator animator;
    private PlayerScript playerScript;
    private Vector2 currentDestination;
    private bool isAggro = false;
    private bool fireSpotted = false;
    private readonly int DISTANCE_TO_SPOT = 15;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAggro();
        Vector2 direction = GetDirectionFromDestination(GetDestination());
        if (direction.y > 0.1f && direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OgresWalkLeft", 0);
            currentDirection = Direction.UP_LEFT;
        }
        else if (direction.y > 0.1f && direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OgresWalkRight", 0);
            currentDirection = Direction.UP_RIGHT;
        }
        else if (direction.y < -0.1f && direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OgresWalkLeft", 0);
            currentDirection = Direction.DOWN_LEFT;
        }
        else if (direction.y < -0.1f && direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OgresWalkRight", 0);
            currentDirection = Direction.DOWN_RIGHT;
        }
        else if (direction.y > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("OgresWalkUp", 0);
            currentDirection = Direction.UP;
        }
        else if (direction.y < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("OgresWalkDown", 0);
            currentDirection = Direction.DOWN;
        }
        else if (direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("OgresWalkLeft", 0);
            currentDirection = Direction.LEFT;
        }
        else if (direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("OgresWalkRight", 0);
            currentDirection = Direction.RIGHT;
        }
        else
        {
            if (currentDirection == Direction.DOWN)
            {
                animator.Play("OgresStopDown", 0);
            }
            else if (currentDirection == Direction.UP)
            {
                animator.Play("OgresStopUp", 0);
            }
            else if (currentDirection == Direction.LEFT || currentDirection == Direction.UP_LEFT || currentDirection == Direction.DOWN_LEFT)
            {
                animator.Play("OgresStopLeft", 0);
            }
            else if (currentDirection == Direction.RIGHT || currentDirection == Direction.UP_RIGHT || currentDirection == Direction.DOWN_RIGHT)
            {
                animator.Play("OgresStopRight", 0);
            }
            else
            {
                animator.Play("OgresStopDown", 0);
            }
        }
    }

    void CheckAggro()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < DISTANCE_TO_SPOT)
        {
            isAggro = true;
        }

        if (Vector2.Distance(this.transform.position, fire.transform.position) < DISTANCE_TO_SPOT)
        {
            fireSpotted = true;
        }
    }

    Vector2 GetDestination()
    {
        if (!fireSpotted)
        {
            if (!isAggro)
            {
                if (Vector2.Distance(this.transform.position, currentDestination) < 4)
                {
                    currentDestination = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
                }
                return currentDestination;
            }
            else
            {
                if (Vector2.Distance(this.transform.position, player.transform.position) > DISTANCE_TO_SPOT * 5)
                {
                    isAggro = false;
                }
                return player.transform.position;
            }
        }
        else
        {
            if (Vector2.Distance(this.transform.position, fire.transform.position) < 4)
            {
                fire.GetComponent<FireScript>().DecrementIntensity(100);
                Destroy(this.gameObject);
            }
            return fire.transform.position;
        }
    }

    Vector2 GetDirectionFromDestination(Vector2 destination)
    {
        return new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
    }
}
