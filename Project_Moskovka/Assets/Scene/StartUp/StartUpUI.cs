using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpUI : MonoBehaviour
{
    /// <summary>
    /// Обеспечивает логику поддержки любого пользовательского интерфейса перед этапом главного меню.
    /// В основном мы просто загружаем главное меню.
    /// </summary>
    void Start()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
