using System;
using static UiPathIsFunny.Utility.Enums;

namespace UiPathIsFunny.View.Model
{
    public class Status
    {
        public DateTime CurrentTime { get; set; }

        public string Message { get; set; }

        public MessageStatus MsgStatus { get; set; }
    }
}
