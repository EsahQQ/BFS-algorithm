using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Project.Scripts
{
    public class UserController : MonoBehaviour
    {
        private Camera _cam;

        public event Action<Transform> OnUserClickOnCell;
        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 1000f))
                    if (hit.transform != null)
                        OnUserClickOnCell?.Invoke(hit.transform);
            }
        }
    }
}