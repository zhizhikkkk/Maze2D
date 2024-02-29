using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IdleScreenManager : MonoBehaviour
{
    public GameObject idlePanel; // Перетащите вашу панель UI сюда через редактор Unity
    private float idleTime = 80f; // Время в секундах, после которого появится заставка
    private float timer; // Таймер для отслеживания времени бездействия

    void Update()
    {
        // Если произошло любое касание или нажатие клавиши, сбросьте таймер
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            if (idlePanel.activeInHierarchy) // Если панель уже показывается, скройте её при касании
            {
                idlePanel.SetActive(false);
            }
            timer = 0;
        }
        else
        {
            // Увеличивайте таймер, пока не будет достигнуто время бездействия
            timer += Time.deltaTime;

            if (timer >= idleTime)
            {
                // Если таймер достиг предела и панель ещё не активна, показать панель
                if (!idlePanel.activeInHierarchy)
                {
                    idlePanel.SetActive(true);
                }
            }
        }
    }
}
