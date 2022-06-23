using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject failBackground;
    [SerializeField] private GameObject successBackground;
    [SerializeField] private GameObject tapToPlayArea;
    [SerializeField] private GameObject inGameArea;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject successArea;
    [SerializeField] private GameObject failArea;

    [SerializeField] private Image damageImage;
    private float damageImageFilledValue;

    private Coroutine showUICoroutine;

    private void Awake()
    {
        Instance = this;
;
    }

    public void ResetUI()
    {
        damageImageFilledValue = 1f;
        damageImage.fillAmount = damageImageFilledValue;

        CheckDamageImageColor();
    }

    public void ShowUI()
    {
        failBackground.SetActive(false);

        successBackground.SetActive(false);

        tapToPlayArea.SetActive((GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().tapToPlayState) && (GameManager.Instance.level < 2));

        inGameArea.SetActive(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().playState);

        successArea.SetActive(false);

        failArea.SetActive(false);

        if(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().failState||
            GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().finishState)
        {

            if(showUICoroutine!=null)
            {
                StopCoroutine(showUICoroutine);
            }
            showUICoroutine=StartCoroutine(ShowUICoroutine());
        }

    }

    public void NextLevelOnUI(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.level = GameManager.Instance.level + 1;
        }

        GameManager.Instance.StartLevelStartAction();
    }
    public IEnumerator ShowUICoroutine()
    {
        yield return new WaitForSeconds(1f);
        successBackground.SetActive(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().finishState);
        successArea.SetActive(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().finishState);

        failBackground.SetActive(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
    GameManager.Instance.GetGameStateMachine().failState);
        failArea.SetActive(GameManager.Instance.GetGameStateMachine().GetCurrentState() ==
            GameManager.Instance.GetGameStateMachine().failState);
    }

    public void ShowLevel()
    {
        levelText.text = "Level " + GameManager.Instance.level;
    }
    public void SetDamageImage(ref float _durability)
    {
        DOTween.Kill("DamageImageTween");

        damageImageFilledValue = _durability / 100f;

        DOTween.To(() => damageImage.fillAmount, x => damageImage.fillAmount = x, damageImageFilledValue, 1f).
                OnUpdate(() =>CheckDamageImageColor()).
                OnComplete(()=>CarManager.Instance.CheckDurability()).
                SetId("DamageImageTween");
    }
    private void CheckDamageImageColor()
    {
        damageImage.color= Color.Lerp(Color.red, Color.green, damageImage.fillAmount);
    }
}
