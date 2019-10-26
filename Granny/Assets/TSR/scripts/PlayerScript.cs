using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {


	public float forcetoAdd=250;
    public float speed = 2;
    public float jpX = 10;
    public float jpY = 10;
    float destroyTime =0;
    public bool onLedge = false;
    public bool onEnemy = false;
    public CharacterController2D controller;
    public float runSpeed = 40.0f;
    float horizontalMove = 0.0f;
    bool jump = false;

    private bool oneJump;
    public GameObject hook;
    public GameObject curHook;

    bool onGround = false;
    Rigidbody2D rb;
    GameObject[] enemies;

    public Animator anim;
    

	void Start () {
		//gives it force
        
        rb = GetComponent<Rigidbody2D>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        

    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        if(horizontalMove>0f|| horizontalMove<0f)
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
       
        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
    }


    void FixedUpdate () {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if( (hit.collider.gameObject.tag == "Ledge")|| ((hit.collider.gameObject.tag == "Enemy")&& (Vector2.Distance(mousePos2D, this.transform.position) < 10f)))
            {
                if (GameManager.ropeActive == false)
                {
                    Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                    curHook.GetComponent<RopeScript>().destiny = destiny;
                    GameManager.ropeActive = true;
                }
                if(hit.collider.gameObject.tag == "Ledge" && !onLedge)
                {
                    onLedge = true;
                }
                if (hit.collider.gameObject.tag == "Enemy" && !onEnemy)
                {
                    onEnemy = true;
                    GameManager.hugActive = true;

                }
            }
            else
            {
                Debug.Log("no enemy or ledge");
            }
        }
        if (GameManager.ropeActive== true)
        {
            if (Input.GetKey(KeyCode.A))
                GetComponent<Rigidbody2D>().AddForce(-Vector2.right * forcetoAdd * 0.5f);
               

            if (Input.GetKey(KeyCode.D))
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * forcetoAdd * 0.5f);
                
            if (Input.GetKey(KeyCode.W))
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * forcetoAdd * 0.5f);
                
            if (Input.GetKey(KeyCode.S))
                GetComponent<Rigidbody2D>().AddForce(-Vector2.up * forcetoAdd * 0.5f);
                
           
            if (Input.GetMouseButtonUp(0) && onLedge  )
            {
               
                Destroy(curHook);
                GameManager.ropeActive = false;
                onLedge = false;
            

            }

            if (onEnemy)
            {
                
                destroyTime += Time.fixedDeltaTime;
                Debug.Log("destroy time:"+destroyTime);

                if (destroyTime >= 2.0f)
                {
                   // Debug.Log("in");
                    Destroy(curHook);

                    GameManager.ropeActive = false;
                    //GameManager.hugActive = false;
                    onEnemy = false;
                    destroyTime = 0.0f;
                }
               
            }

        }

        /*else if (GameManager.ropeActive == false)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += movement * speed *Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                    Jump();
                
            }


        }*/



    }			
    /*void Jump()
    {
        if (onGround)
        {
        rb.AddForce(transform.up * forcetoAdd);
            
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        /*if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("ground touching");
            onGround = true;
        }*/

        if (collision.gameObject.tag == "dead")
        {
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.tag == "grandson")
        {
            SceneManager.LoadScene(2);
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("ground not touching");
            onGround = false;
        }
    }*/
}
