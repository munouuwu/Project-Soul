using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    Button WitchButton;
    [SerializeField]
    Button ChosenButton;
    [SerializeField]
    Button ExitButton;

    private void Start()
    {
        WitchButton.onClick.AddListener(PlayAsWitch);
        ChosenButton.onClick.AddListener(PlayAsChosen);
        ExitButton.onClick.AddListener(Exit);
    }
    void PlayAsWitch()
    {
        SceneManager.LoadScene(2);
    }

    void PlayAsChosen()
    {
        SceneManager.LoadScene(1);
    }

    void Exit()
    {
        Application.Quit();
    }
}
