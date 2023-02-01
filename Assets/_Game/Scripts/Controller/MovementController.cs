using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Controller
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;
        [SerializeField] private float turnSmoothTime = .1f;
        [SerializeField] private Transform cam;

        private CharacterController _controller;
        private float _turnSmoothVelocity;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            
            StartCoroutine(MovementRoutine());
        }

        private IEnumerator MovementRoutine()
        {
            while (true)
            {
                var horizontal = Input.GetAxisRaw("Horizontal");
                var vertical = Input.GetAxisRaw("Vertical");
                var direction = new Vector3(horizontal, 0f, vertical).normalized;

                if (direction.magnitude >= .1f)
                {
                    var tarAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarAngle, ref _turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    var moveDir = Quaternion.Euler(0f, tarAngle, 0f) * Vector3.forward;
                    _controller.Move(moveDir * (speed * Time.deltaTime));
                }
                
                yield return 0;
            }
        } 
    }
}
