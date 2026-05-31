using UnityEngine;

public class CarControl : MonoBehaviour
{
    [Header("Car Settings")]
    public float lateralSpeed = 15f; 
    public float inertia = 7f;      
    public float leftBound = -1.2f;
    public float rightBound = 6.7f;

    private float currentLateralSpeed;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > 0.01f)
        {
            currentLateralSpeed += input * lateralSpeed * Time.deltaTime;
        }
        else
        {
            currentLateralSpeed = Mathf.Lerp(currentLateralSpeed, 0, inertia * Time.deltaTime);
        }

        currentLateralSpeed = Mathf.Clamp(currentLateralSpeed, -lateralSpeed, lateralSpeed);

        transform.Translate(currentLateralSpeed * Time.deltaTime, 0, 0);

        float x = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
