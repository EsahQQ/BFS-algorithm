using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class UserController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI modeText;
        private Camera _cam;
        private bool _isWallMode;
        public event Action<Transform, int> OnUserClickOnCell;
        public event Action<Transform> OnUserDoWall;
        public event Action OnUserClickOnStartButton;
        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (_isWallMode)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var ray = _cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out var hit, 1000f))
                        if (hit.transform != null)
                            OnUserDoWall?.Invoke(hit.transform);
                }
                
                return;
            }
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

        public void SwitchWallMode()
        {
            _isWallMode = !_isWallMode;
            modeText.text = _isWallMode ? "KeyMode" : "WallMode";
        }
        
        public void StartPath()
        {
            OnUserClickOnStartButton?.Invoke();
        }
    }
}