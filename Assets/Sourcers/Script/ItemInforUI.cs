using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInforUI : MonoBehaviour
{
    public int id = 0;
    [SerializeField] private GameObject iconPass,iconFailed;
    public TextMeshProUGUI txtName1;
    public TextMeshProUGUI txtName2;
    public TextMeshProUGUI txtName3;
    public void CheckPass(int value = -1)
    {
        if (value == 0)
        {
            iconPass.SetActive(true);
            iconFailed.SetActive(false);
        }
        else if (value == 1)
        {
            iconPass.SetActive(false);
            iconFailed.SetActive(true);
        }
        else
        {
            iconPass.SetActive(false);
            iconFailed.SetActive(false);
        }
    }

    public void SetData(string name1, string name2 , string name3 , int indexParam2 )
    {
        txtName1.text = name1;
        if (indexParam2 == 2)
        {
            txtName2.fontStyle = FontStyles.Underline;
            txtName3.fontStyle = FontStyles.Normal;
            txtName2.text = name2;
            txtName3.text = name3;
        }
        else
        {
            txtName3.fontStyle = FontStyles.Underline;
            txtName2.fontStyle = FontStyles.Normal;
            txtName2.text = name2;
            txtName3.text = name3;
        }
    }
}
