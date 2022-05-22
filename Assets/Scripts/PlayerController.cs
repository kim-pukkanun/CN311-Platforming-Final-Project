using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    private float moveX;
    private float moveY;
    private float rotateDeath;

    public string playerID;

    public bool isDisable = false;
    public bool isSocket = false; 
    public bool isDeathCount = false;


    private void Awake()
    {
        if (playerID != gameObject.name) {
            isSocket = true;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        moveSpeed = 1f;
        jumpForce = 20f;
        isJumping = false;
    }

    // Update is called once per frame
    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // gameObject.transform.position = new Vector3(x, y, 0);
    }

    private void FixedUpdate() 
    {
        if (!isSocket) {
            #region walk_jump
            if ((moveX > 0.1f || moveX < -0.1f) && !isDisable) {
                rb2D.AddForce(new Vector2(moveX * moveSpeed, 0f), ForceMode2D.Impulse);
            }

            if (!isJumping && moveY > 0.1f && !isDisable) {
                rb2D.AddForce(new Vector2(0f, moveY * jumpForce), ForceMode2D.Impulse);
            }
            #endregion

            #region death
            rotateDeath = gameObject.transform.localRotation.eulerAngles.z;

            if (((rotateDeath > 85 && rotateDeath < 95) || (rotateDeath < 280 && rotateDeath > 270) || (rotateDeath > 179 && rotateDeath < 181)) && !isDisable && !isDeathCount) {
                Debug.Log("Enter on " + rotateDeath);
                Thread t1 = new Thread(OnDeath);
                t1.Start();
                isDeathCount = true;
            }

            if (isDisable) {
                SceneManager.LoadScene(1);
            }
            #endregion

            #region animCheck
            if (moveX > 0.1f && !isDisable) {
                animator.SetBool("isRunning", true);
                gameObject.transform.localScale = new Vector2(1, 1);
            } else if (moveX < -0.1f && !isDisable) {
                animator.SetBool("isRunning", true);
                gameObject.transform.localScale = new Vector2(-1, 1);
            } else {
                animator.SetBool("isRunning", false);
            }
            #endregion
        }
    }

    private void OnDeath() 
    {
        for (var i = 5; i > 0; i--) {
            Debug.Log("Player Die in " + i);
            Thread.Sleep(1000);
            if (rotateDeath < 85 || rotateDeath > 280) {
                Debug.Log("Exit on " + rotateDeath);
                isDeathCount = false;
                return;
            }
        }
        
        if ((rotateDeath > 85 && rotateDeath < 95) || (rotateDeath < 280 && rotateDeath > 270) || (rotateDeath > 179 && rotateDeath < 181)) {
            isDisable = true;
            Debug.Log("Player has been disabled");
        } 

        isDeathCount = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box")) {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box")) {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }
}
