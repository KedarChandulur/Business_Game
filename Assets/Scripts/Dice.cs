public class Dice : UnityEngine.MonoBehaviour
{
    private UnityEngine.UI.Button rollButton;
    public static System.Action<int> OnDiceRolled;

    private void Start()
    {
        if(this.transform.childCount != 3)
        {
            UnityEngine.Debug.LogError("Did you setup dice object correctly?");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        if(!this.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out rollButton))
        {
            UnityEngine.Debug.LogError("Did you setup roll button correctly?");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        rollButton.onClick.RemoveAllListeners();
        rollButton.onClick.AddListener(() => { rollButton.interactable = false; int diceNumber = UnityEngine.Random.Range(1, 6); OnDiceRolled?.Invoke(diceNumber); } );

        GameManager.OnNextPlayerTurn += OnNextPlayerTurn;
    }

    private void OnDestroy()
    {
        rollButton.onClick.RemoveAllListeners();
        GameManager.OnNextPlayerTurn -= OnNextPlayerTurn;
    }

    private void OnNextPlayerTurn(UnityEngine.Color playerColor)
    {
        rollButton.interactable = true;
    }
}
