using UnityEngine;
using UnityEngine.UI;       
using System.Collections;

public class NPC : MonoBehaviour
{
    #region 欄位
    public enum state
    {
        start, notComplete, complete
    }
    public state _state;

    [Header("對話")]
    public string sayStart = "可以幫我拿回我的金幣嗎?";
    public string sayNotComplete = "不只有這些喔~";
    public string sayComplete = "感謝你幫我找到所有的金幣!!!";
    [Range(0.001f, 1.5f)]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    public GameObject final;

    #endregion

    #region 事件


    // 2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到物件為"狐狸"
        if (collision.name == "Player")
            Say();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            SayClose();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 對話：打字效果
    /// </summary>
    private void Say()
    {
        // 畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish) _state = state.complete;


        // 判斷式(狀態)
        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog(sayStart));           
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(sayNotComplete));    
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));
                final.SetActive(true);
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                           

        for (int i = 0; i < say.Length; i++)           
        {
            textSay.text += say[i].ToString();          
            
            yield return new WaitForSeconds(speed);     
        }
    }
    #endregion

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }

}