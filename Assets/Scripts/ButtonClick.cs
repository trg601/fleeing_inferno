using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPlay() {
        SceneManager.LoadScene("Level1");
    }

    public void ClickHowTo() {
        SceneManager.LoadScene("HowToPlay");
    }

    public void ClickQuit() {
        Application.Quit();
    }

    public void ClickMenu() {
        SceneManager.LoadScene("Start");
    }
}
