using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    

    private DataGame dataGame = new DataGame();
    private static List<string> listNameMan = new List<string>(){"John","Alex","Andrew","Christian","Daniel","Harry","Logan","Leo","Ryan","Hugo","Connor","Maverick","Lucas","Jake","Charlie"};
    private static List<string> listNameWoman = new List<string>(){"Helen","Amanda","Serena","Gwen","Larissa","Jocelyn","Desi","Kane","Vera","Ellen","Lucinda","Mei","Elise","Nomi","Diana"};
    
    
    [SerializeField] private ItemInforUI itemInfor1, itemInfor2, itemInfor3;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private TextMeshProUGUI txtTym;
    [SerializeField] private List<Sprite> listAvatarBoy;
    [SerializeField] private List<Sprite> listAvatarMan;
    [SerializeField] private List<Sprite> listAvatarWoman;
    [SerializeField] private List<Sprite> listAvatarGirl;
    [SerializeField] private Transform parentListHuman;
    [SerializeField] private ItemHumanUI itemHumanUi;
    [SerializeField] private Transform fatherTrans;
    [SerializeField] private Transform motherTrans;
    [SerializeField] private Transform c1Trans;
    [SerializeField] private Transform c2Trans;
    [SerializeField] private Transform c3Trans;
    private int gameLive = 3;

    [SerializeField] private TextMeshProUGUI txtCurLevel;
    [SerializeField] private TextMeshProUGUI txtNextLevel;
 
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private Button btnReplay;
    [SerializeField] private Button btnNext;

    [SerializeField] private Animator ft, mt, c1, c2, c3;

    public Canvas canvas;
    
    private void Start()
    {
        btnNext.onClick.AddListener(() =>
        {
            win.SetActive(false);
            lose.SetActive(false);
            ReloadGame();
        });
        btnReplay.onClick.AddListener(() =>
        {
            win.SetActive(false);
            lose.SetActive(false);
            ReloadGame();
            
        });
        GenerateDataGame();
        SetDataInfor();
        LoadPlayerData();
        InitListHuman();
    }

    void ReloadGame()
    {
        foreach (Transform tr in parentListHuman)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in fatherTrans)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in motherTrans)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in c1Trans)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in c2Trans)
        {
            Destroy(tr.gameObject);
        }
        foreach (Transform tr in c3Trans)
        {
            Destroy(tr.gameObject);
        }
        GenerateDataGame();
        SetDataInfor();
        LoadPlayerData();
        InitListHuman();
    }

    public void CheckWin( int slot)
    {
        if (slot == 0)
        {
            ft.SetTrigger("show");
        }
        if (slot == 1)        {
            mt.SetTrigger("show");
        }
        if (slot == 2)        {
            c1.SetTrigger("show");
        }
        if (slot == 3)        {
            c2.SetTrigger("show");
        }
        if (slot == 4)        {
            c3.SetTrigger("show");
        }
        if (fatherTrans.childCount > 0 && motherTrans.childCount > 0 && c1Trans.childCount > 0 &&
            c2Trans.childCount > 0 && c3Trans.childCount > 0)
        {
            ShowWin();
        }
    }
    

    public bool CheckLogic()
    {
        gameLive--;
        txtTym.text = gameLive.ToString();
        if (gameLive < 0)
        {
            ShowLose();
            return false;
        }
        else return true;
    }

    void ShowLose()
    {
        lose.SetActive(true);
    }
    void ShowWin()
    {
        int coin = PlayerPrefs.GetInt("playerCoin", 0);
        coin += 10;
        PlayerPrefs.SetInt("playerCoin", coin);
        int level = PlayerPrefs.GetInt("level", 0)+1;
        level = level > 3 ? 1 : level;
        PlayerPrefs.SetInt("level",level);
        win.SetActive(true);
    }

    void InitListHuman()
    {
        foreach (Transform tr in parentListHuman)
        {
            Destroy(tr.gameObject);
        }
        List<int> list = new List<int>(){1,2,3,4};
        Sprite avatar;
        while (list.Count >0)
        {
            int so = list[Random.Range(0, list.Count)];
            list.Remove(so);
            if (so == 1)
            {
                if (isShowMan)
                {
                    var ft = Instantiate(itemHumanUi, fatherTrans);
                    ft.SetData(this,dataGame.father.nameHuman,listAvatarMan[Random.Range(0,listAvatarMan.Count)],HumanType.Father);
                    ft.SetDonePos();
                    ft.GetComponent<RectTransform>().localScale = new Vector3(1.2f,1.2f,1.2f);
                    var mt = Instantiate(itemHumanUi, parentListHuman);
                    mt.SetData(this,dataGame.mother.nameHuman,listAvatarWoman[Random.Range(0,listAvatarWoman.Count)],HumanType.Mother);
                }
                else
                {
                    var ft = Instantiate(itemHumanUi, parentListHuman);
                    ft.SetData(this,dataGame.father.nameHuman,listAvatarMan[Random.Range(0,listAvatarMan.Count)],HumanType.Father);
                    var mt = Instantiate(itemHumanUi, motherTrans);
                    mt.SetData(this,dataGame.mother.nameHuman,listAvatarWoman[Random.Range(0,listAvatarWoman.Count)],HumanType.Mother);
                    mt.SetDonePos();
                    mt.GetComponent<RectTransform>().localScale = new Vector3(1.2f,1.2f,1.2f);
                }
            }

            if (so == 2)
            {
                var c1 = Instantiate(itemHumanUi, parentListHuman);
                if (dataGame.child1.isAMan)
                {
                    avatar = listAvatarBoy[Random.Range(0, listAvatarBoy.Count)];
                    listAvatarBoy.Remove(avatar);
                }
                else
                {
                    avatar = listAvatarGirl[Random.Range(0, listAvatarGirl.Count)];
                    listAvatarGirl.Remove(avatar);
                }
                c1.SetData(this,dataGame.child1.nameHuman,avatar,HumanType.Child1);
            }

            if (so == 3)
            {
                var c2 = Instantiate(itemHumanUi, parentListHuman);
                if (dataGame.child2.isAMan)
                {
                    avatar = listAvatarBoy[Random.Range(0, listAvatarBoy.Count)];
                    listAvatarBoy.Remove(avatar);
                }
                else
                {
                    avatar = listAvatarGirl[Random.Range(0, listAvatarGirl.Count)];
                    listAvatarGirl.Remove(avatar);
                }
        
                c2.SetData(this,dataGame.child2.nameHuman,avatar,HumanType.Child2);
            }

            if (so == 4)
            {
                var c3 = Instantiate(itemHumanUi, parentListHuman);
                if (dataGame.child3.isAMan)
                {
                    avatar = listAvatarBoy[Random.Range(0, listAvatarBoy.Count)];
                    listAvatarBoy.Remove(avatar);
                }
                else
                {
                    avatar = listAvatarGirl[Random.Range(0, listAvatarGirl.Count)];
                    listAvatarGirl.Remove(avatar);
                }
        
                c3.SetData(this,dataGame.child3.nameHuman,avatar,HumanType.Child3);
            }
        }
    }

    void LoadPlayerData()
    {
        gameLive = 3;
        txtTym.text = gameLive.ToString();
        txtCoin.text = PlayerPrefs.GetInt("playerCoin", 0).ToString();
        int level = PlayerPrefs.GetInt("level", 0)+1;
        txtCurLevel.text = level.ToString();
        txtNextLevel.text = (level + 1).ToString();
    }

    public int CheckPos(Vector2 pos)
    {
        var p = fatherTrans.position;
        var q = canvas.transform.TransformPoint(pos);
        if(Vector2.Distance(p,q) <= 100 && fatherTrans.childCount ==0) return 0;
        p = motherTrans.position;
        if(Vector2.Distance(p,q) <= 100 && motherTrans.childCount ==0) return 1;
        p = c1Trans.position;
        if(Vector2.Distance(p,q) <= 100 && c1Trans.childCount ==0) return 2;
        p = c2Trans.position;
        if(Vector2.Distance(p,q) <= 100 && c2Trans.childCount ==0) return 3;
        p =c3Trans.position;
        if(Vector2.Distance(p,q) <= 100 && c3Trans.childCount ==0) return 4;
        return 10;
    }

    public  Transform GetParent(int slot , Transform tr)
    {
        switch (slot)
        {
            case 0 : return fatherTrans;
            case 1 : return motherTrans;
            case 2 : return c1Trans;
            case 3 : return c2Trans;
            case 4 : return c3Trans;
            default: return null;
        }
    }

    void GenerateDataGame()
    {
        dataGame = new DataGame();
        string name = listNameMan[Random.Range(0, listNameMan.Count)];
        dataGame.father = new Human();
        dataGame.father.nameHuman = name;
        dataGame.father.isAMan = true;
        listNameMan.Remove(name);
        
        dataGame.mother = new Human();
        name = listNameWoman[Random.Range(0, listNameWoman.Count)];
        dataGame.mother.nameHuman = name;
        dataGame.mother.isAMan = false;
        listNameWoman.Remove(name);
        
        dataGame.child1 = new Human();
        bool isAMan = Random.Range(0, 2) == 1;
        if (isAMan)
        {
            dataGame.child1 = new Human();
            name = listNameMan[Random.Range(0, listNameMan.Count)];
            dataGame.child1.nameHuman = name;
            dataGame.child1.isAMan = true;
            listNameMan.Remove(name);
        }
        else
        {
            dataGame.child1 = new Human();
            name = listNameWoman[Random.Range(0, listNameWoman.Count)];
            dataGame.child1.nameHuman = name;
            dataGame.child1.isAMan = false;
            listNameWoman.Remove(name);
        }
        
        dataGame.child2 = new Human();
        isAMan = Random.Range(0, 2) == 1;
        if (isAMan)
        {
            dataGame.child2 = new Human();
            name = listNameMan[Random.Range(0, listNameMan.Count)];
            dataGame.child2.nameHuman = name;
            dataGame.child2.isAMan = true;
            listNameMan.Remove(name);
        }
        else
        {
            dataGame.child2 = new Human();
            name = listNameWoman[Random.Range(0, listNameWoman.Count)];
            dataGame.child2.nameHuman = name;
            dataGame.child2.isAMan = false;
            listNameWoman.Remove(name);
        }
        
        dataGame.child3 = new Human();
        isAMan = Random.Range(0, 2) == 1;
        if (isAMan)
        {
            dataGame.child3 = new Human();
            name = listNameMan[Random.Range(0, listNameMan.Count)];
            dataGame.child3.nameHuman = name;
            dataGame.child3.isAMan = true;
            listNameMan.Remove(name);
        }
        else
        {
            dataGame.child3 = new Human();
            name = listNameWoman[Random.Range(0, listNameWoman.Count)];
            dataGame.child3.nameHuman = name;
            dataGame.child3.isAMan = false;
            listNameWoman.Remove(name);
        }
    }

    private bool isShowMan = true;
    void SetDataInfor()
    {
        isShowMan = Random.Range(0, 2) == 1;
        if (!isShowMan)
        {
            int slot1 = Random.Range(0, 3);
            itemInfor1.SetData(dataGame.father.nameHuman,slot1 == 0 ?dataGame.child1.nameHuman:slot1 == 1 ?dataGame.child2.nameHuman: dataGame.child3.nameHuman ,"'s father",2);
        }
        else
        {
            int slot1 = Random.Range(0, 3);
            itemInfor1.SetData(dataGame.mother.nameHuman,slot1 == 0 ?dataGame.child1.nameHuman:slot1 == 1 ?dataGame.child2.nameHuman: dataGame.child3.nameHuman,"'s mother",2);
        }
        
        int slot2 =Random.Range(0, 2);
        if (slot2 == 0)
        {
            itemInfor2.SetData(dataGame.child3.nameHuman," is the youngest child","",3);
            int slot3 =Random.Range(0, 2);
            if (slot3 == 0)
            {
                itemInfor3.SetData(dataGame.child2.nameHuman," is younger than ", dataGame.child1.nameHuman,3);
            }
            else
            {
                itemInfor3.SetData(dataGame.child1.nameHuman," is the older than ", dataGame.child2.nameHuman,3);
            }
        }
        else
        {
            itemInfor2.SetData(dataGame.child1.nameHuman," is the oldest child","",3);
            int slot3 =Random.Range(0, 2);
            if (slot3 == 0)
            {
                itemInfor3.SetData(dataGame.child3.nameHuman," is younger than ", dataGame.child2.nameHuman,3);
            }
            else
            {
                itemInfor3.SetData(dataGame.child2.nameHuman," is the older than ", dataGame.child3.nameHuman,3);
            }
        }
        
        
        
        
    }
    
    [Serializable]
    public class  DataGame {
        public Human father;
        public Human mother;
        public Human child1;
        public Human child2;
        public Human child3;
        
    }
    
    [Serializable]
    public class Human
    {
        public string nameHuman;
        public bool isAMan = true;
    }

    
}


[Serializable]

public enum HumanType
{
    Father,
    Mother,
    Child1,
    Child2,
    Child3
}