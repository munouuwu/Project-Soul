using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalSceneHub : MonoBehaviour
{
    
    public void GOTOSCENE(int index)
    {
        SceneManager.LoadScene(index);
    }
}
