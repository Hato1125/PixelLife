namespace PixelLife;

internal class SceneBase
{
    protected readonly List<SceneBase> Child = new();

    /// <summary>
    /// 初期化パート
    /// </summary>
    public virtual void Init()
    {
        if (Child.Count() < 0)
            return;

        foreach (var item in Child)
            item.Init();
    }

    /// <summary>
    /// 更新パート
    /// </summary>
    public virtual void Update()
    {
        if (Child.Count() < 0)
            return;

        foreach (var item in Child)
            item.Update();
    }

    /// <summary>
    /// レンダリングパート
    /// </summary>
    public virtual void Render()
    {
        if (Child.Count() < 0)
            return;

        foreach (var item in Child)
            item.Render();
    }

    /// <summary>
    /// 終了パート
    /// </summary>
    public virtual void Finish()
    {
        if (Child.Count() < 0)
            return;

        foreach (var item in Child)
            item.Finish();
    }
}