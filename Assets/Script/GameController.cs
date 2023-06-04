using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    bool isGamePlay = false;

    public GameObject menuScreen;
    public GameObject controlsScreen;
    public GameObject scoreScreen;

    public static int level;
    public static int score;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        menuScreen.SetActive(true);
        controlsScreen.SetActive(false);
        scoreScreen.SetActive(false);

        level = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGamePlay)
        {
            isGamePlay = true;

            GetComponent<TetrominoSpawner>().NewTetromino();

            menuScreen.SetActive(false);
            controlsScreen.SetActive(true);
            scoreScreen.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && isGamePlay)
        {
            Reset();
        }

        if (!isGamePlay)
        {
            GameObject[] mino = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject respawn in mino)
            {
                Destroy(respawn);
            }
        }
    }

    public void Reset()
    {
            isGamePlay = false;

            // Delete Any Remaining Tiles //
            for (int i = 0; i < TetrominoController.width; i++)
            {
                for (int o = 0; o < TetrominoController.height; o++)
                {
                    for (int p = 0; p < TetrominoController.width; p++)
                    {
                        if (TetrominoController.grid[i, o, p] != null)
                        {
                            Destroy(TetrominoController.grid[i, o, p].gameObject);
                        }
                    }
                }
            }

            GameObject[] mino = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject respawn in mino)
            {
                Destroy(respawn);
            }

            // Reset Grid For New Game //
            TetrominoController.grid = new Transform[TetrominoController.width, TetrominoController.height, TetrominoController.width];

            menuScreen.SetActive(true);
            controlsScreen.SetActive(false);
            scoreScreen.SetActive(false);

            level = 1;
            score = 0;
        

        levelText.text = "Level: " + level.ToString();
        scoreText.text = "Score: " + score.ToString();
    }
}