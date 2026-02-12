using System;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int size = 10;

    private GameObject[,] _grid;
    private void Start()
    {
        _grid = new GameObject[size, size];
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _grid[i,j] = Instantiate(cellPrefab, new Vector3(i, 0, j), Quaternion.identity);
    }
}
