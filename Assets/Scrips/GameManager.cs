using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum RaceStatus { Running, Ended };

    public static GameManager Instance { get; private set; }

    public Text ResultText;
    public RaceStatus status = RaceStatus.Running;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResultText.enabled = false;
        status = RaceStatus.Running;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotArrived()
    {
        if(status == RaceStatus.Running)
        {
            ResultText.text = "You Lose!";
            EndRace();
        }
    }

    public void PlayerArrived()
    {
        if (status == RaceStatus.Running)
        {
            ResultText.text = "You win!";
            EndRace();
        }
    }

    private void EndRace()
    {
        ResultText.enabled = true;
        status = RaceStatus.Ended;
    }
}
