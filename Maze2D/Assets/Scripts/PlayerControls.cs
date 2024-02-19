using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 2;

    private Rigidbody2D componentRigidbody;
    private Camera mainCamera; // Камера для преобразования позиции касания в координаты мира
    public GameManager gameManager;
    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Предполагается, что у вас есть основная камера на сцене
    }
    private void Update()
    {
        if (GameManager.Instance.gameHasEnded || Time.timeScale == 0)
        {
            componentRigidbody.velocity = Vector2.zero;
            return;
        }

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR // Для ПК или в редакторе Unity
        // Обработка ввода мыши
        if (Input.GetMouseButton(0)) // Если нажата левая кнопка мыши
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Перемещаем объект в направлении позиции мыши
            Vector2 direction = mousePosition - (Vector2)transform.position;
            direction.Normalize();

            componentRigidbody.velocity = direction * Speed;
        }
        else if (Input.GetMouseButtonUp(0)) // Если кнопка мыши отпущена
        {
            componentRigidbody.velocity = Vector2.zero;
        }
#else // Для мобильных устройств
    // Обработка ввода сенсорного экрана
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
    //    // Проверяем, есть ли касание
    //    if (Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);
    //        Vector2 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

    //        // Перемещаем шарик в направлении позиции касания
    //        Vector2 direction = touchPosition - (Vector2)transform.position;
    //        direction.Normalize(); // Нормализуем вектор, чтобы получить только направление

    //        componentRigidbody.velocity = direction * Speed;

    //        // Если палец убран, останавливаем шарик
    //        if (touch.phase == TouchPhase.Ended)
    //        {
    //            componentRigidbody.velocity = Vector2.zero;
    //        }
    //    }
    //    else
    //    {
    //        // Если нет касаний, останавливаем шарик
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
        Debug.Log("Вы выиграли!");
        gameManager.PlayerWon();
    }
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
    }
   
}
