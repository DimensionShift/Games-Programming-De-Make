using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwing : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform leftHandObjectHoldTransform;
    [SerializeField] Transform rightHandObjectHoldTransform;
    [SerializeField] Transform leftArm;
    [SerializeField] Transform rightArm;
    [SerializeField] float throwForce = 50f;

    Camera fpsCamera;
    public GameObject leftHandHeldObject;
    public GameObject rightHandHeldObject;

    float defaultArmLength;
    public bool hasObjectInLeftHand;
    public bool hasObjectInRightHand;

    void Start()
    {
        fpsCamera = Camera.main;
        defaultArmLength = leftArm.localScale.z;
        hasObjectInRightHand = false;
        hasObjectInLeftHand = false;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!hasObjectInLeftHand)
            {
                RaycastHit hit;

                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("PlayerBall") || hit.collider.gameObject.CompareTag("Door Key") || hit.collider.gameObject.CompareTag("Health Potion"))
                    {
                        StartCoroutine(StretchArm(hit.collider.gameObject, leftArm, true));
                        hasObjectInLeftHand = true;
                    }
                }
            }
            else if (hasObjectInLeftHand)
            {
                ThrowObject(leftHandHeldObject);
                hasObjectInLeftHand = false;
                leftHandHeldObject = null;
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (!hasObjectInRightHand)
            {
                RaycastHit hit;

                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag("PlayerBall") || hit.collider.gameObject.CompareTag("Health Potion"))
                    {
                        StartCoroutine(StretchArm(hit.collider.gameObject, rightArm, false));
                        hasObjectInRightHand = true;
                    }
                }
            }
            else if (hasObjectInRightHand)
            {
                ThrowObject(rightHandHeldObject);
                hasObjectInRightHand = false;
                rightHandHeldObject = null;
            }
        }
    }

    void ThrowObject(GameObject currentHandHeldGameObject)
    {
        currentHandHeldGameObject.transform.parent = null;

        if (currentHandHeldGameObject.GetComponent<HealthPotion>())
        {
            currentHandHeldGameObject.GetComponent<HealthPotion>().RestoreHealth();
        }
        else if (currentHandHeldGameObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody heldObjectRB = currentHandHeldGameObject.AddComponent<Rigidbody>();
            heldObjectRB.linearVelocity = Vector3.zero;
            heldObjectRB.mass = 1;
            heldObjectRB.linearDamping = 1;
            heldObjectRB.angularDamping = 2;
            heldObjectRB.AddForce(fpsCamera.transform.forward * throwForce, ForceMode.Impulse);
            heldObjectRB.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }
    }

    IEnumerator StretchArm(GameObject objectToGrab, Transform currentHand, bool isLeftHand)
    {
        float t = 0;
        float duration = 0.05f;

        float defaultLength = currentHand.localScale.z;
        float targetToGrab = Vector3.Distance(currentHand.position, objectToGrab.transform.position);

        currentHand.LookAt(objectToGrab.transform.position);

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float length = Mathf.Lerp(defaultLength, targetToGrab, t);
            currentHand.localScale = new Vector3(currentHand.localScale.x, currentHand.localScale.y, length);
            yield return null;
        }

        yield return StartCoroutine(RetractArm(currentHand, objectToGrab, isLeftHand));
    }

    IEnumerator RetractArm(Transform currentHand, GameObject objectToGrab, bool isLeftHand)
    {
        float t = 0;
        float duration = 0.05f;

        float currentArmLength = currentHand.localScale.z;
        float startingArmLength = defaultArmLength;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float length = Mathf.Lerp(currentArmLength, startingArmLength, t);
            currentHand.localScale = new Vector3(currentHand.localScale.x, currentHand.localScale.y, length);
            yield return null;
        }

        GrabObject(objectToGrab, isLeftHand);
    }

    void GrabObject(GameObject grabbedObject, bool isLeftHand)
    {
        if (isLeftHand)
        {
            leftHandHeldObject = grabbedObject;
            grabbedObject.transform.parent = leftHandObjectHoldTransform;
        }
        else
        {
            rightHandHeldObject = grabbedObject;
            grabbedObject.transform.parent = rightHandObjectHoldTransform;
        }

        Destroy(grabbedObject.GetComponent<Rigidbody>());
        grabbedObject.transform.localPosition = Vector3.zero;
        grabbedObject.transform.localRotation = Quaternion.identity;
    }
}
