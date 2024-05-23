using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris3D : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20, width = 10, depth = 10; // Added depth for the third dimension
    public Vector3 rotationPoint;
    private static Transform[,,] grid = new Transform[width, height, depth]; // 3D grid

    public int score = 0;
    public GameObject[] tetrominoes; // Array to store the different tetromino prefabs
    private GameObject currentTetromino, heldTetromino, nextTetromino;
    private Vector3 spawnPosition = new Vector3(5, 18, 5); // Adjust spawn position for 3D
    private bool canHold = true;

    public Text scoreText;

    void Start()
    {
        SpawnNextTetromino();
    }

    void Update()
    {
        // MOVE TETROMINO //
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveTetromino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            MoveTetromino(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveTetromino(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateTetromino(Vector3.up, 90); // Rotate around Y-axis for 3D rotation
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotateTetromino(Vector3.forward, 90); // Rotate around Z-axis
        }


        // MOVE DOWN //
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time - previousTime > (fallTime / 10))
            {
                MoveTetromino(Vector3.down);
                previousTime = Time.time;
            }
        }

        // SWAP BLOCK //
        if (Input.GetKeyDown(KeyCode.C))
        {
            HoldTetromino();
        }

        // TIME //
        if (Time.time - previousTime > fallTime)
        {
            MoveTetromino(Vector3.down);
            previousTime = Time.time;
        }

        scoreText.text = score.ToString();
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
                SpawnNextTetromino();
            }
        }
    }

    void RotateTetromino(Vector3 axis, float angle)
    {
        currentTetromino.transform.RotateAround(currentTetromino.transform.TransformPoint(rotationPoint), axis, angle);
        if (!IsValidMove())
        {
            currentTetromino.transform.RotateAround(currentTetromino.transform.TransformPoint(rotationPoint), axis, -angle);
        }
    }

    bool IsValidMove()
    {
        foreach (Transform children in currentTetromino.transform)
        {
            Vector3 pos = children.transform.position;
            int roundedX = Mathf.RoundToInt(pos.x);
            int roundedY = Mathf.RoundToInt(pos.y);
            int roundedZ = Mathf.RoundToInt(pos.z);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height || roundedZ < 0 || roundedZ >= depth)
                return false;

            if (grid[roundedX, roundedY, roundedZ] != null)
                return false;
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in currentTetromino.transform)
        {
            Vector3 pos = children.transform.position;
            int roundedX = Mathf.RoundToInt(pos.x);
            int roundedY = Mathf.RoundToInt(pos.y);
            int roundedZ = Mathf.RoundToInt(pos.z);
            grid[roundedX, roundedY, roundedZ] = children;
        }
    }

    void CheckForLines()
    {
        // Check all horizontal layers (parallel to the XZ plane)
        for (int y = 0; y < height; y++)
        {
            if (IsFullLayerXZ(y))
            {
                ClearLayerXZ(y);
            }
        }
    }

    bool IsFullLayerXZ(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (grid[x, y, z] == null)
                    return false;
            }
        }
        return true;
    }

    void ClearLayerXZ(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
        score += 300; // Increase score for clearing a whole layer
    }

    void HoldTetromino()
    {
        if (!canHold) return;

        if (heldTetromino == null)
        {
            heldTetromino = currentTetromino;
            SpawnNextTetromino();
        }
        else
        {
            GameObject temp = currentTetromino;
            currentTetromino = heldTetromino;
            heldTetromino = temp;
            currentTetromino.transform.position = spawnPosition;
        }

        canHold = false;
    }

    public void SpawnNextTetromino()
    {
        if (nextTetromino != null)
        {
            score += 50;

            currentTetromino = Instantiate(nextTetromino, spawnPosition, Quaternion.identity);
            nextTetromino = tetrominoes[Random.Range(0, tetrominoes.Length)];
        }
        else
        {
            currentTetromino = Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], spawnPosition, Quaternion.identity);
            nextTetromino = tetrominoes[Random.Range(0, tetrominoes.Length)];
        }

        canHold = true; // Reset the ability to hold when a new tetromino spawns
    }
}
