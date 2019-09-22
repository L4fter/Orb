using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneChecker : MonoBehaviour
{
    [SerializeField] 
    private bool isCurrentSceneLoader;
    
    private static bool wasLoaderSceneLoaded;
    
    private void Awake()
    {
        if (isCurrentSceneLoader)
        {
            wasLoaderSceneLoaded = true;
        }
        
        if (!wasLoaderSceneLoaded)
        {
            SceneManager.LoadScene("Loader");
        }
        
        Destroy(this.gameObject);
    }
}
