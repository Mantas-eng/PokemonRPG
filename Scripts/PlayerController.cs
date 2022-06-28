using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{

    public Rigidbody2D therb;
    public float moveSpeed;
    public Animator anim;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public LayerMask grassLayer;
   

    public event Action OnEncountered;
    public bool canMove = true;
    void Start()
    {

        if (instance == null)
        {
            instance = this; 
        }
        else 
        {
            if (instance != this)
            {
                Destroy(gameObject); 
                

            }
        }

        DontDestroyOnLoad(gameObject);

    }

    
    public  void HandleUpdate()
    {
        if (canMove)
        {
            therb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        }
        else
        {
            therb.velocity = Vector2.zero; 
        }

        anim.SetFloat("moveX", therb.velocity.x);
        anim.SetFloat("moveY", therb.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                anim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
        CheckForEncounters();
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftLimit = bottomLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }

    private void CheckForEncounters()
    {
        
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {

                OnEncountered();
            }
        }
    }
}