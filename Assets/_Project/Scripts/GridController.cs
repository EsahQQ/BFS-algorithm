using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private int size = 10;
        [SerializeField] private UserController userController;
        private (int, int) _startInd = (-1, -1);
        private (int, int) _endInd = (-1, -1);
        private GameObject[,] _grid;

        public event Action<GameObject, int> OnCellBecomeKeyPoint;
        public event Action<Dictionary<(int, int), (int, int)>> OnCancelFoundAlgorithm;

        private void OnEnable()
        {
            userController.OnUserClickOnCell += ChangeKeyPoint;
            userController.OnUserClickOnStartButton += TryFoundPath;
        }
        
        private void Start()
        {
            _grid = new GameObject[size, size];
            SpawnGrid();
        }

        private void OnDisable()
        {
            userController.OnUserClickOnCell -= ChangeKeyPoint;
            userController.OnUserClickOnStartButton -= TryFoundPath;
        }

        private void TryFoundPath()
        {
            if (_grid == null || _startInd == (-1, -1) || _endInd == (-1, -1))
                return;

            var q = new Queue<(int, int)>();
            var cameFrom = new Dictionary<(int, int), (int, int)>()
            {
                {_startInd, (-1, -1)}
            };
            q.Enqueue(_startInd);
            while (q.Count > 0)
            {
                var (r, c) = q.Dequeue();
                (int, int)[] directions = { (r + 1, c), (r - 1, c), (r, c + 1), (r, c - 1) };

                foreach (var next in directions)
                {
                    int nr = next.Item1;
                    int nc = next.Item2;

                    if (nr >= 0 && nr < _grid.GetLength(0) && nc >= 0 && nc < _grid.GetLength(1))
                    {
                        if (!cameFrom.ContainsKey(next))
                        {
                            cameFrom.Add(next, (r, c));

                            if (next == _endInd)
                            {
                                OnCancelFoundAlgorithm?.Invoke(cameFrom);
                                return;
                            }
                                

                            q.Enqueue(next);
                        }
                    }
                }
            }
            
            OnCancelFoundAlgorithm?.Invoke(cameFrom);
        }

        public GameObject [,] GetGrid()
        {
            return _grid;
        }
        
        public (int, int) GetEndIndex()
        {
            return _endInd;
        }
        
        public (int, int) GetStartIndex()
        {
            return _startInd;
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

        private void SpawnGrid()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    _grid[i, j] = Instantiate(cellPrefab, new Vector3(i, 0, j), Quaternion.identity);
            
            Camera.main!.transform.position = new Vector3((size - 1) / 2f, size * 0.6f, (size - 1) / 2f);
        }
    }
}