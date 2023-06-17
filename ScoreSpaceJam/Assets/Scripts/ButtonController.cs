using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void start()
    {
        SceneManager.LoadScene("scene");
    }
    public void options()
    {
        SceneManager.LoadScene("Options");

    }
    public void mainmenu()
    {
        //  SceneManager.LoadScene("MainMenu");
         SceneManager.LoadScene(0);

    }
}
