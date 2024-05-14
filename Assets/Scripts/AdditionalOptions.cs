using UnityEngine;
using UnityEngine.UI;

public class AdditionalOptions : MonoBehaviour
{
    private GameObject buyMenu;
    private TMPro.TextMeshProUGUI b_title;
    private TMPro.TextMeshProUGUI b_price;
    private TMPro.TextMeshProUGUI b_rent;
    private Button b_buyButton;
    private Button b_cancelButton;

    private GameObject rentMenu;
    private TMPro.TextMeshProUGUI r_title;
    private TMPro.TextMeshProUGUI r_rent;
    private Button r_payrentButton;

    private PropertyData propertyData_Ref;

    private void Awake()
    {
        if (this.transform.childCount != 2)
        {
            Debug.LogError("Object didn't setup correctly.");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        buyMenu = this.transform.GetChild(0).gameObject;

        if(buyMenu == null)
        {
            Debug.LogError("Object didn't setup correctly.");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        rentMenu = this.transform.GetChild(1).gameObject;

        if(rentMenu == null)
        {
            Debug.LogError("Object didn't setup correctly.");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        SetReferences(0);
        SetReferences(1);

        CloseBuyMenu();
        CloseRentMenu();

        Property.showBuyMenu += OpenBuyMenu;
        Property.showrentMenu += OpenRentMenu;

        b_buyButton.onClick.RemoveAllListeners();
        b_buyButton.onClick.AddListener(() => { GameManager.instance.GetBanker().CreditAmount(propertyData_Ref.buyAmount);
                                                propertyData_Ref.owner.DebitAmount(propertyData_Ref.buyAmount);
                                                propertyData_Ref.isBuyable = false; 
                                                CloseBuyMenu(); 
                                                GameManager.instance.SwitchToNextPlayer();
                                                propertyData_Ref.bg.color = propertyData_Ref.owner.GetPlayerColor();
                                                propertyData_Ref.owner.IncrementProperitiesOwned();
                                                propertyData_Ref.owner.IncrementAssetMoney(propertyData_Ref.buyAmount);
                                                });

        b_cancelButton.onClick.RemoveAllListeners();
        b_cancelButton.onClick.AddListener(() => { propertyData_Ref.owner = null; 
                                                    propertyData_Ref.isBuyable = true; 
                                                    CloseBuyMenu(); 
                                                    GameManager.instance.SwitchToNextPlayer(); });

        r_payrentButton.onClick.RemoveAllListeners();
        r_payrentButton.onClick.AddListener(()=> {  propertyData_Ref.otherPlayer.DebitAmount(propertyData_Ref.rentAmount);
                                                    propertyData_Ref.owner.CreditAmount(propertyData_Ref.rentAmount);
                                                    CloseRentMenu(); 
                                                    GameManager.instance.SwitchToNextPlayer(); });
    }

    private void OnDestroy()
    {
        Property.showBuyMenu -= OpenBuyMenu;
        Property.showrentMenu -= OpenRentMenu;

        b_buyButton.onClick.RemoveAllListeners();
        b_cancelButton.onClick.RemoveAllListeners();
        r_payrentButton.onClick.RemoveAllListeners();
    }

    void OpenBuyMenu(PropertyData propertyData)
    {
        buyMenu.SetActive(true);
        b_title.text = "Buy Menu for: " + propertyData.propertyName.ToString();
        b_price.text = "Price: " + propertyData.buyAmount.ToString();
        b_rent.text = "Rent: " + propertyData.rentAmount.ToString();

        if (propertyData.buyAmount <= propertyData.owner.MoneyAvailable())
        {
            b_buyButton.interactable = true;
        }
        else
        {
            b_buyButton.interactable = false;
        }

        propertyData_Ref = propertyData;
    }

    void CloseBuyMenu()
    {
        buyMenu.SetActive(false);
    }

    void OpenRentMenu(PropertyData propertyData)
    {
        rentMenu.SetActive(true);
        r_title.text = "Rent Menu for: " + propertyData.propertyName.ToString();
        r_rent.text = "Rent: " + propertyData.rentAmount.ToString();

        propertyData_Ref = propertyData;
    }

    void CloseRentMenu()
    {
        rentMenu.SetActive(false);
    }

    void SetReferences(ushort childCount)
    {
        if(childCount == 0)
        {
            if(buyMenu.transform.childCount != 5)
            {
                Debug.LogError("Buy menu child count is wrong.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if(!buyMenu.transform.GetChild(0).TryGetComponent<TMPro.TextMeshProUGUI>(out b_title))
            {
                Debug.LogError("Buy menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!buyMenu.transform.GetChild(1).TryGetComponent<TMPro.TextMeshProUGUI>(out b_price))
            {
                Debug.LogError("Buy menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!buyMenu.transform.GetChild(2).TryGetComponent<TMPro.TextMeshProUGUI>(out b_rent))
            {
                Debug.LogError("Buy menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!buyMenu.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out b_buyButton))
            {
                Debug.LogError("Buy menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!buyMenu.transform.GetChild(4).TryGetComponent<UnityEngine.UI.Button>(out b_cancelButton))
            {
                Debug.LogError("Buy menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }
        }
        else if (childCount == 1)
        {
            if (rentMenu.transform.childCount != 3)
            {
                Debug.LogError("Buy menu child count is wrong.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            //private TMPro.TextMeshPro r_title;
            //private TMPro.TextMeshPro r_rent;

            if (!rentMenu.transform.GetChild(0).TryGetComponent<TMPro.TextMeshProUGUI>(out r_title))
            {
                Debug.LogError("Rent menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!rentMenu.transform.GetChild(1).TryGetComponent<TMPro.TextMeshProUGUI>(out r_rent))
            {
                Debug.LogError("Rent menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }

            if (!rentMenu.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out r_payrentButton))
            {
                Debug.LogError("Rent menu not setup correctly.");
                Utilities.QuitPlayModeInEditor();
                return;
            }
        }
        else
        {
            Debug.LogError("Something went wrong.");
            Utilities.QuitPlayModeInEditor();
            return;
        }
    }
}
