using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeReference] private Material endPointMaterial;
        [SerializeReference] private Material startPointMaterial;
        [SerializeReference] private Material foundCellMaterial;
        [SerializeReference] private Material pathMaterial;
        [SerializeReference] private Material defaultMaterial;
        private GameObject _startCell;
        private GameObject _endCell;
        private GridController _gridController;

        private void Awake()
        {
            _gridController = GetComponent<GridController>();
        }

        private void OnEnable()
        {
            _gridController.OnCellBecomeKeyPoint += ChangeKeyPointCell;
            _gridController.OnCancelFoundAlgorithm += DoFoundVisual;
        }

        private void DoFoundVisual(Dictionary<(int, int), (int, int)> e)
        {
            StartCoroutine(ChangeFoundCellsColor(e));
        }

        private IEnumerator ChangeFoundCellsColor(Dictionary<(int, int), (int, int)> e)
        {
            foreach (var c in e.Keys)
            {
                if (c != _gridController.GetStartIndex()  && c != _gridController.GetEndIndex())
                {
                    _gridController.GetGrid()[c.Item1, c.Item2].GetComponent<Renderer>().material = foundCellMaterial;
                    yield return new WaitForSeconds(0.005f);
                }
            }
            
            DoPathVisual(e, e[_gridController.GetEndIndex()]);
        }
        
        private void DoPathVisual(Dictionary<(int, int), (int, int)> e, (int, int) ind)
        {
            if (ind == _gridController.GetStartIndex())
                return;
            
            _gridController.GetGrid()[ind.Item1, ind.Item2].GetComponent<Renderer>().material = pathMaterial;
            DoPathVisual(e, e[ind]);
        }
        

        private void OnDisable()
        {
            _gridController.OnCellBecomeKeyPoint -= ChangeKeyPointCell;
            _gridController.OnCancelFoundAlgorithm += DoFoundVisual;
        }

        private void ChangeKeyPointCell(GameObject obj, int dir)
        {
            if (dir == 0)
            {
                if (_startCell != null)
                    _startCell.GetComponent<Renderer>().material = defaultMaterial;
                _startCell = obj;
                _startCell.GetComponent<Renderer>().material = startPointMaterial;
            }

            if (dir == 1)
            {
                if (_endCell != null)
                    _endCell.GetComponent<Renderer>().material = defaultMaterial;
                _endCell = obj;
                _endCell.GetComponent<Renderer>().material = endPointMaterial;
            }
        }
    }
}