using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Project.Scripts
{
    public class UserController : MonoBehaviour
    {
        private Camera _cam;

        public event Action<Transform, int> OnUserClickOnCell;
        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            var btn = -1;
            if (Input.GetMouseButtonDown(0))
                btn = 0;
            if (Input.GetMouseButtonDown(1))
                btn = 1;

            if (btn == 0 || btn == 1)
            {
                var ray = _cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 1000f))
                    if (hit.transform != null)
                        OnUserClickOnCell?.Invoke(hit.transform, btn);
            }
        }
    }
}