using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris2DManager : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20, width = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, height];

    public int score = 0;
    public GameObject[] tetrominoes; // Array to store the different tetromino prefabs
    private GameObject currentTetromino, heldTetromino, nextTetromino;
    private Vector3 spawnPosition = new Vector3(5, 18, 0); // Adjust spawn position as needed
    private bool canHold = true;

    void Start()
    {
        SpawnNextTetromino();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTetromino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateTetromino();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Time.time - previousTime > (fallTime / 10))
            {
                MoveTetromino(Vector3.down);
                previousTime = Time.time;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HoldTetromino();
        }

        if (Time.time - previousTime > fallTime)
        {
            MoveTetromino(Vector3.down);
            previousTime = Time.time;
        }
    }

    void MoveTetromino(Vector3 direction)
    {
        currentTetromino.transform.position += direction;
        if (!IsValidMove())
        {
            currentTetromino.transform.position -= direction;
            if (direction == Vector3.down)
            {
                AddToGrid();
                CheckForLines();
                this.SpawnNextTetromino();
            }
        }
    }

    void RotateTetromino()
    {
        currentTetromino.transform.RotateAround(currentTetromino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!IsValidMove())
            currentTetromino.transform.RotateAround(currentTetromino.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
    }

    bool IsValidMove()
    {
        foreach (Transform children in currentTetromino.transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                return false;

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in currentTetromino.transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
        }
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        score += 100;
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void HoldTetromino()
    {
        return;

        if (!canHold) return;

        if (heldTetromino == null)
        {
            heldTetromino = currentTetromino;
            SpawnNextTetromino(); // method to spawn a Tetromino
        }
        else
        {
            GameObject temp = currentTetromino;
            currentTetromino = heldTetromino;
            heldTetromino = temp;
            currentTetromino.transform.position = new Vector3(width / 2, height - 1, 0);
        }

        canHold = false;
    }

    public void SpawnNextTetromino()
    {
        if (nextTetromino != null)
        {
            currentTetromino = Instantiate(nextTetromino, spawnPosition, Quaternion.identity);
            nextTetromino = tetrominoes[Random.Range(0, tetrominoes.Length)];
            // Update the UI to show next tetromino
        }
        else
        {
            currentTetromino = Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], spawnPosition, Quaternion.identity);
            nextTetromino = tetrominoes[Random.Range(0, tetrominoes.Length)];
            // Update the UI to show next tetromino
        }

        canHold = true; // Reset the ability to hold when a new tetromino spawns
    }
}
