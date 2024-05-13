using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI banker_Balance;
    private TMPro.TextMeshProUGUI player1_Balance;
    private TMPro.TextMeshProUGUI player2_Balance;
    private TMPro.TextMeshProUGUI player3_Balance;
    private TMPro.TextMeshProUGUI player4_Balance;
    private TMPro.TextMeshProUGUI diceNumber;

    private void Awake()
    {
        if(this.transform.childCount != 6)
        {
            Debug.LogError("Did you setup the players and banker UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        SetupBalance(0, ref banker_Balance);
        SetupBalance(1, ref player1_Balance);
        SetupBalance(2, ref player2_Balance);
        SetupBalance(3, ref player3_Balance);
        SetupBalance(4, ref player4_Balance);
        SetupBalance(5, ref diceNumber);

        SetupPlayerName(1);
        SetupPlayerName(2);
        SetupPlayerName(3);
        SetupPlayerName(4);

        SetupPlayerBackground(1);
        SetupPlayerBackground(2);
        SetupPlayerBackground(3);
        SetupPlayerBackground(4);

        Player.OnPlayerMoneyChanged += OnPlayersMoneyChanged;
        Banker.OnBankerMoneyChanged += OnBankersMoneyChanged;
        Dice.OnDiceRolled += OnDiceRolled;
    }

    private void OnDestroy()
    {
        Player.OnPlayerMoneyChanged -= OnPlayersMoneyChanged;
        Banker.OnBankerMoneyChanged -= OnBankersMoneyChanged;
        Dice.OnDiceRolled -= OnDiceRolled;
    }

    private void SetupPlayerBackground(int playerIndex)
    {
        Transform temporary = this.transform.GetChild(playerIndex);

        if (!temporary.TryGetComponent<UnityEngine.UI.Image>(out UnityEngine.UI.Image output))
        {
            Debug.LogError("Error setting up the Background.");
            Utilities.QuitPlayModeInEditor();
        }

        if(!temporary.TryGetComponent<Player>(out Player player))
        {
            Debug.LogError("Error getting player script.");
            Utilities.QuitPlayModeInEditor();
        }

        output.color = player.GetPlayerColor();
    }

    private void SetupPlayerName(int childIndex)
    {
        Transform temporary = this.transform.GetChild(childIndex).GetChild(0);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out TMPro.TextMeshProUGUI output))
        {
            Debug.LogError("Error setting up the Player Name.");
            Utilities.QuitPlayModeInEditor();
        }

        output.text = "Player: " + childIndex;
    }

    private void SetupBalance(int childIndex, ref TMPro.TextMeshProUGUI output)
    {
        Transform temporary = transform.GetChild(childIndex);

        if (temporary.childCount < 2)
        {
            Debug.LogError("Did you setup the UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(1);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out output))
        {
            Debug.LogError("Error setting up the UI.");
            Utilities.QuitPlayModeInEditor();
        }
    }

    private void OnPlayersMoneyChanged(System.Int64 updatedMoney, ushort playerID)
    {
        switch (playerID)
        {
            case 1:
                FormatToIndianCurrencyAndPrint(player1_Balance, updatedMoney);
                break;
            case 2:
                FormatToIndianCurrencyAndPrint(player2_Balance, updatedMoney);
                break;
            case 3:
                FormatToIndianCurrencyAndPrint(player3_Balance, updatedMoney);
                break;
            case 4:
                FormatToIndianCurrencyAndPrint(player4_Balance, updatedMoney);
                break;
            default:
                Debug.LogError("Unhandled player ID: " + playerID);
                Utilities.QuitPlayModeInEditor();
                break;
        }
    }

    private void OnBankersMoneyChanged(System.Int64 updatedMoney)
    {
        FormatToIndianCurrencyAndPrint(banker_Balance, updatedMoney);
    }

    private void FormatToIndianCurrencyAndPrint(TMPro.TMP_Text output, System.Int64 amount)
    {
        string formattedString = amount.ToString();

        for (int i = formattedString.Length - 3; i > 0; i -= 2)
        {
            formattedString = formattedString.Insert(i, ",");
        }

        output.text = "Balance: " + formattedString + " Rupees";
    }

    private void OnDiceRolled(int diceValue)
    {
        diceNumber.text = "Number: " + diceValue.ToString();
    }
}