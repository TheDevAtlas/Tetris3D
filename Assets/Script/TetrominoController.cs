using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoController : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 10;
    public static int width = 7;
    public static Transform[,,] grid = new Transform[width, height, width];

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!IsValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!IsValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1);
            if (!IsValidMove())
                transform.position -= new Vector3(0, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1);
            if (!IsValidMove())
                transform.position -= new Vector3(0, 0, -1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!IsValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!IsValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), -90);
            if (!IsValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), 90);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), 90);
            if (!IsValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(1, 0, 0), -90);
        }

        if (Time.time - previousTime > (Input.GetKey(KeyCode.Space) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!IsValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<TetrominoSpawner>().NewTetromino();
            }
            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            if(roundedY >= height)
            {
                GameObject gc = GameObject.FindGameObjectWithTag("GameController");
                gc.GetComponent<GameController>().Reset();
                Destroy(this.gameObject);
                return;
            }

            grid[roundedX, roundedY, roundedZ] = children;
        }
    }

    void CheckLines()
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
        for (int k = 0; k < width; k++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, i, k] == null)
                    return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int k = 0; k < width; k++)
        {
            for (int j = 0; j < width; j++)
            {
                Destroy(grid[j, i, k].gameObject);
                grid[j, i, k] = null;
            }
        }

        GameController.score += 100 * GameController.level;
        GameController.level += 1;
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int k = 0; k < width; k++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[j, y, k] != null)
                    {
                        grid[j, y - 1, k] = grid[j, y, k];
                        grid[j, y, k] = null;
                        grid[j, y - 1, k].transform.position -= new Vector3(0, 1, 0);
                    }
                }
            }
        }
    }

    bool IsValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedZ < 0 || roundedZ >= width)
            {
                return false;
            }

            //if (roundedY >= height)
            //{
            //    GameObject gc = GameObject.FindGameObjectWithTag("GameController");
            //    gc.GetComponent<GameController>().Reset();
            //    return false;
            //}

            if (grid[roundedX, roundedY, roundedZ] != null)
                return false;
        }
        return true;
    }
}
