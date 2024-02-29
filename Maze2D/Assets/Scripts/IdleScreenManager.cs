using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IdleScreenManager : MonoBehaviour
{
    public GameObject idlePanel; // ���������� ���� ������ UI ���� ����� �������� Unity
    private float idleTime = 80f; // ����� � ��������, ����� �������� �������� ��������
    private float timer; // ������ ��� ������������ ������� �����������

    void Update()
    {
        // ���� ��������� ����� ������� ��� ������� �������, �������� ������
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            if (idlePanel.activeInHierarchy) // ���� ������ ��� ������������, ������� � ��� �������
            {
                idlePanel.SetActive(false);
            }
            timer = 0;
        }
        else
        {
            // ������������ ������, ���� �� ����� ���������� ����� �����������
            timer += Time.deltaTime;

            if (timer >= idleTime)
            {
                // ���� ������ ������ ������� � ������ ��� �� �������, �������� ������
                if (!idlePanel.activeInHierarchy)
                {
                    idlePanel.SetActive(true);
                }
            }
        }
    }
}
