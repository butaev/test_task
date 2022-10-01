using UnityEngine;
using UnityEngine.AI;

namespace BattleArena.Arena
{
    public class AreaController : MonoBehaviour
    {
        [SerializeField] private NavMeshObstacle _leftBorder;
        [SerializeField] private NavMeshObstacle _rightBorder;
        [SerializeField] private NavMeshObstacle _upBorder;
        [SerializeField] private NavMeshObstacle _downBorder;
        [SerializeField] private float _borderOffset = 0.5f;

        private Vector2 _size;

        public void Initialize(Vector2 size)
        {
            _size = size;
            _leftBorder.transform.position -= Vector3.right * (size.x / 2f + _leftBorder.size.x / 2f);
            _rightBorder.transform.position += Vector3.right * (size.x / 2f + _rightBorder.size.x / 2f);
            _upBorder.transform.position += Vector3.forward * (size.y / 2f + _upBorder.size.z / 2f);
            _downBorder.transform.position -= Vector3.forward * (size.y / 2f + _downBorder.size.z / 2f);

            _leftBorder.gameObject.transform.localScale += Vector3.forward * size.y;
            _rightBorder.gameObject.transform.localScale += Vector3.forward * size.y;
            _upBorder.gameObject.transform.localScale += Vector3.right * size.x;
            _downBorder.gameObject.transform.localScale += Vector3.right * size.x;
        }

        public Vector3 GetRandomPositionInArea()
        {
            return new Vector3(Random.Range(-(_size.x / 2f - _borderOffset), _size.x / 2f - _borderOffset), 0f,
                Random.Range(-(_size.y / 2f - _borderOffset), _size.y / 2f - _borderOffset));
        }
    }
}