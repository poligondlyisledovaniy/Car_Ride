using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Ссылки")]
    public Transform player;
    public TextMeshProUGUI speedText;
    public GameObject finishMessage;   
    public TextMeshProUGUI timeText;

    private float timer = 0f;
    private Vector3 lastPos;
    private bool finished = false;

    void Start()
    {
        if (player != null)
            lastPos = player.position;
    }

    void Update()
    {
        // === РЕСТАРТ (клавиша R) ===
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            return; // выходим, чтобы остальной код не выполнялся
        }

        if (finished) return;

        // === ТАЙМЕР ===
        timer += Time.deltaTime;
        if (timeText != null)
            timeText.text = $"Время: {timer:F1} с";

        // === СКОРОСТЬ ===
        if (speedText != null && player != null)
        {
            float speedMs = Vector3.Distance(player.position, lastPos) / Time.deltaTime;
            float speedKmh = speedMs * 3.6f;
            speedText.text = $"Скорость: {speedKmh:F0} км/ч";
            lastPos = player.position;
        }

        // === ПРОВЕРКА ФИНИША ===
        // === ФИНИШ ===
        if (player != null && player.position.z >= 240f)
        {
            finished = true;

            if (timeText != null)
                timeText.text = $"Время: {timer:F1} с";

            if (speedText != null)
                speedText.text = $"Скорость: 0 км/ч";

            if (finishMessage != null)
            {
                finishMessage.SetActive(true);   // включаем надпись

                // записываем время в текст
                TextMeshProUGUI finishText = finishMessage.GetComponent<TextMeshProUGUI>();
                if (finishText != null)
                    finishText.text = $"ФИНИШ!\n{timer:F1} с";
            }
        }
    }


    public void StopTimer()
    {
        finished = true;
    }

    public float GetTimer()
    {
        return timer;
    }
}