using System.Collections;
using System.Text;
using Highlands.Server;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChannelController : MonoBehaviour
{
    // User 
    [SerializeField] private GameObject[] playerCard;

    // Chatting
    [SerializeField] private GameObject scrollView;
    [SerializeField] private TMP_Text chattingMessage;
    [SerializeField] private TMP_InputField chattingInput;
    [SerializeField] private Button chattingSendButton;

    // Map 
    [SerializeField] private Image mapImage;
    [SerializeField] private TMP_Dropdown dropdown;

    // Button
    [SerializeField] private Button characterSelectButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        var nickname = playerCard[0].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        nickname.text = "asdf";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chattingInput.isFocused)
            {
                Debug.Log(chattingInput.text);
                // 채팅창 포커스인 경우 채팅 보내기
                SendChattingMessage();
            }
            else
            {
                // 채팅창 포커스 시키기
                chattingInput.Select();
            }
        }
    }

    #region 채팅

    private void SendChattingMessage()
    {
        // var myName = GameManager.Instance.loginUserInfo.NickName;
        // var myName = "test";
        // int channelIndex = 0; // 받아와야 되요 ㅎ;
        // var buffer = MessageHandler.PackChatMessage(chattingInput, channelIndex, myName);
        Debug.Log(chattingInput.text);
        chattingInput.text = "";
        // NetworkManager.Instance.SendChatMessage(buffer);
    }

    public void UpdateChatMessage(ChatMessage message)
    {
        var sb = new StringBuilder();
        var myName = GameManager.Instance.loginUserInfo.NickName;

        if (myName.Equals(message.UserName))
        {
            sb.Append(message.UserName).Append("(나): ").Append(message.Message);
        }
        else
        {
            sb.Append(message.UserName).Append(": ").Append(message.Message);
        }

        Transform content = scrollView.transform.Find("Viewport/Content");
        TMP_Text temp = Instantiate(chattingMessage, content, false);

        temp.text = sb.ToString();

        // 20개 이상 위에서부터 제거
        if (content.childCount >= 20)
        {
            Destroy(content.GetChild(0).gameObject);
        }

        StartCoroutine(ScrollToBottom(scrollView));
    }

    IEnumerator ScrollToBottom(GameObject scrollView)
    {
        yield return null;

        var content = scrollView.transform.Find("Viewport/Content");
        
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);
        
        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    #endregion
    
}