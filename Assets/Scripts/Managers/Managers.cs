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
    //SceneManagerEx _scene = new SceneManagerEx();
    SceneManagerAddressable _scene = null;
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource {  get { return Instance._resource; } }
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

            GameObject sceneManagerGO = new GameObject("@SceneManagerAddressable");
            sceneManagerGO.transform.parent = go.transform;
            s_instance._scene = sceneManagerGO.GetOrAddComponent<SceneManagerAddressable>();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();

        Pool.Clear(); //다른 매니저에서 풀링된 오브젝트를 사용할 수도 있으므로 PoolManager는 항상 마지막에 Clear해준다.
    }
}
