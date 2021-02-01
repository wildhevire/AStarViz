using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wildhevire.AStarViz
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GridProperties gridProperties;

        [Range(0, 2)]   public float moveSpeed = .5f;
        [Range(0, 10)]  public float smoothFactor = .5f;
        [Range(0, 10)]  public float mouseSensitivity = .5f;
        private Vector3 newPosition;
        private Vector2 newRotation;

        private void Start()
        {
            transform.position = new Vector3(gridProperties.dimension.x / 2, transform.position.y, gridProperties.dimension.y / 2);
        }
        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                var forwardVector = transform.forward;
                forwardVector.y = 0;
                forwardVector.Normalize();
                newPosition += forwardVector * moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                var forwardVector = transform.forward;
                forwardVector.y = 0;
                forwardVector.Normalize();
                newPosition -= forwardVector * moveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newPosition -= transform.right * moveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newPosition += transform.right * moveSpeed;
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                newRotation.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                newRotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
            }

            transform.eulerAngles = newRotation;
            transform.position = Vector3.Lerp(transform.position, transform.forward + newPosition, Time.deltaTime * smoothFactor);
        }
    }
}
