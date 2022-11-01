namespace PixelLife;

internal class SceneManeger
{
    private SceneBase? scene;
    private bool isInit;
    private readonly Dictionary<string, SceneBase> sceneList = new();

    /// <summary>
    /// 現在のシーンの数を取得する
    /// </summary>
    /// <returns>シーンの数</returns>
    public int GetSceneCount { get => sceneList.Count(); }

    /// <summary>
    /// 現在のシーンの名前
    /// </summary>
    /// <value>シーン名</value>
    public string NowSceneName { get; private set; } = string.Empty;

    /// <summary>
    /// シーンを追加する
    /// </summary>
    /// <param name="sceneName">シーンの名前</param>
    /// <param name="scene">シーン</param>
    public void AddScene(string sceneName, SceneBase scene)
    {
        if (sceneName == string.Empty || scene == null)
            return;

        sceneList.Add(sceneName, scene);
    }

    /// <summary>
    /// シーンを変更する
    /// </summary>
    /// <param name="sceneName">変更先のシーン名</param>
    public void ChangeScene(string sceneName)
    {
        if (scene != null)
            scene.Finish();

        scene = sceneList[sceneName];
        isInit = true;
        NowSceneName = sceneName;
    }

    /// <summary>
    /// 初期化パート
    /// </summary>
    public void Init()
    {
        if (scene == null || !isInit)
            return;

        scene.Init();
        isInit = false;
    }

    /// <summary>
    /// 更新パート
    /// </summary>
    public void Update()
    {
        if (scene == null || isInit)
            return;

        scene.Update();
    }

    /// <summary>
    /// レンダリングパート
    /// </summary>
    public void Render()
    {
        if (scene == null || isInit)
            return;

        scene.Render();
    }
}