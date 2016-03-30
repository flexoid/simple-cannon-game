using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour 
{
    public GameObject target;

    /* Максимальное количество мишеней, которое
     * может находиться на поле одновременно */
    public float maxTargets = 5;

    //Мишени создаются под объектом Terrain
    float startY = -5;
    //Сколько мишеней находиться на поле в данный момент
    int targets = 0;

	void Update() 
    {
        //Поддерживать количество мишеней на поле на уровне максимального
        if (targets < maxTargets)
        {
            //Создается новая мишень в случаном месте в пределах заданного прямоугольника
            Instantiate(target, new Vector3(Random.Range(210F, 340F), startY, Random.Range(140F, 255F)), target.transform.rotation);
            targets++;
        }
	}

    //Метод выхывается из других объектов для синхронизации
    void DecTargetCount()
    { targets--; }
}
