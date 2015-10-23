using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThrFrd.GameTrans.Tool;

namespace ThrFrd.GameTrans.Infrastructure.Component.ShortMessage
{
    public class ShortMessageHandle
    { 
        private ShortMessageContext messageItem;
        public ShortMessageHandle(ShortMessageContext messageItem)
        {
            this.messageItem = messageItem;
        }

        public void Send()
        {
            try
            {
                ShortMessageHelper.Instance.Send(messageItem.Body, messageItem.Way, messageItem.Recipient);
            }
            catch (Exception e)
            { 
            
            }
        }
    }
}
