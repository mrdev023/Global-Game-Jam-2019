using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int SPEED = 10;
    public GameObject interactMessage;
    public Text text;
    public ParticleSystem hurt;

    public GameObject wall;

    private Direction currentDirection = Direction.DOWN;
    private Animator animator;
    private List<CombustibleItem> selectedCombustibleItem = new List<CombustibleItem>();
    public int score = 0;
    public float previousTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Global.player = this;
        animator = GetComponent<Animator>();
        previousTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup - previousTime > 1.0f)
        {
            score++;
            previousTime = Time.realtimeSinceStartup;
        }
        text.text = "Bois : " + selectedCombustibleItem.Count;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Manette.IsUp()) && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Manette.IsLeft()))
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkLeft", 0);
            currentDirection = Direction.UP_LEFT;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Manette.IsUp()) && (Input.GetKey(KeyCode.D) || Manette.IsRight()))
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkRight", 0);
            currentDirection = Direction.UP_RIGHT;
        }
        else if ((Input.GetKey(KeyCode.S) || Manette.IsDown()) && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Manette.IsLeft()))
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkLeft", 0);
            currentDirection = Direction.DOWN_LEFT;
        }
        else if ((Input.GetKey(KeyCode.S) || Manette.IsDown()) && (Input.GetKey(KeyCode.D) || Manette.IsRight()))
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.y - SPEED * Time.deltaTime * 0.70710678118655f, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkRight", 0);
            currentDirection = Direction.DOWN_RIGHT;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Manette.IsUp())
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkUp", 0);
            currentDirection = Direction.UP;
        }
        else if (Input.GetKey(KeyCode.S) || Manette.IsDown())
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - SPEED * Time.deltaTime, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkDown", 0);
            currentDirection = Direction.DOWN;
        }
        else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || Manette.IsLeft())
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkLeft", 0);
            currentDirection = Direction.LEFT;
        }
        else if (Input.GetKey(KeyCode.D) || Manette.IsRight())
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + SPEED * Time.deltaTime, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            animator.Play("PlayerWalkRight", 0);
            currentDirection = Direction.RIGHT;
        }
        else if ((Input.GetKey(KeyCode.Space) || Manette.IsPlaceBlock()) && this.selectedCombustibleItem.Count >= 30)
        {
            this.BurnObjects(300);
            Vector3 objectPlacement = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);

            switch (currentDirection)
            {
                case Direction.LEFT:
                    objectPlacement.x--;
                    break;
                case Direction.RIGHT:
                    objectPlacement.x++;
                    break;
                case Direction.UP:
                    objectPlacement.y++;
                    break;
                case Direction.DOWN:
                    objectPlacement.y--;
                    break;
            }

            Instantiate(wall, objectPlacement, Quaternion.identity);
        }
        else
        {
            if (currentDirection == Direction.DOWN)
            {
                animator.Play("PlayerStopDown", 0);
            }
            else if (currentDirection == Direction.UP)
            {
                animator.Play("PlayerStopUp", 0);
            } 
            else if (currentDirection == Direction.LEFT || currentDirection == Direction.UP_LEFT || currentDirection == Direction.DOWN_LEFT)
            {
                animator.Play("PlayerStopLeft", 0);
            }
            else if (currentDirection == Direction.RIGHT || currentDirection == Direction.UP_RIGHT || currentDirection == Direction.DOWN_RIGHT)
            {
                animator.Play("PlayerStopRight", 0);
            }
            else
            {
                animator.Play("PlayerStopDown", 0);
            }
        }
    }

    public bool PickupObject (CombustibleItem combustibleItem)
    {
        score += (int)combustibleItem.GetQuantityOfEnergy();
        GetComponent<AudioSource>().Play();
        if (this.selectedCombustibleItem.Count < 30)
        {
            this.selectedCombustibleItem.Add(combustibleItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetNumberOfWood ()
    {
        return selectedCombustibleItem.Count;
    }

    public CombustibleItem[] BurnObjects(float leftIntensity)
    {
        List<CombustibleItem> combustibles = new List<CombustibleItem>();
        foreach (CombustibleItem combustibleItem in selectedCombustibleItem)
        {
            if (leftIntensity < combustibleItem.GetQuantityOfEnergy()) break;
            leftIntensity -= combustibleItem.GetQuantityOfEnergy();
            combustibles.Add(combustibleItem);
        }
        foreach (CombustibleItem c in combustibles)
        {
            selectedCombustibleItem.Remove(c);
        }
        return combustibles.ToArray();
    }

    public void SetInteractMessage(bool visible, string tag)
    {
        if (interactMessage is GameObject) interactMessage.SetActive(visible);
        if (tag is string) interactMessage.GetComponent<Text>().text = "Press E to interact with " + tag;
    }

    public void EmitHurtParticle()
    {
        score -= 100;
        hurt.Play();
    }

    public int GetScore ()
    {
        return this.score;
    }
}
