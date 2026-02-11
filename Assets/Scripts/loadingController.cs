
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadingController : MonoBehaviour
{
  
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressText;

    // cấu hình 
    [SerializeField] private float minLoadTime = 1.5f; // thời gian tải tối thiểu

    // dùng cho scene nào
    private static string sceneToLoad;

    public static void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("Loading");

    }
    void Start()
    {
        if(string.IsNullOrEmpty(sceneToLoad))
        {
            sceneToLoad = "StartGame";
        }
        StartCoroutine(loadTargetScene());
    }

   private IEnumerator loadTargetScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(operation.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float fakeProgress = Mathf.Clamp01(timer / minLoadTime);
            float displayedProgress = Mathf.Min(realProgress, fakeProgress);   

            if(progressBar != null)
            {
                progressBar.value = displayedProgress;
            }
            if(progressText != null)
            {
                progressText.text =$"{Mathf.RoundToInt(displayedProgress * 100)}%";
            }

            yield return null;
        }
        if(progressBar != null)
        {
            progressBar.value = 1f;
        }

        if(progressText != null)
        {
            progressText.text = "100%";
        }

        yield return new WaitForSeconds(0.2f);
        operation.allowSceneActivation = true;

    }
}
