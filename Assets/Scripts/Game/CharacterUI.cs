using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public int id;
    public Character character;
    public TextMeshProUGUI name, damage, health;
    public Image artwork;
    private int _damage, _health;
    public GameManager gameManager;
    public bool myPlayer;
    public int playerNumber;
    public int DamageVariable
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
            UpdateUI();
        }
    }
    
    public int HealthVariable
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            UpdateUI();

            if (_health <= 0)
            {
                if (myPlayer)
                {
                    gameManager.LeftPlayersForMe--;
                    if(gameManager.LeftPlayersForMe == gameManager.attackCount)
                        gameManager.ClearMyTeamAttacks();
                }
                else
                {
                    gameManager.LeftPlayersForOpponent--;
                    if(gameManager.LeftPlayersForOpponent == gameManager.opponentAttackCount)
                        gameManager.ClearOpponentTeamAttacks();
                }

                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (playerNumber == 1)
            id = PlayerPrefs.GetInt("FirstPlayer", 0);
        if (playerNumber == 2)
            id = PlayerPrefs.GetInt("SecondPlayer", 1);
        if (playerNumber == 3)
            id = PlayerPrefs.GetInt("ThirdPlayer", 2);
        if (playerNumber == 4)
            id = PlayerPrefs.GetInt("ForthPlayer", 3);
        
        

        character = GameManager.instance.Characters[id];
        name.text = character.name;
        damage.text = character.damage.ToString();
        health.text = character.health.ToString();
        artwork.sprite = character.artwork;
        _damage = character.damage;
        _health = character.health;

    }

    public void UpdateUI()
    {
        damage.text = _damage.ToString();
        health.text = _health.ToString();
    }
    
    
}
