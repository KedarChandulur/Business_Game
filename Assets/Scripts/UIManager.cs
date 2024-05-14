using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMPro.TextMeshProUGUI banker_Balance;
    private TMPro.TextMeshProUGUI player1_Balance;
    private TMPro.TextMeshProUGUI player2_Balance;
    private TMPro.TextMeshProUGUI player3_Balance;
    private TMPro.TextMeshProUGUI player4_Balance;

    private TMPro.TextMeshProUGUI player1_AssetsMoney;
    private TMPro.TextMeshProUGUI player2_AssetsMoney;
    private TMPro.TextMeshProUGUI player3_AssetsMoney;
    private TMPro.TextMeshProUGUI player4_AssetsMoney;

    private TMPro.TextMeshProUGUI diceNumber;
    private TMPro.TextMeshProUGUI diceTitle;
    private TMPro.TextMeshProUGUI statusText;

    private void Awake()
    {
        if(this.transform.childCount != 7)
        {
            Debug.LogError("Did you setup the players and banker UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        SetupBalance(0, ref banker_Balance);
        SetupBalance(1, ref player1_Balance);
        SetupBalance(2, ref player2_Balance);
        SetupBalance(3, ref player3_Balance);
        SetupBalance(4, ref player4_Balance);

        Setup_AssetMoney(1, ref player1_AssetsMoney);
        Setup_AssetMoney(2, ref player2_AssetsMoney);
        Setup_AssetMoney(3, ref player3_AssetsMoney);
        Setup_AssetMoney(4, ref player4_AssetsMoney);

        SetupBalance(5, ref diceNumber);
        SetDiceTitle(5, ref diceTitle);
        SetStatusText(6, ref statusText);

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
        GameManager.OnPlayerInit += OnPlayerInit;

        Player.OnPlayerAssetMoneyChanged += OnPlayerAssetMoneyChanged;
    }

    private void Start()
    {
        Dice.OnDiceRolled += OnDiceRolled;
        GameManager.OnNextPlayerTurn += OnNextPlayerTurn;
        GameManager.OnPlayerWin += OnPlayerWin;

        statusText.text = string.Empty;
    }

    private void OnDestroy()
    {
        Player.OnPlayerMoneyChanged -= OnPlayersMoneyChanged;
        Banker.OnBankerMoneyChanged -= OnBankersMoneyChanged;
        GameManager.OnPlayerInit -= OnPlayerInit;

        Dice.OnDiceRolled -= OnDiceRolled;
        GameManager.OnNextPlayerTurn -= OnNextPlayerTurn;
        GameManager.OnPlayerWin -= OnPlayerWin;

        Player.OnPlayerAssetMoneyChanged -= OnPlayerAssetMoneyChanged;
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

    private void SetDiceTitle(int childIndex, ref TMPro.TextMeshProUGUI output)
    {
        Transform temporary = transform.GetChild(childIndex);

        if (temporary.childCount < 2)
        {
            Debug.LogError("Did you setup the UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(0);

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out output))
        {
            Debug.LogError("Error setting up the UI.");
            Utilities.QuitPlayModeInEditor();
        }
    }

    private void SetStatusText(int childIndex, ref TMPro.TextMeshProUGUI output)
    {
        Transform temporary = transform.GetChild(childIndex);

        if (temporary.childCount != 0)
        {
            Debug.LogError("Did you setup the UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        if (!temporary.TryGetComponent<TMPro.TextMeshProUGUI>(out output))
        {
            Debug.LogError("Error setting up the UI.");
            Utilities.QuitPlayModeInEditor();
        }
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

    private void Setup_AssetMoney(int childIndex, ref TMPro.TextMeshProUGUI output)
    {
        Transform temporary = transform.GetChild(childIndex);

        if (temporary.childCount < 2)
        {
            Debug.LogError("Did you setup the UI correctly?");
            Utilities.QuitPlayModeInEditor();
        }

        temporary = temporary.GetChild(2);

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

    private void OnPlayerAssetMoneyChanged(System.Int64 updatedAssetMoney, ushort playerID)
    {
        switch (playerID)
        {
            case 1:
                FormatAssetMoneyToIndianCurrencyAndPrint(player1_AssetsMoney, updatedAssetMoney);
                break;
            case 2:
                FormatAssetMoneyToIndianCurrencyAndPrint(player2_AssetsMoney, updatedAssetMoney);
                break;
            case 3:
                FormatAssetMoneyToIndianCurrencyAndPrint(player3_AssetsMoney, updatedAssetMoney);
                break;
            case 4:
                FormatAssetMoneyToIndianCurrencyAndPrint(player4_AssetsMoney, updatedAssetMoney);
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

    private void FormatAssetMoneyToIndianCurrencyAndPrint(TMPro.TMP_Text output, System.Int64 amount)
    {
        string formattedString = amount.ToString();
        bool settingNegativeValue = false;

        if (formattedString.StartsWith('-'))
        {
            settingNegativeValue = true;
            formattedString = formattedString.Remove(0, 1);
        }

        for (int i = formattedString.Length - 3; i > 0; i -= 2)
        {
            formattedString = formattedString.Insert(i, ",");
        }

        if (settingNegativeValue)
        {
            if (formattedString.StartsWith(','))
            {
                formattedString = formattedString.Remove(0, 1);
            }

            formattedString = formattedString.Insert(0, "-");
        }

        output.text = "Total Amount invested on Assets: " + formattedString + " Rupees";
    }

    private void FormatToIndianCurrencyAndPrint(TMPro.TMP_Text output, System.Int64 amount)
    {
        string formattedString = amount.ToString();
        bool settingNegativeValue = false;

        if (formattedString.StartsWith('-'))
        {
            settingNegativeValue = true;
            formattedString = formattedString.Remove(0, 1);
        }

        for (int i = formattedString.Length - 3; i > 0; i -= 2)
        {
            formattedString = formattedString.Insert(i, ",");
        }

        if(settingNegativeValue)
        {
            if(formattedString.StartsWith(','))
            {
                formattedString = formattedString.Remove(0, 1);
            }

            formattedString = formattedString.Insert(0, "-");
        }

        output.text = "Balance: " + formattedString + " Rupees";
    }

    private void OnDiceRolled(int diceValue)
    {
        diceNumber.text = "Number: " + diceValue.ToString();
    }

    public void OnPlayerInit(Player player)
    {
        if(player != null && player.GetPlayerID() == 1)
        { 
            diceTitle.color = player.GetPlayerColor();
        }
    }

    public void OnNextPlayerTurn(UnityEngine.Color playerColor)
    {
        diceTitle.color = playerColor;
    }

    public void OnPlayerWin(Player player)
    {
        statusText.text = "Status: Player: " + player.GetPlayerID() + " Won! with the asset investment and savings combined: " + player.GetFinalAmount() + "\n Congratulations player: " + player.GetPlayerID();
    }
}