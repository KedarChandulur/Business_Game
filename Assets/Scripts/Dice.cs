public class Dice : UnityEngine.MonoBehaviour
{
    private UnityEngine.UI.Button roll;
    public static System.Action<int> OnDiceRolled;

    private void Start()
    {
        if(this.transform.childCount != 3)
        {
            UnityEngine.Debug.LogError("Did you setup dice object correctly?");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        if(!this.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out roll))
        {
            UnityEngine.Debug.LogError("Did you setup roll button correctly?");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        roll.onClick.RemoveAllListeners();
        roll.onClick.AddListener(() => { int diceNumber = UnityEngine.Random.Range(1, 6); UnityEngine.Debug.Log(diceNumber); OnDiceRolled?.Invoke(diceNumber); } );
    }

    private void OnDestroy()
    {
        roll.onClick.RemoveAllListeners();
    }
}
