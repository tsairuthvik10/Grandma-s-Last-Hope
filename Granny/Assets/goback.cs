using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class goback : MonoBehaviour
{
    public float back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        back += Time.deltaTime;

        if(back > 2.0f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
