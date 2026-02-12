using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeReference] private Material redMaterial;
        [SerializeReference] private Material greenMaterial;
        [SerializeReference] private Material blueMaterial;
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
        }

        private void OnDisable()
        {
            _gridController.OnCellBecomeKeyPoint -= ChangeKeyPointCell;
        }

        private void ChangeKeyPointCell(GameObject obj, int dir)
        {
            if (dir == 0)
            {
                if (_startCell != null)
                    _startCell.GetComponent<Renderer>().material = defaultMaterial;
                _startCell = obj;
                _startCell.GetComponent<Renderer>().material = greenMaterial;
            }

            if (dir == 1)
            {
                if (_endCell != null)
                    _endCell.GetComponent<Renderer>().material = defaultMaterial;
                _endCell = obj;
                _endCell.GetComponent<Renderer>().material = redMaterial;
            }
        }
    }
}