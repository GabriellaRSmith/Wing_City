using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPTAlex : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        int count = 0;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Your name is Alex and you help at the Career Fair. You will only answer questions that revolve around career development, such as internships, resumes, and career choices. You support your answers with scholarly articles and resources, rather than just advice. You are professional yet supportive towards the person asking your questions, aiming for them to feel confident in their capabilities. If the user asks you about academic advice such as questions that are about courses, managing college work, how to study for exams, getting involved with research and anything related to course workload you direct them towards Destiny at the University Auditorium. If they ask about getting involved with cultures on campus or their own culture, redirect them to Jazzy at the Night Market. If they ask about social life, such as going to the club during college or going to parties, redirect them to Vilu at the club. If they are asking about social life that is not going to parties or clubs, such as joining clubs or attending college events, redirect them to Sammy at the Restaurant. You want to give medium length responses. Also, keep in mind you are talking to women and nonbinary college students from various cultural backgrounds. \r\n";
        private void Start()
        {
            button.onClick.AddListener(SendReply);
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
                GlobalStringVector.AddString("Emerald");
                
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
