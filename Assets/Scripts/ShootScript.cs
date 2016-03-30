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
        // ���������� ������ � ������������ ������� ������� � ������ ��������� GUI
        if (guiController != null)
            guiController.SendMessage("SetMaxHoldingTime", maxHoldingTime, SendMessageOptions.DontRequireReceiver);
	}
	
	void Update() 
    {
        // ���� ������ ���� �� ����� ���� ������
        if (mouseHolding)
        {
            // �����, ������� ���� ������ ����, �� ������ �������������
            float time = ((Time.time - clickTime) < maxHoldingTime) ? (Time.time - clickTime) : maxHoldingTime;

            // ���� �� ���������, ���������� �������
            if (!(Input.GetKey(KeyCode.Mouse0)))
            {
                // ������� ��������� ���� �� ����� ���� �����
                GameObject bull_clone = (GameObject)Instantiate(bullet,
                    transform.position + transform.up.normalized * transform.localScale.y, transform.rotation);
                // ���������� �������� � ����� �����
                Physics.IgnoreCollision(bull_clone.collider, collider);
                // ������ ���� �������
                bull_clone.rigidbody.AddForce(transform.up * (maxBulletImpulse * (time / maxHoldingTime)), ForceMode.Impulse);
                // �������������� ��������� ��� ���������� ��������
                lastShotTime = Time.time;
                mouseHolding = false;
                // �������������� ����� ���������
                if (gameController != null)
                    gameController.SendMessage("IncShots", SendMessageOptions.DontRequireReceiver);
                time = 0;
            }
            // ���������� ������ � ������� ������� � ������ ��������� GUI
            if (guiController != null)
                guiController.SendMessage("SetHoldingTime", time, SendMessageOptions.DontRequireReceiver);
        }
        //���� ������ ������ �� ����
        else
        {
            //���� ������ ������ ������
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //���������, ������ �� ����������� ����� ����� ����������
                if (Time.time > (lastShotTime + shootSpeed))
                {
                    mouseHolding = true;
                    clickTime = Time.time;
                }
            }
        }
	}
}
