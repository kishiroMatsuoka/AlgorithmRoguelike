using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Encuesta : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI EncuestaRut, EncuestaValue, EncuestaFB;
    [SerializeField] GameObject MainMenu;
    private void OnEnable()
    {
        EncuestaRut.text = "";
        EncuestaRut.text = "";
        EncuestaRut.text = "";
    }
    public void SendData()
    {
        if (EncuestaRut.text.Length > 7)
        {
            StartCoroutine(FeedBack());
            MainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private string url ="https://docs.google.com/forms/u/0/d/e/1FAIpQLSetpQBy8Rk_btJm9Jmt8fu3uKs3GdSbCFlR1SdR68R91JDycg/formResponse";
    IEnumerator FeedBack()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.605464766", EncuestaRut.text);//rut
        form.AddField("entry.1476881134", EncuestaValue.text);//escala
        form.AddField("entry.1495522511", EncuestaFB.text);//feedback
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }
}
