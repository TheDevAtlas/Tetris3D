using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
        GameController.score += 10 * GameController.level;
    }

    void Start()
    {
        //NewTetromino();
    }
}
