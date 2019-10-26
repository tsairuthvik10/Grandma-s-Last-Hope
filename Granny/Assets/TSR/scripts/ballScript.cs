using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ballScript : MonoBehaviour
{
    public float velX;
    public float velY;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-velX, velY);
        rb.AddForce(transform.up * 1);
        rb.AddForce(transform.forward * 1);


    }

    // Update is called once per frame
    void Update()
    {
      

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.hugActive = false;
            GameManager.ropeActive = false;
            SceneManager.LoadScene(1);
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Rope")
        {
            Destroy(this.gameObject);
        }
    }
}
