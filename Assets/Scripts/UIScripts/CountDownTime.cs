using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownTime : MonoBehaviour
{
    [SerializeField] Image TimeImage;
    [SerializeField] Text TimeText;
    [SerializeField] float Duration, CurrentTime;

    private void Start()
    {
        CurrentTime = Duration;
        TimeText.text = "0:"+CurrentTime.ToString();
        StartCoroutine(TimeEnd());
    }

    IEnumerator TimeEnd()
    {
        while(CurrentTime>=0)
        {
            TimeImage.fillAmount = Mathf.InverseLerp(0, Duration, CurrentTime);
            TimeText.text = "0:"+CurrentTime.ToString();
            yield return new WaitForSeconds(1f);
            CurrentTime--;
        }

        Debug.Log("END GAME");
    }   
    
    
}
