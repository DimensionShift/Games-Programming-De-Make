using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{
    [SerializeField] float throwForce = 30f;
    [SerializeField] Transform ballHoldTransform;

    Camera fpsCamera;
    GameObject ball;

    bool hasBall;

    void Start()
    {
        fpsCamera = Camera.main;
        hasBall = true; // Enable this if starting with the ball
        ball = GameObject.Find("Ball");
    }

    void Update()
    {
        if (!hasBall && Mouse.current.rightButton.wasPressedThisFrame) // Temp code, will be setup using the input system
        {
            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.gameObject.name == "Ball")
                {
                    ball = hit.collider.gameObject;
                    ball.transform.parent = ballHoldTransform;

                    Destroy(ball.GetComponent<Rigidbody>());
                    ball.transform.localPosition = Vector3.zero;
                    ball.transform.localRotation = Quaternion.identity;

                    hasBall = true;
                }
            }
        }

        if (hasBall && Mouse.current.leftButton.wasPressedThisFrame) // Temp code, will be setup using the input system
        {
            Rigidbody ballRb = ball.AddComponent<Rigidbody>();
            ball.transform.parent = null;

            ballRb.linearVelocity = Vector3.zero;
            ballRb.mass = 2f;
            ballRb.AddForce(fpsCamera.transform.forward * throwForce, ForceMode.Impulse);
            ballRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            hasBall = false;
        }
    }
}
