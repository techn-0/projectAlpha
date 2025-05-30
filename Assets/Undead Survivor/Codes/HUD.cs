using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExo = GameManager.instance.exp;
                float maxExo = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExo / maxExo;
                break;
            case InfoType.Level:
            myText.text = "Lv." + GameManager.instance.level.ToString();
                break;
            case InfoType.Kill:
                myText.text = GameManager.instance.kill.ToString();
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                break;
        }
    }
}
