using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{
    int shots = 0;
    int score = 0;

    public float holdingTime;
    public float maxHoldingTime;

    GUIStyle style;

	void Start() 
    {
        /* ������� ����� ������ */
        style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.red;
	}
	
    // ����������� ����������
    void OnGUI()
    {
        // ��������� ���������� ����� � ��������� 
        GUI.Label(new Rect(10, 10, 250, 20), "Shots: " + shots.ToString(), style);
        GUI.Label(new Rect(10, 30, 250, 20), "Score: " + score.ToString(), style);

        // ������ ��������� ���� ��������
        if (holdingTime != 0)
        {
            Texture2D lol = new Texture2D(1, 1);
            lol.SetPixel(0, 0, new Color(holdingTime / maxHoldingTime, 1 - (holdingTime / maxHoldingTime), 0));
            lol.Apply();
            float maxIndH = Screen.height - 40;
            GUI.DrawTexture(new Rect(Screen.width - 50, (Screen.height - 20) - maxIndH * (holdingTime / maxHoldingTime), 30, Screen.height), lol);
        }
    }

    /* ��������� ������ ���������� �� ������ ��������
     * ��� ������������� �������� ����� ���� */
    void SetShots(int shots)
    { this.shots = shots; }

    void SetScore(int score)
    { this.score = score; }

    void SetHoldingTime(float holdingTime)
    { this.holdingTime = holdingTime; }

    void SetMaxHoldingTime(float maxHoldingTime)
    { this.maxHoldingTime = maxHoldingTime; }
}
