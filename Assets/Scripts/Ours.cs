using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ours : MonoBehaviour
{
    public GameObject player;
    public GameObject tent;
    public float SPEED = 1;

    private Direction currentDirection = Direction.DOWN;
    private Animator animator;
    private PlayerScript playerScript;
    private Vector2 currentDestination;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GetDirectionFromDestination(GetDestination());

        if (direction.y > 0.1f && direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OursWalkLeft", 0);
            currentDirection = Direction.UP_LEFT;
        }
        else if (direction.y > 0.1f && direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OursWalkRight", 0);
            currentDirection = Direction.UP_RIGHT;
        }
        else if (direction.y < -0.1f && direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OursWalkLeft", 0);
            currentDirection = Direction.DOWN_LEFT;
        }
        else if (direction.y < -0.1f && direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("OursWalkRight", 0);
            currentDirection = Direction.DOWN_RIGHT;
        }
        else if (direction.y > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("OursWalkUp", 0);
            currentDirection = Direction.UP;
        }
        else if (direction.y < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("OursWalkDown", 0);
            currentDirection = Direction.DOWN;
        }
        else if (direction.x < -0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("OursWalkLeft", 0);
            currentDirection = Direction.LEFT;
        }
        else if (direction.x > 0.1f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("OursWalkRight", 0);
            currentDirection = Direction.RIGHT;
        }
        else
        {
            if (currentDirection == Direction.DOWN)
            {
                animator.Play("OursStopDown", 0);
            }
            else if (currentDirection == Direction.UP)
            {
                animator.Play("OursStopUp", 0);
            }
            else if (currentDirection == Direction.LEFT || currentDirection == Direction.UP_LEFT || currentDirection == Direction.DOWN_LEFT)
            {
                animator.Play("OursStopLeft", 0);
            }
            else if (currentDirection == Direction.RIGHT || currentDirection == Direction.UP_RIGHT || currentDirection == Direction.DOWN_RIGHT)
            {
                animator.Play("OursStopRight", 0);
            }
            else
            {
                animator.Play("OursStopDown", 0);
            }
        }
    }

    Vector2 GetDestination()
    {
        if (playerScript.GetNumberOfWood() > 0 && !tent.GetComponent<Tent>().playerIsSafe())
        {
            if (Vector2.Distance(this.transform.position, player.transform.position) < 4)
            {
                playerScript.BurnObjects(1000);
                this.GetComponent<AudioSource>().Play();
                player.GetComponent<FearLevel>().IncrementLevel(10);
                playerScript.EmitHurtParticle();
            }
            return player.transform.position;
        }
        if (Vector2.Distance(this.transform.position, currentDestination) < 4)
        {
            currentDestination = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
        }
        return currentDestination;
    }

    Vector2 GetDirectionFromDestination (Vector2 destination)
    {
        return new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
    }
}
