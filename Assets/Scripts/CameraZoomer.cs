using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildhevire.AStarViz
{
    public class CameraZoomer : MonoBehaviour
    {
        public Transform CameraTarget;
        private float x = 0.0f;
        private float y = 0.0f;

        private int mouseXSpeedMod = 5;
        private int mouseYSpeedMod = 5;

        public float MaxViewDistance = 200f;
        public float MinViewDistance = 50f;
        public int ZoomRate = 20;
        private float distance = 50f;
        private float desireDistance;

        public float cameraTargetHeight = 1.0f;
        public bool canRotateTarget = true;


        void Start()
        {
            Vector3 Angles = transform.rotation.eulerAngles;
            x = Angles.y;
            y = Angles.x;
            desireDistance = distance;
        }

        void LateUpdate()
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
                y += Input.GetAxis("Mouse Y") * mouseYSpeedMod;
            }



            y = ClampAngle(y, 0, 180);
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            desireDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desireDistance);
            desireDistance = Mathf.Clamp(desireDistance, MinViewDistance, MaxViewDistance);

            Vector3 position = CameraTarget.position - (rotation * Vector3.forward * desireDistance);

            transform.rotation = rotation;
            transform.position = position;


            float cameraX = transform.rotation.x;
            if (Input.GetMouseButton(1) && canRotateTarget)
            {
                CameraTarget.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);
            }

        }
        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }

    }
}
