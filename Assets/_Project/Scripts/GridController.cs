using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int size = 10;
        [SerializeField] private UserController userController;
        private (int, int) _startInd;
        private (int, int) _endInd;
        private GameObject[,] _grid;

        public event Action<GameObject, int> OnCellBecomeKeyPoint;

        private void OnEnable()
        {
            userController.OnUserClickOnCell += ChangeKeyPoint;
        }

        private void ChangeKeyPoint(Transform obj, int dir)
        {
            var ind = FindInGrid(obj.gameObject);
            if (ind == (-1, -1)) return;
            switch (dir)
            {
                case 0:
                    OnCellBecomeKeyPoint?.Invoke(obj.gameObject, dir);
                    _startInd = ind;
                    break;
                case 1:
                    OnCellBecomeKeyPoint?.Invoke(obj.gameObject, dir);
                    _endInd = ind;
                    break;
            }
        }

        private (int, int) FindInGrid(GameObject obj)
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
                for (var j = 0; j < _grid.GetLength(1); j++)
                    if (obj == _grid[i, j])
                        return (i,j);

            return (-1, -1);
        }
        
        private void Start()
        {
            _grid = new GameObject[size, size];
            SpawnGrid();
        }

        private void OnDisable()
        {
            userController.OnUserClickOnCell -= ChangeKeyPoint;
        }

        private void SpawnGrid()
        {
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _grid[i, j] = Instantiate(cellPrefab, new Vector3(i, 0, j), Quaternion.identity);
        }
    }
}