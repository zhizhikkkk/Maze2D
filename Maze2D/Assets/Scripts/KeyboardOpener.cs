using System.Diagnostics; // Необходимо для запуска внешних процессов
using UnityEngine;

public class KeyboardOpener : MonoBehaviour
{
    public void OpenSystemKeyboard()
    {
        // Попытка закрыть уже запущенную клавиатуру перед открытием новой
        CloseSystemKeyboard();

        Process.Start("osk.exe"); // Запускаем экранную клавиатуру Windows
    }

    private void CloseSystemKeyboard()
    {
        foreach (var proc in Process.GetProcessesByName("osk"))
        {
            proc.Kill(); // Закрываем процесс, если он уже запущен
        }
    }
}