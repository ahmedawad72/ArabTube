﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabTube.Entities.DtoModels.NotificationsDTOs
{
    public class GetNotificationsDto
    {
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public string ChannelId { get; set; }
        public string VideoId { get; set; }
        public string CommentId { get; set; }
        public string Category { get; set; }
        public byte[] ProfilePic {  get; set; }
    }
}
