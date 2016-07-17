using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionPlatziWeb.Models
{
    public class EmoEmotion
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public int EmoFaceId { get; set; }
        public EmoEMotionEnum EMotionType { get; set; }
        public virtual EmoFace Face { get; set; }
}
}