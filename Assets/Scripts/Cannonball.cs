using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour 
{
    public float disappearTime = 60f; //¬рем€, через которое выпущенное €дро уничтожитс€

    void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //¬ызываетс€ метод разрушени€ мишени, с которой произошло столкновение
        collision.gameObject.SendMessage("Destruct", SendMessageOptions.DontRequireReceiver);
    }
}
