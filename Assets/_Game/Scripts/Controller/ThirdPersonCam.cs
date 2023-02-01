using UnityEngine;

namespace _Game.Scripts.Controller
{
    public class ThirdPersonCam : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform player;
        [SerializeField] private Transform playerObj;
        [SerializeField] private Rigidbody rb;
        
        [SerializeField] private float rotationSpeed;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // Rotate orientation
            var viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;
            
            // Rotate player object
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward =
                    Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        }
    }
}