using Game;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;

    public Button nextButton;
    public TextMeshProUGUI dialogText;
    public CanvasGroup textZone;
    public GameObject enemyTutorPlanet;
    public Slider balancePower;

    private string[] dialogList;
    private bool isTyping = false;
    private bool canSkip = true;
    private bool isFillBalancePower = false;
    private float speedText = 0.09f;
    public int index = 0;

    private void Awake()
    {
        dialogList = new string[7] { "����������� �����, ��� ��� ������ �� ����� �� ������ ����������.\r\n������ � ����� ���� � ���� ����..", //0
                                     "�� ������ ��������� ��������� ����� �������.\r\n�������� ������ ��� � �������� �������.", //1
                                     "�� ������ ������� ���� �����, �������� �� ��������� ��������, � ����������� �� ������� �������. ��� ������ �������, ��� ������� ������������ �����.", //2
                                     "������� �� ������ ����������� ����� ����� ��� � ��� ����������.\r\n����� �� ����, �� ��������� ����� ��������� ��� �����������.", //3
                                     "������� ������ �� ��������� ������� ��� �������. (����� ��� � ������ ������� ���������������, ���� ������ ���)\r\n�������� ������ ������ �� ���!", //4
                                     "������ ������������ ��� �� �������, ������� ������ ��� ���������� ����!", //5
                                     "���������! �� ������� ��������� �������� ������������.\r\n����� �� ������ finish.", //6
        };

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        dialogText.text = string.Empty;
        CosmoCat.Instance.ShowCC();

        StartCoroutine(RiseTextZone());
        Invoke("StartDialog", 3);
    }
    private void Update()
    {
        if (index == 1 || index == 4 || index == 5)
        {
            canSkip = false;
        }
        else if (index == 3 && !isFillBalancePower)
        {
            canSkip=false;
            StartCoroutine(FillBalancePower());
            isFillBalancePower=true;
        }
        else if (index == dialogList.Length - 1)
        {
            Debug.Log("End tutor");
        }
    }
    IEnumerator RiseTextZone()
    {
        yield return new WaitForSeconds(2f);

        while (textZone.alpha < 1)
        {
            yield return new WaitForSeconds(0.1f);

            textZone.alpha += 0.1f;
        }
    }
    private void StartDialog()
    {
        StartCoroutine(TypeLine());
    }
    IEnumerator FillBalancePower()
    {
        while (balancePower.value < 1) 
        {
            balancePower.value += 0.005f;
            yield return new WaitForSeconds(0.025f);  
        }

        yield return new WaitForSeconds(0.45f);

        while (balancePower.value > 0.5)
        {
            balancePower.value -= 0.005f;
            yield return new WaitForSeconds(0.025f);
        }
        canSkip = true;
        TutorSelectManager.Instance.canSendUnits = true;
    }
    public void RisePlanet()
    {
        StartCoroutine(RiseEnemyPlanet());
    }
    IEnumerator RiseEnemyPlanet()
    {
        SpriteRenderer spriteRenderer = enemyTutorPlanet.GetComponent<SpriteRenderer>();
        TextMeshProUGUI text = enemyTutorPlanet.GetComponentInChildren<TextMeshProUGUI>();

        Color startColor = spriteRenderer.color;

        while (startColor.a < 1)
        {
            yield return new WaitForSeconds(0.085f);

            text.alpha += 0.1f;
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a += 0.1f);

            yield return null;
        }
    }
    public void NextDialog()
    {
        if (index < dialogList.Length - 1)
        {
            dialogText.text = string.Empty;
            index++;
            StartCoroutine(TypeLine());
        }
    }
    public void SkipText()
    {
        if (isTyping)
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = ">> next";
            isTyping = false;

            dialogText.text += dialogList[index].Substring(dialogText.text.Length);
            CosmoCat.Instance.IdleCC();
        }
        else if (!isTyping && index >= 6)
        {
            EndTutorial();
        }
        else if (!isTyping && canSkip)
        {
            NextDialog();
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = ">> skip";
        }
    }
    IEnumerator TypeLine()
    {
        canSkip = true;
        isTyping = true;
        CosmoCat.Instance.TalkCC();

        foreach (var c in dialogList[index].ToCharArray())
        {
            if (isTyping)
            {
                dialogText.text += c;
                yield return new WaitForSeconds(speedText);
            }
            else break;
        }
        isTyping = false;
        if (index == dialogList.Length - 1)
        { 
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = ">> finish";
        }
        else nextButton.GetComponentInChildren<TextMeshProUGUI>().text = ">> next";

        CosmoCat.Instance.IdleCC();
    }
    private void EndTutorial()
    {
        SceneManager.LoadScene(0);
    }
}
