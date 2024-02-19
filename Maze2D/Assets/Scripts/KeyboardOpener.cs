using System.Diagnostics; // ���������� ��� ������� ������� ���������
using UnityEngine;

public class KeyboardOpener : MonoBehaviour
{
    public void OpenSystemKeyboard()
    {
        // ������� ������� ��� ���������� ���������� ����� ��������� �����
        CloseSystemKeyboard();

        Process.Start("osk.exe"); // ��������� �������� ���������� Windows
    }

    private void CloseSystemKeyboard()
    {
        foreach (var proc in Process.GetProcessesByName("osk"))
        {
            proc.Kill(); // ��������� �������, ���� �� ��� �������
        }
    }
}