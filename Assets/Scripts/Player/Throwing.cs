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
        else
        {
            Rigidbody heldObjectRB = currentHandHeldGameObject.GetComponent<Rigidbody>();
            heldObjectRB.useGravity = true;
            heldObjectRB.isKinematic = false;
            heldObjectRB.AddForce(fpsCamera.transform.forward * throwForce, ForceMode.Impulse);
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

        if (grabbedObject.GetComponent<Rigidbody>())
        {
            Rigidbody heldObjectRB = grabbedObject.GetComponent<Rigidbody>();
            heldObjectRB.useGravity = false;
            heldObjectRB.isKinematic = true;
        }

        grabbedObject.transform.localPosition = Vector3.zero;
        grabbedObject.transform.localRotation = Quaternion.identity;
    }
}
