using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour 
{
    public float disappearTime = 60f; //�����, ����� ������� ���������� ���� �����������

    void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //���������� ����� ���������� ������, � ������� ��������� ������������
        collision.gameObject.SendMessage("Destruct", SendMessageOptions.DontRequireReceiver);
    }
}
