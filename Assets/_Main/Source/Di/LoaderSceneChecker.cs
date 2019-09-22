using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderSceneChecker : MonoBehaviour
{
    [SerializeField] 
    private bool isCurrentSceneLoader;

    public static bool wasLoaderSceneLoaded { get; private set; }
    
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
