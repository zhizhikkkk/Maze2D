using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody2D componentRigidbody;
    private Camera mainCamera; // ������ ��� �������������� ������� ������� � ���������� ����

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ��������������, ��� � ��� ���� �������� ������ �� �����
    }

    private void Update()
    {
        // ���������, ���� �� �������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            // ���������� ����� � ����������� ������� �������
            Vector2 direction = touchPosition - (Vector2)transform.position;
            direction.Normalize(); // ����������� ������, ����� �������� ������ �����������

            componentRigidbody.velocity = direction * Speed;

            // ���� ����� �����, ������������� �����
            if (touch.phase == TouchPhase.Ended)
            {
                componentRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            // ���� ��� �������, ������������� �����
            componentRigidbody.velocity = Vector2.zero;
        }
    }
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

    }

    #region �������� ���������
    //public float Speed = 2;

    //private Rigidbody2D componentRigidbody;

    //private void Start()
    //{
    //    componentRigidbody = GetComponent<Rigidbody2D>();
    //}

    //private void Update()
    //{
    //    componentRigidbody.velocity = Vector2.zero;
    //    if (Input.GetKey(KeyCode.LeftArrow)) componentRigidbody.velocity += Vector2.left * Speed;
    //    if (Input.GetKey(KeyCode.RightArrow)) componentRigidbody.velocity += Vector2.right * Speed;
    //    if (Input.GetKey(KeyCode.UpArrow)) componentRigidbody.velocity += Vector2.up * Speed;
    //    if (Input.GetKey(KeyCode.DownArrow)) componentRigidbody.velocity += Vector2.down * Speed;
    //}
    #endregion

}
