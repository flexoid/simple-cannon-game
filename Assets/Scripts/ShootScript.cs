using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour
{
    private float lastShotTime;
    private float clickTime;
    private bool mouseHolding;
    public float maxBulletImpulse = 300f;
    public float maxHoldingTime = 5f;
    public float shootSpeed = 1f;

    public GameObject bullet;

    GameObject gameController;
    GameObject guiController;

	void Start()
    {
        lastShotTime = 0.0f;
        clickTime = 0.0f;
        mouseHolding = false;
        gameController = GameObject.Find("GameController");
        guiController = GameObject.Find("GUIController");
        // Отправляем данные о максимальном времени нажатия в скрипт отрисовки GUI
        if (guiController != null)
            guiController.SendMessage("SetMaxHoldingTime", maxHoldingTime, SendMessageOptions.DontRequireReceiver);
	}

	void Update()
    {
        // Если кнопка мыши до этого была нажата
        if (mouseHolding)
        {
            // Время, которое была зажата мышь, не больше максимального
            float time = ((Time.time - clickTime) < maxHoldingTime) ? (Time.time - clickTime) : maxHoldingTime;

            // Если ее отпустили, производим выстрел
            if (!(Input.GetKey(KeyCode.Mouse0)))
            {
                // Создаем экземпляр ядра на конце дула пушки
                GameObject bull_clone = (GameObject)Instantiate(bullet,
                    transform.position + transform.up.normalized * transform.localScale.y, transform.rotation);
                // Игнорируем коллизии с дулом пушки
                Physics.IgnoreCollision(bull_clone.collider, collider);
                // Задаем силу объекту
                bull_clone.rigidbody.AddForce(transform.up * (maxBulletImpulse * (time / maxHoldingTime)), ForceMode.Impulse);
                // Подготавливаем параметры для следующего выстрела
                lastShotTime = Time.time;
                mouseHolding = false;
                // Инкрементируем число выстрелов
                if (gameController != null)
                    gameController.SendMessage("IncShots", SendMessageOptions.DontRequireReceiver);
                time = 0;
            }
            // Отправляем данные о времени нажатия в скрипт отрисовки GUI
            if (guiController != null)
                guiController.SendMessage("SetHoldingTime", time, SendMessageOptions.DontRequireReceiver);
        }
        //Если кнопка зажата не была
        else
        {
            //Если кнопка нажата сейчас
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Проверяем, прошло ли минимальное время между выстрелами
                if (Time.time > (lastShotTime + shootSpeed))
                {
                    mouseHolding = true;
                    clickTime = Time.time;
                }
            }
        }
	}
}
