﻿using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLama.Common
{
    /// <summary>
    /// Role of the message author, e.g. user/assistant/system
    /// </summary>
    public enum AuthorRole
    {
        /// <summary>
        /// Role is unknown
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Message comes from a "system" prompt, not written by a user or language model
        /// </summary>
        System = 0,

        /// <summary>
        /// Message comes from the user
        /// </summary>
        User = 1,

        /// <summary>
        /// Messages was generated by the language model
        /// </summary>
        Assistant = 2,
    }

    // copy from semantic-kernel
    /// <summary>
    /// The chat history class
    /// </summary>
    public class ChatHistory
    {
        private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        /// <summary>
        /// Chat message representation
        /// </summary>
        public class Message
        {
            /// <summary>
            /// Role of the message author, e.g. user/assistant/system
            /// </summary>
            [JsonConverter(typeof(JsonStringEnumConverter))]
            [JsonPropertyName("author_role")]
            public AuthorRole AuthorRole { get; set; }

            /// <summary>
            /// Message content
            /// </summary>
            [JsonPropertyName("content")]
            public string Content { get; set; }

            /// <summary>
            /// Create a new instance
            /// </summary>
            /// <param name="authorRole">Role of message author</param>
            /// <param name="content">Message content</param>
            public Message(AuthorRole authorRole, string content)
            {
                AuthorRole = authorRole;
                Content = content;
            }
        }

        /// <summary>
        /// List of messages in the chat
        /// </summary>
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new();

        /// <summary>
        /// Create a new instance of the chat content class
        /// </summary>
        [JsonConstructor]
        public ChatHistory() { }

        /// <summary>
        /// Create a new instance of the chat history from array of messages
        /// </summary>
        /// <param name="messageHistory"></param>
        public ChatHistory(Message[] messageHistory)
        {
            Messages = messageHistory.ToList();
        }

        /// <summary>
        /// Add a message to the chat history
        /// </summary>
        /// <param name="authorRole">Role of the message author</param>
        /// <param name="content">Message content</param>
        public void AddMessage(AuthorRole authorRole, string content)
        {
            Messages.Add(new Message(authorRole, content));
        }

        /// <summary>
        /// Serialize the chat history to JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, _jsonOptions);
        }

        /// <summary>
        /// Deserialize a chat history from JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static ChatHistory? FromJson(string json)
        {
            return JsonSerializer.Deserialize<ChatHistory>(json);
        }
    }
}
