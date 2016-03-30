using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour
{
    public float disappearTime = 60f; //Время, через которое выпущенное ядро уничтожится

    void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Вызывается метод разрушения мишени, с которой произошло столкновение
        collision.gameObject.SendMessage("Destruct", SendMessageOptions.DontRequireReceiver);
    }
}
