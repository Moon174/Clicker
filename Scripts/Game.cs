using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Game : MonoBehaviour
{

    public int Money;
    private int MoneyUp = 1;

    private bool Check = true;

    public Text MoneyText;

    public GameObject ExitPanel;

    public int[] UpgradeNumber;
    public Text[] ShopText;
    public GameObject StorePanel;
    public int[] Auto;
    public int BonusPerSec;

    private Save sv = new Save();

    private void Awake()
    {
        if(PlayerPrefs.HasKey("SV"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            Money = sv.Money;
            MoneyUp = sv.MoneyUp;
            BonusPerSec = sv.BonusPerSec;
            

         
            for (int i = 0; i < 1; i++)
            {
                UpgradeNumber[i] = sv.UpgradeNumber[i];
                ShopText[0].text = "Улучшить бур \n" + sv.UpgradeNumber[i] + "$";
            }


            for (int i = 0; i < 1; i++)
            {
                Auto[i] = sv.Auto[i];
                ShopText[1].text = "Автосбор Валюты \n" + Auto[i] + "$";
            }
        }
    }

    private void Start()
    {
        StartCoroutine(BonusSec());
    }


    void Update()
    {
        MoneyText.text = Money + "$";
    }

    public void OnClickButton()
    {
        Money += MoneyUp;
    }

    public void OnClickStoreButton()
    {
        if (Check == true)
        {
            StorePanel.SetActive(true);
            Check = false;
        }

        else
        {
            StorePanel.SetActive(false);
            Check = true;
        }

    }

    public void OnClickExit()
    {
        if (Check == true)
        {
            ExitPanel.SetActive(true);
            Check = false;
            Time.timeScale = 0f;
        }

        else
        {
            ExitPanel.SetActive(false);
            Check = true;
            Time.timeScale = 1f;
        }

    }


    public void OnClickBuy()
    {
        if (Money >= UpgradeNumber[0])
        {
            MoneyUp *= 2;
            Money -= UpgradeNumber[0];
            UpgradeNumber[0] *= 5;
            ShopText[0].text = "Улучшить бур \n" + UpgradeNumber[0] + "$";

        }
    }

    public void AutoCollect(int index)
    {
        if (Money >= Auto[0])
        {
            BonusPerSec++;
            Money -= Auto[0];
            Auto[0] *= 2;
            ShopText[1].text = "Автосбор Валюты \n" + Auto[0] + "$";
        }
    }

    IEnumerator BonusSec()
    {
        while (true)
        {
            Money += BonusPerSec;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnApplicationQuit()
    {
        sv.Money = Money;
        sv.UpgradeNumber = new int[1];
        sv.Auto = new int[1];
        sv.MoneyUp = MoneyUp;
        sv.BonusPerSec = BonusPerSec;


        for (int i = 0; i < 1; i++)
        {
            sv.UpgradeNumber[i] = UpgradeNumber[i];
        }

        for (int i = 0; i < 1; i++)
        {
            sv.Auto[i] = Auto[i];
        }



        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }


}

[Serializable]

public class Save
{
    public int Money;
    public int[] UpgradeNumber;
    public int[] Auto;
    public int MoneyUp;
    public int BonusPerSec;
}