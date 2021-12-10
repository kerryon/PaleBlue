using System;
using UnityEngine;

public class PieMenu : MonoBehaviour
{
    public Pie Data;
    public PiePiece PiePiecePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected PiePiece[] Pieces;
    protected PieMenu Parent;
    public string Path;

    public RectTransform parentCanvas;

    void Start()
    {
        float stepLength = 360f / Data.Elements.Length;
        //PiePiecePrefab.Icon.transform.position >>> Vector3.Distance(new Vector3(0, Screen.width/5, 0), PiePiecePrefab.CakePiece.transform.position)
        float iconDist = parentCanvas.sizeDelta.x / 3;

        //Position it
        Pieces = new PiePiece[Data.Elements.Length];

        for (int i = 0; i < Data.Elements.Length; i++)
        {
            Pieces[i] = Instantiate(PiePiecePrefab, transform);
            //set root element
            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;

            //set cake piece
            Pieces[i].CakePiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Pieces[i].CakePiece.transform.localPosition = Vector3.zero;
            Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength);

            //set icon
            Pieces[i].Icon.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;
            Pieces[i].CakePiece.color = new Color32(32, 32, 32, (byte)UnityEngine.Random.Range(0, 250));
        }
    }

    public void SelectPie(int index)
    {
        string path = Path + "/" + Data.Elements[index].pathName;
        if (Data.Elements[index].NextPie != null)
        {
            var newSubRing = Instantiate(gameObject, transform.parent).GetComponent<PieMenu>();
            newSubRing.Parent = this;
            for (int j = 0; j < newSubRing.transform.childCount; j++)
            {
                Destroy(newSubRing.transform.GetChild(j).gameObject);
            }
            newSubRing.name = PiePiecePrefab.name;
            newSubRing.Data = Data.Elements[index].NextPie;
            newSubRing.Path = path;
            newSubRing.callback = callback;
        }
        else
        {
            callback?.Invoke(path);
            //parentCanvas.parent.gameObject.GetComponent<Interface>().ActionToggle(); //MEHR INFOS ZUR AKTION ÖFFNEN !
        }

        if (transform.GetSiblingIndex() != 0)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    float stepLength = 360f / Data.Elements.Length;
    //    Vector3 screenPoint = Input.mousePosition;
    //    screenPoint.z = UICam.nearClipPlane;
    //    float mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
    //    //float mouseAngle = NormalizeAngle((Mathf.Atan2(UICam.ScreenToWorldPoint(screenPoint).y, UICam.ScreenToWorldPoint(screenPoint).x) * Mathf.Rad2Deg) + stepLength / 2f);
    //    int activeElement = (int)(mouseAngle / stepLength);
    //    for (int i = 0; i < Data.Elements.Length; i++)
    //    {
    //        if (i == activeElement)
    //            Pieces[i].CakePiece.color = new Color32(17,17,17, 254);
    //        else
    //            Pieces[i].CakePiece.color = new Color(0, 0, 0, 0);
    //    }


    //    if (Input.GetMouseButtonDown(0)) // + Touch einbauen !!!
    //    {
    //        string path = Path + "/" + Data.Elements[activeElement].Name;
    //        if (Data.Elements[activeElement].NextPie != null)
    //        {
    //            var newSubRing = Instantiate(gameObject, transform.parent).GetComponent<PieMenu>();
    //            newSubRing.Parent = this;
    //            for (int j = 0; j < newSubRing.transform.childCount; j++)
    //            {
    //                Destroy(newSubRing.transform.GetChild(j).gameObject);
    //            }
    //            newSubRing.name = PiePiecePrefab.name;
    //            newSubRing.Data = Data.Elements[activeElement].NextPie;
    //            newSubRing.Path = path;
    //            newSubRing.callback = callback;
    //        }
    //        else
    //        {
    //            callback?.Invoke(path);
    //        }
            
    //        if (transform.GetSiblingIndex() != 0)
    //        {
    //            Destroy(gameObject);
    //        } else
    //        {
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    //private float NormalizeAngle(float a) => (a + 360f) % 360f;
}
