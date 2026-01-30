using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour 
{
    public void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    
}
