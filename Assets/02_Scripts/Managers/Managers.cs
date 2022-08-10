using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game; } }
    #endregion


    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    ResourceManagerAddressable _resourceA = new ResourceManagerAddressable();
    //SceneManagerEx _scene = new SceneManagerEx();
    SceneManagerAddressable _scene = null;
    SoundManager _sound = new SoundManager();
    //UIManager _ui = new UIManager();
    UIManager _ui = null;
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource {  get { return Instance._resource; } }
    public static ResourceManagerAddressable ResourceA { get { return Instance._resourceA; } }
    //public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SceneManagerAddressable Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();

            Transform sceneManagerTransform = s_instance.transform.Find("@SceneManagerAddressable");
            if (sceneManagerTransform == null)
            {
                sceneManagerTransform = new GameObject("@SceneManagerAddressable").transform;
            }
            sceneManagerTransform.parent = go.transform;
            s_instance._scene = sceneManagerTransform.gameObject.GetOrAddComponent<SceneManagerAddressable>();

            Transform uiManagerTransform = s_instance.transform.Find("@UIManager");
            if (uiManagerTransform == null)
            {
                uiManagerTransform = new GameObject("@UIManager").transform;
            }
            uiManagerTransform.parent = go.transform;
            s_instance._ui = uiManagerTransform.gameObject.GetOrAddComponent<UIManager>();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();

        Pool.Clear(); //다른 매니저에서 풀링된 오브젝트를 사용할 수도 있으므로 PoolManager는 항상 마지막에 Clear해준다.
        ResourceA.Clear();
    }
}
