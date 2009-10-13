using System;
using System.Threading;
using Xunit;

namespace Rhino.ServiceBus.Tests
{
    public class SendAndReceivePrioritizedMessages : MsmqTestBase
    {
        [Fact]
        public void Send_high_Prioirty_messages_has_priority()
        {
            TestMessage receivedMsg = null;
            var waitHandle = new AutoResetEvent(false);
            var today = DateTime.Today;
            int count = 0, priorityMsg = int.MinValue, normalMsg = int.MinValue;
            Transport.MessageArrived += msg =>
            {
                if (msg is PriorityTestMessage)
                {
                    priorityMsg = count;
                }
                else
                {
                    normalMsg = count;
                }
                waitHandle.Set();
                return true;
            };
        }

        public class PriorityTestMessage : IPrioritySpecifyingMessage
        {


            public System.Messaging.MessagePriority DesiredPriority
            {
                get { return System.Messaging.MessagePriority.AboveNormal; }
            }
        }
        public class TestMessage
        {

        }
    }
}
