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

    /* ������������ ��� ����������/���������� �������
     * � ���������� ��������� ��� ����������� �� FPS */
    float lastUpdate;

    bool isVisible = true;

	void Start() 
    {
        creationTime = Time.time;
        //����� ������������ ������ - ��������� ����� � �������� ���������
        timeout = Random.Range(minTimeut, maxTimeut);

        gameController = GameObject.Find("GameController");
        targetController = GameObject.Find("TargetController");
	}
	
	void Update() 
    {
        if (lastUpdate == 0f)
            lastUpdate = Time.time;

        //�� �������� �������� ������ ������
        if (Time.time - creationTime >= timeout)
            Hide();

        //��������� ��� ��������� ������ � ����������� �� isVisible
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
        //������� ������ ������
        Instantiate(explosion, transform.position - transform.up.normalized * (transform.localScale.y / 2), transform.rotation);
        //�������������� ��������� � �������� GameController
        if (gameController != null)
            gameController.SendMessage("SetScore", (int)Vector3.Distance(transform.position, GameObject.Find("Cannon").transform.position),
                SendMessageOptions.DontRequireReceiver);
        if (targetController != null)
            targetController.SendMessage("DecTargetCount", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}