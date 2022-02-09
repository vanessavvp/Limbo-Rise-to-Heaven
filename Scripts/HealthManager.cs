using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HealthManager : MonoBehaviour {
    public static HealthManager instance;
    public int currentHealth, maxHealth;
    public float invincibleLength = 1f;
    public float invincibleCounter;

    public Sprite[] healthBarImages;

    
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        ResetHealth();
    }

    // Update is called once per frame
    void Update() {
        if (invincibleCounter > 0) {
            invincibleCounter -= Time.deltaTime;
        } else {
            invincibleCounter = 0;
        }
    }

    public void Hurt() {
        if (invincibleCounter <= 0) {
            currentHealth--;
            if (currentHealth <= 0) {
                currentHealth = 0;
                GameManager.instance.Respawn();
            } else {
                FirstPersonController.instance.KnockBack();
                invincibleCounter = invincibleLength;
                UIManager.instance.StartHurtingFlash(.25f);
            }
        }
        UpdateUI();
    }

    public void ResetHealth() {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UIManager.instance.healthText.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int healAmount) {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }

    public void UpdateUI() {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch(currentHealth) {
            case 0:
                UIManager.instance.healthImage.enabled = false;
                UIManager.instance.healthText.enabled = false;
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
        }
    }

    public void PlayerKilled() {
        currentHealth = 0;
        UpdateUI();
    }
}
