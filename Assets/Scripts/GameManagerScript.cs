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
    private float timer;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        ballLabel = GameObject.Find("BallLabel").GetComponent<Text>();
        string sceneName = SceneManager.GetActiveScene().name;
        sceneNumber = System.Array.IndexOf(scenes, sceneName);
        updateBallLabel();

        audioManager = GetComponent<AudioManager>();

        //start timer
        timer = 0;

        if (sceneName.Equals("Level1")) {
            //Reset saved timer
            PlayerPrefs.SetFloat("timer", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Cheats
        if (Input.GetKeyDown(KeyCode.N)) 
            completeScene();
        if (Input.GetKeyDown(KeyCode.B) && sceneNumber > 0)
            SceneManager.LoadScene(scenes[sceneNumber - 1]);
        if (Input.GetKeyDown(KeyCode.H))
            SceneManager.LoadScene("Start");
        if (Input.GetKeyDown(KeyCode.G))
            SceneManager.LoadScene("GameOver");
        if (Input.GetKeyDown(KeyCode.Y))
            SceneManager.LoadScene("Escape");

        //Update timer
        timer += Time.deltaTime;
    }

    public AudioManager GetAudioManager() {
        return audioManager;
    }

    public void loseBall() {
        numberOfBalls--;
        updateBallLabel();
        if (numberOfBalls <= 0) {
            audioManager.PlayClip(AudioManager.Clip.PlayerDeath);
            GameObject.Find("Ball").SetActive(false);
            StartCoroutine(gameOver());
        }
    }

    public void destroyBrick() {
        numBricks--;
        if (numBricks <= 0) completeScene();
    }

    public void completeScene() {
        PlayerPrefs.SetFloat("timer", PlayerPrefs.GetFloat("timer", 0) + timer);
        SceneManager.LoadScene(scenes[sceneNumber + 1]);
    }

    private void updateBallLabel() {
        if (numberOfBalls == 1) ballLabel.text = numberOfBalls.ToString() + " Ball";
        else ballLabel.text = numberOfBalls.ToString() + " Balls";
    }

    IEnumerator gameOver() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

}