using UnityEngine;

public class UIManager : MonoBehaviour
{
    //private Banker banker_UI;
    //private Player player1_UI;
    //private Player player2_UI;
    //private Player player3_UI;
    //private Player player4_UI;

    private TMPro.TextMeshProUGUI banker_UI;
    private TMPro.TextMeshProUGUI player1_UI;
    private TMPro.TextMeshProUGUI player2_UI;
    private TMPro.TextMeshProUGUI player3_UI;
    private TMPro.TextMeshProUGUI player4_UI;

    private void Awake()
    {
        Transform temporary = null;

        if(this.transform.childCount != 5)
        {
            Debug.LogError("Did you setup the players and banker UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        // Banker Ref
        temporary = this.transform.GetChild(0);

        if (temporary.childCount != 2)
        {
            Debug.LogError("Did you setup the banker UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out banker_UI))
        {
            Debug.LogError("Error setting up banker UI.");
            Utilities.QuitPlayModeInEditor();
        }

        // Player 1 Ref
        temporary = this.transform.GetChild(1);

        if (temporary.childCount != 2)
        {
            Debug.LogError("Did you setup the Player 1 UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out player1_UI))
        {
            Debug.LogError("Error setting up Player 1 UI.");
            Utilities.QuitPlayModeInEditor();
        }

        // Player 2 Ref
        temporary = this.transform.GetChild(2);

        if (temporary.childCount != 2)
        {
            Debug.LogError("Did you setup the Player 2 UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out player2_UI))
        {
            Debug.LogError("Error setting up Player 2 UI.");
            Utilities.QuitPlayModeInEditor();
        }

        // Player 3 Ref
        temporary = this.transform.GetChild(3);

        if (temporary.childCount != 2)
        {
            Debug.LogError("Did you setup the Player 3 UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out player3_UI))
        {
            Debug.LogError("Error setting up Player 3 UI.");
            Utilities.QuitPlayModeInEditor();
        }

        // Player 4 Ref
        temporary = this.transform.GetChild(4);

        if (temporary.childCount != 2)
        {
            Debug.LogError("Did you setup the Player 4 UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out player4_UI))
        {
            Debug.LogError("Error setting up Player 4 UI.");
            Utilities.QuitPlayModeInEditor();
        }

        Player.MoneyChanged += OnPlayersMoneyChange;
        Banker.MoneyChanged += OnBankersMoneyChange;
    }

    private void OnDestroy()
    {
        Player.MoneyChanged -= OnPlayersMoneyChange;
        Banker.MoneyChanged -= OnBankersMoneyChange;
    }

    private void OnPlayersMoneyChange(System.Int64 updatedMoney, ushort playerID)
    {
        switch (playerID)
        {
            case 1:
                FormatToIndianCurrencyAndPrint(player1_UI, updatedMoney);
                break;
            case 2:
                FormatToIndianCurrencyAndPrint(player2_UI, updatedMoney);
                break;
            case 3:
                FormatToIndianCurrencyAndPrint(player3_UI, updatedMoney);
                break;
            case 4:
                FormatToIndianCurrencyAndPrint(player4_UI, updatedMoney);
                break;
            default:
                Debug.LogError("Unhandled player ID: " + playerID);
                Utilities.QuitPlayModeInEditor();
                break;
        }
    }

    private void OnBankersMoneyChange(System.Int64 updatedMoney)
    {
        FormatToIndianCurrencyAndPrint(banker_UI, updatedMoney);
    }

    void FormatToIndianCurrencyAndPrint(TMPro.TMP_Text output, System.Int64 amount)
    {
        string formattedString = amount.ToString();

        for (int i = formattedString.Length - 3; i > 0; i -= 2)
        {
            formattedString = formattedString.Insert(i, ",");
        }

        output.text = "Balance: " + formattedString + " Rupees";
    }
}