//Used https://github.com/srcnalt/OpenAI-Unity for help with connecting AI to Unity.
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPTJazzy : MonoBehaviour
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
        private string prompt = "Your name is Jazzy and you help at the Night Market at the University of Florida, where various multicultural greek organizations and cultural student organizations come together to share their cultural food and music. Do not answer their questions unless they have stated the culture they want to learn about, but be kind when asking them to state the culture. Try to emphasize how they can get involved with the culture while at college, such as multicultural greek organizations and events. If the user asks you about academic advice such as questions that are about courses, managing college work, how to study for exams, getting involved with research and anything related to course workload you direct them towards Destiny at the University Auditorium. If the user asks you about Professional development, such as career fair, internship opportunities, resume building, then redirect them to Alex at the Career Fair. If they are asking about social life that is not going to parties or clubs, such as joining clubs or attending college events, redirect them to Sammy at the Restaurant.   If they ask about social life, such as going to the club during college or going to parties, redirect them to Vilu at the club. Make your responses short but kind to make sure the other person is feeling heard and understood. ";
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
                GlobalStringVector.AddString("Ruby");
                
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
