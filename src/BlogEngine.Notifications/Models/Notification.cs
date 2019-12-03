using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Notifications.Models
{
    public class Notification
    {
        /// <summary>
        /// Notification subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Notification content
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Notification recipients
        /// </summary>
        public string[] Recipients { get; set; }
    }
}
