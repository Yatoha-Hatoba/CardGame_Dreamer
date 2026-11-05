using UnityEngine;

public class ChildLoadManager : MonoBehaviour
{
    // 親ロードマネージャーをシーンが移行しても使用するために子供ロードマネージャーをシーン上に置いておく
    public void Load(string LoadSceneName)
    {
        ParentLoadManager.Instance.LoadStart(LoadSceneName);
    }
}
