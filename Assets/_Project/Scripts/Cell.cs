using UnityEngine;

namespace _Project.Scripts
{
    public class Cell : MonoBehaviour
    {
        public (int, int) Index { get; set; }
        public bool IsWall { get; set; }
    }
}