using UnityEngine;

public class Lerper : MonoBehaviour
{
    [SerializeField] Transform entity;
    [SerializeField] Transform positionOne;
    [SerializeField] Transform positionTwo;
    [SerializeField] float speed;

    float T = 0;
    bool canLerp = false;

    void Update()
    {
        if (canLerp)
        {
            LerpObject();
        }
    }

    void LerpObject()
    {
        T += speed * Time.deltaTime;
        T = Mathf.Clamp01(T);
        entity.position = Vector3.Lerp(positionOne.position, positionTwo.position, T);
    }

    public void SetCanLerp(bool _canLerp)
    {
        canLerp = _canLerp;
    }
}
