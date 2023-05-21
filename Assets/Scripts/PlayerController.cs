using UnityEngine;
using UnityEngine.Serialization;

namespace ProgrammingTest {

    public class PlayerController : MonoBehaviour {

        private static readonly KeyCode FORWARD_KEY = KeyCode.W;
        private static readonly KeyCode BACKWARD_KEY = KeyCode.S;
        private static readonly KeyCode LEFT_KEY = KeyCode.A;
        private static readonly KeyCode RIGHT_KEY = KeyCode.D;

        [FormerlySerializedAs("maxSpeed")] [SerializeField]
        private float m_maxSpeed = default;

        [FormerlySerializedAs("accelerationForce")] [SerializeField]
        private float m_accelerationForce = default;

        [FormerlySerializedAs("mouseSensitivity")] [SerializeField]
        private float m_mouseSensitivity = default;

        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up => Vector3.up;

        private float m_heading;

        private Vector3 m_localMovementDirection;

        private Rigidbody m_rigidBody;

        private void Awake() {
            m_rigidBody = GetComponent<Rigidbody>();
            if (m_rigidBody == null) {
                Debug.Log("Player has no rigidbody. Will be disabled");
                enabled = false;
            }
        }

        private void Update() {
            HandleInput();
            UpdateMovement();
        }

        private void HandleInput() {
            HandleKeyboardInput();
            HandleMouseInput();
        }

        private void HandleKeyboardInput() {

            float lateralMovement = 0;
            float longitudinalMovement = 0;

            if (Input.GetKey(FORWARD_KEY)) {
                longitudinalMovement += 1;
            }

            if (Input.GetKey(BACKWARD_KEY)) {
                longitudinalMovement -= 1;
            }

            if (Input.GetKey(RIGHT_KEY)) {
                lateralMovement += 1;
            }

            if (Input.GetKey(LEFT_KEY)) {
                lateralMovement -= 1;
            }

            m_localMovementDirection = (new Vector3(lateralMovement, 0, longitudinalMovement)).normalized;
        }

        private void HandleMouseInput() {

            m_heading += Input.GetAxis("Mouse X") * m_mouseSensitivity;

            Forward = new Vector3(Mathf.Sin(m_heading), 0, Mathf.Cos(m_heading));
            Right = new Vector3(Mathf.Cos(m_heading), 0, -Mathf.Sin(m_heading));
        }

        private void UpdateMovement() {

            Vector3 globalMovementDirection = m_localMovementDirection.x * Right + m_localMovementDirection.z * Forward;
            float speedInMovementDirection = Vector3.Dot(globalMovementDirection, m_rigidBody.velocity);

            float relativeSpeed = m_maxSpeed - speedInMovementDirection;

            if (relativeSpeed > 0) {
                m_rigidBody.AddForce(m_accelerationForce * globalMovementDirection);
            }

        }

    }

}
