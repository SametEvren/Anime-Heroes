using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public List<Character> Characters;
    public List<CharacterUI> myTeam,opponentTeam;
    public int attacker, defender;
    private int turn;
    public TextMeshProUGUI turnText;
    public int leftPlayersForMe, leftPlayersForOpponent;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverText;
    public GameObject punchUI;
    public GameObject allGame;

    public List<bool> myAttackedChars;
    public List<bool> opponentAttackedChars;
    public int attackCount;
    public int attackingNumber;
    public int opponentAttackCount;


    private void Start()
    {
        for (int i = 0; i < opponentTeam.Count; i++)
        {
            opponentTeam[i].id = UnityEngine.Random.Range(0, Characters.Count);
        }
    }

    public int TurnVariable
    {
        get
        {
            return turn;
        }
        set
        {
            turn = value;
            UpdateTurnUI();
        }
    }
    public int LeftPlayersForMe
    {
        get
        {
            return leftPlayersForMe;
        }
        set
        {
            leftPlayersForMe = value;
            if (leftPlayersForMe == 0)
            {
                gameOverScreen.SetActive(true);
                gameOverText.text = "YOU LOST!";
            }
        }
    }
    public int LeftPlayersForOpponent
    {
        get
        {
            return leftPlayersForOpponent;
        }
        set
        {
            leftPlayersForOpponent = value;
            if (leftPlayersForOpponent == 0)
            {
                gameOverScreen.SetActive(true);
                gameOverText.text = "YOU WIN!";
            }
        }
    }

    


    enum Attacker
    {
        Player1,
        Player2,
        Player3,
        Player4,
        NotAttacking
    }

    [SerializeField]
    private Attacker myAttacker;
    
     enum Defender
    {
        Player1,
        Player2,
        Player3,
        Player4,
        NotDefending
    }

    [SerializeField]
    private Defender myDefender;
    
    

    public void ChangeAttacker(string name)
    {
        if (turn == 1)
            return;
        if (name.Contains("1") && myAttackedChars[0] == true)
            return;
        if (name.Contains("2") && myAttackedChars[1] == true)
            return;
        if (name.Contains("3") && myAttackedChars[2] == true)
            return;
        if (name.Contains("4") && myAttackedChars[3] == true)
            return;
        
        
        attacker = Convert.ToInt16(name) - 1;

        for (int i = 0; i < 4; i++)
        {
            if(i == attacker)
                myTeam[i].gameObject.transform.DOScale(2.8f,0.01f);
            else
                myTeam[i].gameObject.transform.DOScale(2.2f,0.01f);
        }

        if (name.Contains("1"))
        {
            myAttacker = Attacker.Player1;
            attackingNumber = 0;
        }

        if (name.Contains("2"))
        {
            myAttacker = Attacker.Player2;
            attackingNumber = 1;
        }

        if (name.Contains("3"))
        {
            myAttacker = Attacker.Player3;
            attackingNumber = 2;
        }

        if (name.Contains("4"))
        {
            myAttacker = Attacker.Player4;
            attackingNumber = 3;
        }
    }

    public void ChangeDefender(string name)
    {
        if (turn == 1)
            return;
        
        defender = Convert.ToInt16(name) - 1;
        
        if (name.Contains("1"))
            myDefender = Defender.Player1;
        if (name.Contains("2"))
            myDefender = Defender.Player2;
        if (name.Contains("3"))
            myDefender = Defender.Player3;
        if (name.Contains("4"))
            myDefender = Defender.Player4;
    }

    public void Attack()
    {
        if (myAttacker == Attacker.NotAttacking)
            return;
        else
        {
            punchUI.GetComponent<RectTransform>().position =
                myTeam[attacker].gameObject.GetComponent<RectTransform>().position + Vector3.right * 150;
            punchUI.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            punchUI.GetComponent<Image>().enabled = true;
            punchUI.GetComponent<RectTransform>()
                .DOMove(opponentTeam[defender].gameObject.GetComponent<RectTransform>().position + Vector3.left * 150,0.5f).OnComplete(() =>
                {
                    opponentTeam[defender].gameObject.GetComponent<RectTransform>().DOShakePosition(0.5f, 5f);
                    opponentTeam[defender].gameObject.GetComponent<RectTransform>().DOShakeRotation(0.5f, 5f).OnComplete(()=>
                    {
                        opponentTeam[defender].HealthVariable -= myTeam[attacker].DamageVariable;
                        myAttacker = Attacker.NotAttacking;
                        myDefender = Defender.NotDefending;
                        TurnVariable = 1;
                        punchUI.GetComponent<Image>().enabled = false;
                        for (int i = 0; i < 4; i++)
                        {
                            myTeam[i].gameObject.transform.DOScale(2.2f,0.01f);
                        }

                        myAttackedChars[attackingNumber] = true;

                        attackCount++;
                        if (attackCount == LeftPlayersForMe)
                        {
                            for (int i = 0; i < myAttackedChars.Count; i++)
                            {
                                myAttackedChars[i] = false;
                            }

                            attackCount = 0;
                        }

                        StartCoroutine(OpponentAttack(2f));
                    }); 
                });
            

        }
    }

    public void ClearMyTeamAttacks()
    {
        for (int i = 0; i < myAttackedChars.Count; i++)
        {
            myAttackedChars[i] = false;
        }

        attackCount = 0;
    }

    public void ClearOpponentTeamAttacks()
    {
        for (int i = 0; i < opponentAttackedChars.Count; i++)
        {
            opponentAttackedChars[i] = false;
        }

        opponentAttackCount = 0;
    }
    public IEnumerator OpponentAttack(float time)
    {
        yield return new WaitForSeconds(time);
        int randOpp = UnityEngine.Random.Range(0, 4);
        int randMy = UnityEngine.Random.Range(0, 4);
        if(!opponentTeam[randOpp].gameObject.activeSelf || !myTeam[randMy].gameObject.activeSelf || opponentAttackedChars[randOpp])
            StartCoroutine(OpponentAttack(0f));
        else
        {
            punchUI.GetComponent<RectTransform>().position =
                opponentTeam[randOpp].gameObject.GetComponent<RectTransform>().position + Vector3.left * 150;
            punchUI.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            punchUI.GetComponent<Image>().enabled = true;
            punchUI.GetComponent<RectTransform>()
                .DOMove(myTeam[randMy].gameObject.GetComponent<RectTransform>().position + Vector3.right * 150,
                    0.5f).OnComplete(
                    () =>
                    {
                        myTeam[randMy].gameObject.GetComponent<RectTransform>().DOShakePosition(0.5f, 5f);
                        myTeam[randMy].gameObject.GetComponent<RectTransform>().DOShakeRotation(0.5f, 5f).OnComplete(() =>
                        {
                            opponentAttackedChars[randOpp] = true;
                            opponentAttackCount++;
                            if (opponentAttackCount == LeftPlayersForOpponent)
                            {
                                for (int i = 0; i < opponentAttackedChars.Count; i++)
                                {
                                    opponentAttackedChars[i] = false;
                                }

                                opponentAttackCount = 0;
                            }
                            myTeam[randMy].HealthVariable -= opponentTeam[randOpp].DamageVariable;
                            TurnVariable = 0;
                            punchUI.GetComponent<Image>().enabled = false;
                        });
                    });
        }
    }

    public void UpdateTurnUI()
    {
        if (turn == 0)
            turnText.text = "Your Turn";
        if (turn == 1)
            turnText.text = "Opponent Turn";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public void ChangeMusic()
    {
        MusicManager.Instance.ChangeSong(UnityEngine.Random.Range(0,MusicManager.Instance.songs.Length));
    }

    public void StopMusic()
    {
        if (MusicManager.Instance.volume == 1)
        {
            MusicManager.Instance.volume = 0;
            return;
        }

        if (MusicManager.Instance.volume == 0)
        {
            MusicManager.Instance.volume = 1;
            return;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
