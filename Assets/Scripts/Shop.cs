using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Slider playerHealthSlider, eggHealthSlider, attackSlider;
    public int maxHealth, maxAttack;

    public int currentPlayerHealth, currentEggHealth, currentAttack;
    public int PlayerHealthCost, EggHealthCost, PlayerAttackhCost;
    public int souls;

    // Start is called before the first frame update
    void Start()
    {
        //Reset();
        SetBuff();
    }

    void Update()
    {
        souls = PlayerPrefs.GetInt("Souls");
    }

    void SetBuff(){
        currentEggHealth = PlayerPrefs.GetInt("EggHealth", 0);
        currentPlayerHealth = PlayerPrefs.GetInt("PlayerHealth", 0);
        currentAttack = PlayerPrefs.GetInt("attack", 0);

        PlayerHealthCost = 125;
        EggHealthCost = 150;
        PlayerAttackhCost = 100;

        playerHealthSlider.value = currentPlayerHealth;
        eggHealthSlider.value = currentEggHealth;
        attackSlider.value = currentAttack;
    }

    public void buyEggHealth(){
        if(currentEggHealth < maxHealth && souls >= EggHealthCost){
            currentEggHealth += 20;
            souls -= EggHealthCost;
            PlayerPrefs.SetInt("EggHealth", currentEggHealth);
            PlayerPrefs.SetInt("Souls", souls);
            eggHealthSlider.value = currentEggHealth;
            Debug.Log("EHealth : " + currentEggHealth);
        }
        else{
            Debug.Log("MAX");
        }
    }

    public void buyPlayerHealth(){
        if(currentPlayerHealth < maxHealth && souls >= PlayerHealthCost){
            currentPlayerHealth += 20;
            souls -= PlayerHealthCost;
            PlayerPrefs.SetInt("PlayerHealth", currentPlayerHealth);
            PlayerPrefs.SetInt("Souls", souls);
            playerHealthSlider.value = currentPlayerHealth;
            Debug.Log("PHealth : " + currentPlayerHealth);
        }
        else{
            Debug.Log("MAX");
        }
    }

    public void buyPlayerAttack(){
        if(currentAttack < maxAttack && souls >= PlayerAttackhCost){
            currentAttack += 10;
            souls -= PlayerAttackhCost;
            PlayerPrefs.SetInt("attack", currentAttack);
            PlayerPrefs.SetInt("Souls", souls);
            attackSlider.value = currentAttack;
            Debug.Log("Attack : " + currentAttack);
        }
        else{
            Debug.Log("MAX");
        }
    }

    public void Reset(){
        PlayerPrefs.SetInt("PlayerHealth", 0);
        PlayerPrefs.SetInt("EggHealth", 0);
        PlayerPrefs.SetInt("attack", 0);
        PlayerPrefs.SetInt("souls", 0);
    }
}
