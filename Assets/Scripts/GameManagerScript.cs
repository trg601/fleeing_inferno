using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int numberOfBalls = 5;
    public int numBricks;
    private int sceneNumber;
    private string[] scenes = {"Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9", "Escape"};
    private Text ballLabel;

    // Start is called before the first frame update
    void Start()
    {
        ballLabel = GameObject.Find("BallLabel").GetComponent<Text>();
        sceneNumber = System.Array.IndexOf(scenes, SceneManager.GetActiveScene().name);
        updateBallLabel();
    }

    // Update is called once per frame
    void Update()
    {
        //Cheats
        if (Input.GetKeyDown(KeyCode.N)) {
            SceneManager.LoadScene(scenes[sceneNumber + 1]);
        }
        if (Input.GetKeyDown(KeyCode.B) && sceneNumber > 0) SceneManager.LoadScene(scenes[sceneNumber - 1]);
        if (Input.GetKeyDown(KeyCode.H)) {
            SceneManager.LoadScene("Start");
        }
    }

    public void loseBall() {
        numberOfBalls--;
        updateBallLabel();
        if (numberOfBalls <= 0)
            SceneManager.LoadScene("GameOver");
    }

    public void destroyBrick() {
        numBricks--;
        if (numBricks <= 0)
            SceneManager.LoadScene(scenes[sceneNumber + 1]);
    }

    private void updateBallLabel() {
        if (numberOfBalls == 1) ballLabel.text = numberOfBalls.ToString() + " Ball";
        else ballLabel.text = numberOfBalls.ToString() + " Balls";
    }

}