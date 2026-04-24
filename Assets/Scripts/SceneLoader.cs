using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Array of scene names that can be loaded. Index 0 = Scene1, Index 1 = Scene2, etc.")]
    private string[] m_SceneNames = new string[]
    {
        "Game2Scene",
        "PriceIsRightScene",
        "DemoScene"
    };

    void Start()
    {
        Debug.Log($"SceneLoader: Initialized with {m_SceneNames.Length} scenes available");
        for (int i = 0; i < m_SceneNames.Length; i++)
        {
            Debug.Log($"  Scene {i + 1}: {m_SceneNames[i]}");
        }
    }

    /// <summary>
    /// Loads a scene by index
    /// </summary>
    /// <param name="sceneIndex">Index of the scene (0-based). Scene 1 = index 0, Scene 2 = index 1, etc.</param>
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= m_SceneNames.Length)
        {
            Debug.LogError($"SceneLoader: Invalid scene index {sceneIndex}. Valid range is 0-{m_SceneNames.Length - 1}");
            return;
        }

        string sceneName = m_SceneNames[sceneIndex];
        Debug.Log($"SceneLoader: Loading scene '{sceneName}' (index {sceneIndex})");
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads scene 1
    /// </summary>
    public void LoadScene1()
    {
        LoadScene(0);
    }

    /// <summary>
    /// Loads scene 2
    /// </summary>
    public void LoadScene2()
    {
        LoadScene(1);
    }

    /// <summary>
    /// Loads scene 3
    /// </summary>
    public void LoadScene3()
    {
        LoadScene(2);
    }

    /// <summary>
    /// Sets the scene names array (for dynamic configuration)
    /// </summary>
    public void SetSceneNames(string[] sceneNames)
    {
        if (sceneNames != null && sceneNames.Length > 0)
        {
            m_SceneNames = sceneNames;
            Debug.Log($"SceneLoader: Scene names updated to {m_SceneNames.Length} scenes");
        }
        else
        {
            Debug.LogWarning("SceneLoader: Cannot set empty or null scene names array");
        }
    }

    /// <summary>
    /// Gets the current scene name
    /// </summary>
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Gets the number of available scenes
    /// </summary>
    public int GetSceneCount()
    {
        return m_SceneNames.Length;
    }
}
