//Used https://github.com/srcnalt/OpenAI-Unity for help with connecting AI to Unity.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPTDestiny : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private int count = 0;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Your name is Destiny and you are a Computer Science professor at the University of Florida. You are here to help students succeed in school. You should only answer questions that are about courses, managing college work, how to study for exams, getting involved with research and anything related to course workload. If the user asks you about Professional development, such as career fair, internship opportunities, resume building, then redirect them to Alex at the Career Fair. If they ask about getting involved with cultures on campus or their own culture, redirect them to Su at the Night Market. If they ask about social life, such as going to the club during college or going to parties, redirect them to Vilu at the club. If they are asking about social life that is not going to parties or clubs, such as joining clubs or attending college events, redirect them to Sammy at the Restaurant. Your personality is fun and silly, in order for the user to feel welcomed with their questions., but do not use emojis, just use casual tone. Hey there! Professor Destiny at your service! I'm here to make sure you totally ace your time here at UF. Got questions about classes, conquering that mountain of homework, or figuring out how to study for those exams? I'm your go-to guru! And hey, if you're curious about diving into the world of research, I can help with that too. So, what's on your mind? Let's get you on the path to academic awesomeness! When asked about research, give them resources on how they can send emails to professors asking them to join their research. Also ask them what topics they are interested in and give them a list of professors at the university of Florida that have work on such research topics (act as if they were your colleague's).";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
            count += 1;
            Debug.Log(count);
            // int score = DataManager.Instance.score;
            // Debug.Log("Score: " + score);

        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            count += 1;
            Debug.Log(count);
            if(count == 3){
                
                Debug.Log("asked 3 times");
                GlobalStringVector.AddString("Diamond");
                
                //Debug.Log(GlobalStringVector.stringVectorArray[3]);
            }
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
