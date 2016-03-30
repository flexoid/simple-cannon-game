using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    /* ������� ���� */
    public int shots = 0;
    int score = 0;

    GameObject guiController;

	void Start() 
    {
        Screen.showCursor = false;
        guiController = GameObject.Find("GUIController");
	}

    /* ����������������� �������� ���������
     * ����� ���������� �� ������ �������� */
    void IncShots()
    {
        shots++;
        if (guiController != null)
            guiController.SendMessage("SetShots", shots, SendMessageOptions.DontRequireReceiver);
    }

    /* ������������ ���������� �����
     * ����� ���������� �� ������ �������� */
    void SetScore(int score)
    {
        this.score += score;
        if (guiController != null)
            guiController.SendMessage("SetScore", this.score, SendMessageOptions.DontRequireReceiver);
    }
}
