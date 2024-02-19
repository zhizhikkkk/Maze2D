using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody2D componentRigidbody;
    private Camera mainCamera; // ������ ��� �������������� ������� ������� � ���������� ����
    public GameManager gameManager;
    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ��������������, ��� � ��� ���� �������� ������ �� �����
    }
    private void Update()
    {
        if (GameManager.Instance.gameHasEnded || Time.timeScale == 0)
        {
            componentRigidbody.velocity = Vector2.zero;
            return;
        }

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR // ��� �� ��� � ��������� Unity
        // ��������� ����� ����
        if (Input.GetMouseButton(0)) // ���� ������ ����� ������ ����
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // ���������� ������ � ����������� ������� ����
            Vector2 direction = mousePosition - (Vector2)transform.position;
            direction.Normalize();

            componentRigidbody.velocity = direction * Speed;
        }
        else if (Input.GetMouseButtonUp(0)) // ���� ������ ���� ��������
        {
            componentRigidbody.velocity = Vector2.zero;
        }
#else // ��� ��������� ���������
    // ��������� ����� ���������� ������
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

        Vector2 direction = touchPosition - (Vector2)transform.position;
        direction.Normalize();

        componentRigidbody.velocity = direction * Speed;

        if (touch.phase == TouchPhase.Ended)
        {
            componentRigidbody.velocity = Vector2.zero;
        }
    }
    else
    {
        componentRigidbody.velocity = Vector2.zero;
    }
#endif
    }
    //private void Update()
    //{
    //    if (GameManager.Instance.gameHasEnded || Time.timeScale == 0)
    //    {
    //        componentRigidbody.velocity = Vector2.zero;
    //        return;
    //    }
    //    // ���������, ���� �� �������
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

    //        // ���������� ����� � ����������� ������� �������
    //        Vector2 direction = touchPosition - (Vector2)transform.position;
    //        direction.Normalize(); // ����������� ������, ����� �������� ������ �����������

    //        componentRigidbody.velocity = direction * Speed;

    //        // ���� ����� �����, ������������� �����
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            componentRigidbody.velocity = Vector2.zero;
    //        }
    //    }
    //    else
    //    {
    //        // ���� ��� �������, ������������� �����
    //        componentRigidbody.velocity = Vector2.zero;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Exit"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("�� ��������!");
        gameManager.PlayerWon();
    }
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
    }
   
}
