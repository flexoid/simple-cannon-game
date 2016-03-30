using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour 
{
    public GameObject target;

    /* ������������ ���������� �������, �������
     * ����� ���������� �� ���� ������������ */
    public float maxTargets = 5;

    //������ ��������� ��� �������� Terrain
    float startY = -5;
    //������� ������� ���������� �� ���� � ������ ������
    int targets = 0;

	void Update() 
    {
        //������������ ���������� ������� �� ���� �� ������ �������������
        if (targets < maxTargets)
        {
            //��������� ����� ������ � �������� ����� � �������� ��������� ��������������
            Instantiate(target, new Vector3(Random.Range(210F, 340F), startY, Random.Range(140F, 255F)), target.transform.rotation);
            targets++;
        }
	}

    //����� ���������� �� ������ �������� ��� �������������
    void DecTargetCount()
    { targets--; }
}
