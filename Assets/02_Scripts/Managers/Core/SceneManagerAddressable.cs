using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManagerAddressable : MonoBehaviour
{
    public Action<float> SceneLoadProgress = null;

    private Dictionary<string, AsyncOperationHandle<SceneInstance>> _loadedScene = new System.Collections.Generic.Dictionary<string, AsyncOperationHandle<SceneInstance>>();

    private Define.Scene PrevSceneType { get; set; } = Define.Scene.Unknown;
    public Define.Scene NextSceneType { get; set; } = Define.Scene.Unknown;
    public BaseScene CurrentScene { get; set; } = null;
    public Color FadeColor { get; set; } = Color.black;

    public void Clear()
    {
        CurrentScene.Clear();
    }

    public void SceneLoad(Define.Scene nextSceneType, bool useLoadingScene = false)
    {
        if (NextSceneType == nextSceneType)
            return;

        
        if (useLoadingScene)
        {
            StartCoroutine(SceneLoadWithLoadingProcess(nextSceneType));
        }
        else
        {
            StartCoroutine(Co_SceneLoadProcess(nextSceneType));
        }
    }

    IEnumerator SceneLoadWithLoadingProcess(Define.Scene nextSceneType)
    {
        yield return StartCoroutine(Co_SceneLoadProcess(Define.Scene.Loading));
        yield return StartCoroutine(Co_SceneLoadProcess(nextSceneType));
    }

    IEnumerator Co_SceneLoadProcess(Define.Scene nextSceneType)
    {
        //A 씬 : 언로드 할 씬 (현재 씬)
        //B 씬 : 로드 할 씬
        
        
        Scene a_Scene = SceneManager.GetActiveScene();
        BaseScene a_BaseScene = GameObject.FindObjectOfType<BaseScene>();

        //A씬 페이드 아웃
        Debug.Log("A씬 페이드 아웃");
        yield return StartCoroutine(a_BaseScene.Co_FadeOut(FadeColor));

        //다음 씬 로드
        Debug.Log("B씬 로드");
        string b_SceneKey = System.Enum.GetName(typeof(Define.Scene), nextSceneType); //씬이름 = Key
        bool isLoaded = false;
        bool isFailed = false;
        AsyncOperationHandle<SceneInstance> loadSceneAO = Addressables.LoadSceneAsync(b_SceneKey, LoadSceneMode.Additive, false);
        loadSceneAO.Completed += (result) =>
        {
            if (result.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedScene.Add(b_SceneKey, loadSceneAO);
                isLoaded = true;
                isFailed = false;
            }
            else if (result.Status == AsyncOperationStatus.Failed)
            {
                isLoaded = true;
                isFailed = true;
            }
        };

        while (isLoaded == false)
        {
            if (SceneLoadProgress != null)
            {
                if (loadSceneAO.GetDownloadStatus().IsDone)
                    SceneLoadProgress.Invoke(loadSceneAO.GetDownloadStatus().Percent);
                else
                    SceneLoadProgress.Invoke(loadSceneAO.PercentComplete);
            }
            yield return null;
        }
        SceneLoadProgress?.Invoke(1f);
        //yield return new WaitUntil(() => isLoaded == true);

        if (isFailed)
        {
            Debug.LogError(loadSceneAO.OperationException.Message);
            yield break;
        }

        Debug.Log("활성화 씬 변경");
        yield return loadSceneAO.Result.ActivateAsync();
        Scene b_Scene = loadSceneAO.Result.Scene;
        SceneManager.SetActiveScene(b_Scene);
        DynamicGI.UpdateEnvironment();

        // 씬 전환 중 처리 (이전씬 및 풀링 등의 정리)
        Managers.Clear();

        GameObject[] goList = a_Scene.GetRootGameObjects();
        for (int i = 0; i < goList.Length; i++)
        {
            Destroy(goList[i]);
        }

        yield return new WaitForEndOfFrame();

        //다음 씬 BaseScene 설정
        CurrentScene = b_Scene.GetRootGameObjects().Single(x => x.CompareTag("Scene")).GetComponent<BaseScene>();
        yield return new WaitForEndOfFrame();

        // A 씬 언로드
        Debug.Log("A 씬 언로드");
        string prevSceneKey = System.Enum.GetName(typeof(Define.Scene), a_BaseScene.SceneType); //씬이름 = Key
        if (_loadedScene.ContainsKey(prevSceneKey))
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(_loadedScene[prevSceneKey]);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {
                    _loadedScene.Remove(prevSceneKey);
                    Addressables.Release(handle);
                    Addressables.ClearDependencyCacheAsync(prevSceneKey);
                }
            };
            yield return handle;
        }
        else
        {
            yield return SceneManager.UnloadSceneAsync(a_Scene);
        }

        // 씬 초기화
        Debug.Log("B 씬 초기화");
        yield return StartCoroutine(CurrentScene.Co_InitAsync());

        yield return new WaitForEndOfFrame();
        //씬 페이드 인
        Debug.Log("B 씬 페이드 인");
        yield return StartCoroutine(CurrentScene.Co_FadeIn(FadeColor));

    }
}
