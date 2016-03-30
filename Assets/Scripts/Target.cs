using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
    public GameObject explosion;

    GameObject gameController;
    GameObject targetController;

    public float minTimeut = 3f;
    public float maxTimeut = 10f;

    float creationTime;
    float timeout;

    /* Используется для выдвигания/задвигания мишеней
     * с одинаковой скоростью вне зависимости от FPS */
    float lastUpdate;

    bool isVisible = true;

	void Start() 
    {
        creationTime = Time.time;
        //Время исчезновения мишени - случайное число в заданном диапазоне
        timeout = Random.Range(minTimeut, maxTimeut);

        gameController = GameObject.Find("GameController");
        targetController = GameObject.Find("TargetController");
	}
	
	void Update() 
    {
        if (lastUpdate == 0f)
            lastUpdate = Time.time;

        //По истечени таймаута убрать мишень
        if (Time.time - creationTime >= timeout)
            Hide();

        //Выдвигаем или задвигаем мишени в зависимости от isVisible
        if (isVisible)
        {
            if (transform.position.y < transform.localScale.x / 2)
                transform.position += new Vector3(0, 10f * (Time.time - lastUpdate), 0);
        }
        else
        {
            if (transform.position.y > -transform.localScale.x / 2)
                transform.position -= new Vector3(0, 10f * (Time.time - lastUpdate), 0);
            else
            {
                if (targetController != null)
                    targetController.SendMessage("DecTargetCount", SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
        lastUpdate = Time.time;
	}

    void Hide()
    {
        isVisible = false;
    }

    void Destruct()
    {
        //Создаем эффект взрыва
        Instantiate(explosion, transform.position - transform.up.normalized * (transform.localScale.y / 2), transform.rotation);
        //Синхронизируем параметры с объектом GameController
        if (gameController != null)
            gameController.SendMessage("SetScore", (int)Vector3.Distance(transform.position, GameObject.Find("Cannon").transform.position),
                SendMessageOptions.DontRequireReceiver);
        if (targetController != null)
            targetController.SendMessage("DecTargetCount", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}