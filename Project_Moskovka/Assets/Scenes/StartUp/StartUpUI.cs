using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Обеспечивает логику поддержки любого пользовательского интерфейса перед этапом главного меню.
/// В основном мы просто загружаем главное меню.
/// </summary>
public class StartUpUI : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
