using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    /* Игровой счет */
    public int shots = 0;
    int score = 0;

    GameObject guiController;

	void Start() 
    {
        Screen.showCursor = false;
        guiController = GameObject.Find("GUIController");
	}

    /* Инкрементирование счетчика выстрелов
     * Метод вызывается из других объектов */
    void IncShots()
    {
        shots++;
        if (guiController != null)
            guiController.SendMessage("SetShots", shots, SendMessageOptions.DontRequireReceiver);
    }

    /* Установление количества очков
     * Метод вызывается из других объектов */
    void SetScore(int score)
    {
        this.score += score;
        if (guiController != null)
            guiController.SendMessage("SetScore", this.score, SendMessageOptions.DontRequireReceiver);
    }
}
