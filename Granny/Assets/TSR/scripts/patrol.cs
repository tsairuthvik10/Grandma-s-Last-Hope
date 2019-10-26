using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public float speed;
    public float lassoSpeed = 1.0f;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;
    public GameObject ball;
    Vector2 ballPos;
    public float fireRate = 3.0f;
    float nextFire;
    public GameObject hook;
    public GameObject player;
    //public GameObject[] enemies;
   // public bool hugActive = false;
    public bool Captured;
    bool catchEnemy;
    public float hugTime = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // enemies = GameObject.FindGameObjectsWithTag("Enemy");
      


    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Captured)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }

            nextFire += Time.deltaTime;

            if (nextFire > fireRate && GameManager.hugActive == false&& (Vector2.Distance(player.transform.position, transform.position)<10f))
            {
                Fire();
                nextFire = 0;
            }
        }
        if (GameManager.hugActive == true && (Vector2.Distance(player.transform.position, transform.position) < 10f))
        {
            float step = lassoSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(this.gameObject.transform.position, player.transform.position, step);

            if (Captured)
            {

                transform.Translate(0, 0, 0);
                // transform.position = player.transform.position;
                transform.rotation = player.transform.rotation;
                hugTime += Time.deltaTime;
                player.GetComponent<Animator>().SetBool("Hug", true);

                if (hugTime >= 2)
                {
                    Captured = false;
                    GameManager.hugActive = false;
                    player.GetComponent<Animator>().SetBool("Hug", false);
                    Destroy(this.gameObject);
                    
                }
            }
           // GameManager.hugActive = false;
        }

    }

    void Fire()
    {
        ballPos = transform.position;
        ballPos += new Vector2(-1f, -0.43f);
        Instantiate(ball, ballPos, Quaternion.identity);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag=="Hook")
        //{
        //    float step = lassoSpeed * Time.deltaTime;
        //    transform.position = Vector2.MoveTowards(this.gameObject.transform.position, player.transform.position, step);
        //}
        if (collision.gameObject.tag == "Player")
        {
            
            Captured = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;


            //GameManager.ropeActive = false;
        }

     
    }


   /* void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && (Vector2.Distance(player.transform.position, transform.position) < 30.0f ))
        {

            
            if (GameManager.ropeActive == false)
            {
                hugActive = true;
                Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                curHook.GetComponent<RopeScript>().destiny = destiny;
                GameManager.ropeActive = true;

            }
            else
            {

                //delete rope

                Destroy(curHook);


                GameManager.ropeActive = false;
                hugActive = false;
            }
        }
    }*/
}
