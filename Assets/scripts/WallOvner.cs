using UnityEngine;

public class WallOvner : MonoBehaviour
{
    public Player Ovner;

    #region SingleTon
    public static WallOvner Instance { get; private set; }
    #endregion

    private void Awake()
    {
        Instance = this;
    }
}
