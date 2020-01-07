using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("Demo");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
