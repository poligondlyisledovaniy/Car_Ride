using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Скорость")]
    public float speed = 15f;          // м/с вперёд
    public float reverseSpeed = 6f;    // м/с назад

    [Header("Поворот")]
    public float turnSpeed = 90f;      // градусов в секунду

    void Update()
    {
        // === ПОВОРОТ (A / D) ===
        float turn = 0f;
        if (Input.GetKey(KeyCode.A)) turn -= 1f;
        if (Input.GetKey(KeyCode.D)) turn += 1f;

        // руль работает только когда едешь 
        bool moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        if (moving)
            transform.Rotate(0f, turn * turnSpeed * Time.deltaTime, 0f);

        // === ДВИЖЕНИЕ (W / S) ===
        float drive = 0f;
        if (Input.GetKey(KeyCode.W)) drive = 1f;
        if (Input.GetKey(KeyCode.S)) drive = -1f;

        float currentSpeed = (drive > 0) ? speed : reverseSpeed;
        transform.position += transform.forward * drive * currentSpeed * Time.deltaTime;
    }
}