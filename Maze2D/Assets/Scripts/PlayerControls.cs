using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody2D componentRigidbody;
    private Camera mainCamera; // Камера для преобразования позиции касания в координаты мира

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Предполагается, что у вас есть основная камера на сцене
    }

    private void Update()
    {
        // Проверяем, есть ли касание
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            // Перемещаем шарик в направлении позиции касания
            Vector2 direction = touchPosition - (Vector2)transform.position;
            direction.Normalize(); // Нормализуем вектор, чтобы получить только направление

            componentRigidbody.velocity = direction * Speed;

            // Если палец убран, останавливаем шарик
            if (touch.phase == TouchPhase.Ended)
            {
                componentRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            // Если нет касаний, останавливаем шарик
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
        Debug.Log("Вы выиграли!");

    }
    public void ResetPlayer()
    {
        // Сброс позиции игрока
        transform.position = Vector3.zero;
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        // Сброс скорости, чтобы шарик остановился
        componentRigidbody.velocity = Vector2.zero;
    }
}
