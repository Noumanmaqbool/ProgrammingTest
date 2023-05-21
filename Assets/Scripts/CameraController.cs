using UnityEngine;
using UnityEngine.Serialization;

namespace ProgrammingTest {

    public class CameraController : MonoBehaviour {

        [FormerlySerializedAs("player")] [SerializeField]
        private PlayerController m_player = default;

        [FormerlySerializedAs("distance")] [SerializeField]
        private float m_distance = default;

        [FormerlySerializedAs("height")] [SerializeField]
        private float m_height = default;

        private void Start() {
            UpdatePose();
        }

        private void LateUpdate() {
            UpdatePose();
        }

        private void UpdatePose() {
            transform.position = m_player.transform.position - m_player.Forward * m_distance + m_player.Up * m_height;
            transform.rotation = Quaternion.LookRotation(m_player.transform.position - transform.position, m_player.Up);
        }

    }

}
