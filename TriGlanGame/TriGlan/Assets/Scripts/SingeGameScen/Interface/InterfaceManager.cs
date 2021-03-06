using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public PanelGameOverMeneger panelGameOverMeneger;
    public AudioSource backroundMusic;

    public Sprite hp;
    public Sprite DeadHp;
    public Image boostBar;
    public GameObject TextTime;

    public Text TextTimeTextObj;

    public GameObject UseWeapons;
    public GameObject AllHp;
    public GameObject countCoin;
    public GameObject PauseCanvas;

    public Animator CoinVisibleAnimation;

    private int countCoinI;
    public int countAllGetCoins = 10;

    private int hpInt = 5;
    private int LastHp = 5;

    public TimeSpan time;
    private float timeStartGame;

    private int useWeapon = 1;

    public Sprite useWeaponSprite;

    public LvlMeneger lvlMeneger;

    public MainTrictangle MainTrictangle;

    private Image UseWeapon1Clone;
    private Image UseWeapon2Clone;

    public int HpInt
    {
        get => hpInt;
        set
        {
            LastHp = hpInt;
            hpInt = value;
            OnChangeHp();
        }
    }

    public int UseWeapon
    {
        get => useWeapon;
        set
        {
            useWeapon = value;
            OnChengeWeapon();
        }
    }

    public int CountCoinI
    {
        get => countCoinI;
        set
        {
            countCoinI = value;
            countCoin.GetComponent<Text>().text = countCoinI.ToString();
            CoinVisibleAnimation.SetTrigger("Coin");
        }
    }

    public void OnChengeSkin()
    {
        var WeaponObj = UseWeapons.transform.Find($"UseWeapon{useWeapon}").transform.Find($"Weapon");
        WeaponObj.transform.localPosition = new Vector2(0, 0);
        var imageChangeObj = WeaponObj.GetComponent<Image>();

        if (useWeaponSprite != null)
        {
            imageChangeObj.enabled = true;
            imageChangeObj.sprite = useWeaponSprite;
            var nameSprite = imageChangeObj.sprite.name;
            SetSizeImage(nameSprite);
        }
        else
        {
            imageChangeObj.enabled = false;
            SetSizeImage(null);
        }
    }

    private void SetSizeImage(string nameSprite)
    {
        if (nameSprite == "glock" || nameSprite == "deagle")
            ReziseImage(new Vector3(0.5f, 0.5f));
        if (nameSprite == "drobovikShadowOff" || nameSprite == "mag 7")
            ReziseImage(new Vector3(0.65f, 0.65f));
        if (nameSprite == "awp")
        {
            ReziseImage(new Vector3(0.8f, 0.4f));
            UseWeapons.transform.Find($"UseWeapon{useWeapon}").transform.Find($"Weapon").GetComponent<Image>().transform.position += new Vector3(5, 0, 0);
        }
        else
            ReziseImage(new Vector3(0.65f, 0.35f));
    }

    private void ReziseImage(Vector2 vector2)
    {
        UseWeapons.transform.Find($"UseWeapon{useWeapon}").transform.Find($"Weapon").GetComponent<Image>().transform.localScale = vector2;
    }
    public void OnChengeWeapon()
    {
        if (useWeapon > 1)
        {
            UseWeapon2Clone.enabled = true;
            UseWeapon1Clone.enabled = false;
        }
        else
        {
            UseWeapon2Clone.enabled = false;
            UseWeapon1Clone.enabled = true;
        }
    }


    public void StartCoroutineUpdateMatchInfo()
    {
        StartCoroutine(MainTrictangle.Server.UpdateMatchInfo(ServerMenu.MatchID, lvlMeneger.LvlNow, countAllGetCoins, TextTimeTextObj.text.Replace(" ", "")));
    }

    public void GameOver()
    {
        StartCoroutineUpdateMatchInfo();
        lvlMeneger.LvlNow = 1;
        Time.timeScale = 0f;
        
        panelGameOverMeneger.GameOverStartAnimation();
        MainTrictangle.GameOver();
    }

    public void Restart()
    {
        Start();
        for (int i = 1; i < 6; i++)
            AllHp.gameObject.transform.Find($"hp{i}").GetComponent<Image>().sprite = hp;

        UseWeapons.transform.Find($"UseWeapon1").transform.Find($"Weapon").GetComponent<Image>().enabled = false;
        UseWeapons.transform.Find($"UseWeapon2").transform.Find($"Weapon").GetComponent<Image>().enabled = false;

        if (OptionsClick.backroundMusic)
            backroundMusic.volume = 0.05f;
        StartCoroutine(MainTrictangle.Server.CteateMatch());
    }

    public void OnChangeHp()
    {
        if (hpInt < LastHp)
        {
            {
                AllHp.transform.Find($"hp{LastHp}").GetComponent<Image>().sprite = DeadHp;
                if (LastHp == 1)
                    GameOver();
            }
        }
        else
        {
            if (hpInt <= 5)
                AllHp.transform.Find($"hp{hpInt}").GetComponent<Image>().sprite = hp;
            else
                hpInt = 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        countAllGetCoins = 10;
        lvlMeneger = GameObject.Find("LvlMeneger").GetComponent<LvlMeneger>();
        MainTrictangle = GameObject.FindWithTag("Player").GetComponent<MainTrictangle>();
        hpInt = 5;
        time = new TimeSpan(0, 0, 0);
        timeStartGame = 1;
        CountCoinI = 10;
        TextTimeTextObj = TextTime.GetComponent<Text>();
        UseWeapon1Clone = UseWeapons.transform.Find($"UseWeapon1").transform.Find($"UseWeapon1Clone").GetComponent<Image>();
        UseWeapon2Clone = UseWeapons.transform.Find($"UseWeapon2").transform.Find($"UseWeapon2Clone").GetComponent<Image>();
    }
    public void ExitToMainMenu()
    {
        StartCoroutineUpdateMatchInfo();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        StartCoroutineUpdateMatchInfo();
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PauseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseCanvas.active)
            {
                Time.timeScale = 0f;
                PauseCanvas.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                PauseCanvas.gameObject.SetActive(false);
            }
        }

        time = TimeSpan.FromSeconds(timeStartGame);

        TextTimeTextObj.text = $"{(time.Minutes >= 10 ? (time.Minutes).ToString() : "0" + (time.Minutes).ToString())} : {(time.Seconds >= 10 ? (time.Seconds).ToString() : "0" + (time.Seconds).ToString())}";
        timeStartGame += Time.deltaTime;

        if (boostBar.fillAmount != 1)
        {
            boostBar.fillAmount += Time.deltaTime / 2;
            if (boostBar.fillAmount > 1f)
                boostBar.fillAmount = 1f;
        }
    }
}
