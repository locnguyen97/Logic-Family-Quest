using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHumanUI : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Image avatar;
    private bool canDrag = true;

    private Vector2 curentPos;
    public HumanType humanType;
    private bool isDraging;
    [SerializeField] private TextMeshProUGUI txtname;

    public void SetData(GameManager gameManager, string name, Sprite av, HumanType type)
    {
        avatar.sprite = av;
        txtname.text = name;
        _gameManager = gameManager;
        curentPos = transform.position;
        humanType = type;
        canDrag = true;
    }

    public void DragHandle(BaseEventData data)
    {
        if (!canDrag) return;
        transform.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        isDraging = true;
        var pointerEventData = (PointerEventData) data;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) _gameManager.canvas.transform,
            pointerEventData.position,
            _gameManager.canvas.worldCamera,
            out pos);
        transform.position = _gameManager.canvas.transform.TransformPoint(pos);
    }

    public void SetDonePos()
    {
        canDrag = false;
    }

    public void OnPointUp(BaseEventData data)
    {
        if (!canDrag) return;
        if (isDraging)
        {
            transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;
            var pointerEventData = (PointerEventData) data;
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) _gameManager.canvas.transform,
                pointerEventData.position,
                _gameManager.canvas.worldCamera,
                out pos);
            Debug.Log(pos);
            var val = _gameManager.CheckPos(pos);
            if (val >= 0 && val <= 4)
            {
                if ((val == 0 && humanType == HumanType.Father) || (val == 1 && humanType == HumanType.Mother) || 
                    (val == 2 && humanType == HumanType.Child1) || (val == 3 && humanType == HumanType.Child2) ||
                    (val == 4 && humanType == HumanType.Child3) )
                {
                    transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
                    transform.parent = _gameManager.GetParent(val, transform);
                    transform.localPosition = Vector3.zero;
                    SetDonePos();
                    _gameManager.CheckWin(val);
                }
                else
                {
                    if (_gameManager.CheckLogic())
                    {
                        transform.position = curentPos;
                        transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
                    }
                }
            }
            else
            {
                transform.position = curentPos;
                transform.GetComponent<RectTransform>().localScale = Vector3.one;
                transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;
            }
        }
        
    }
}
