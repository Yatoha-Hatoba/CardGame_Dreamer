public interface Iinitialize
{
    /*
    [SerializeField, Header("初期化優先順位_デフォルト - 0 -)")]
    private int _InitPriority = 0;
    public int InitPriority { get { return _InitPriority; } private set { _InitPriority = value; } }
    */
    int InitPriority { get; }
    bool Initialize();
}