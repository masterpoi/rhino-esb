using System;
using System.Threading;
using Xunit;
using Rhino.ServiceBus.Msmq;
using Rhino.ServiceBus.Serializers;
using Rhino.ServiceBus.Impl;
using Castle.MicroKernel;
using System.Transactions;
using System.Collections;
using System.Messaging;

namespace Rhino.ServiceBus.Tests
{
    public class SendAndReceivePrioritizedMessages : MsmqTestBase
    {
        [Fact]
        public void Send_high_Prioirty_messages_has_priority()
        {            
            var waitHandle = new ManualResetEvent(false);

            var ok = false;
            
            Transport.MessageArrived += msg =>
            {
                ok = ((msg as MsmqCurrentMessageInformation).MsmqMessage as Message).Priority != MessagePriority.Normal;
                waitHandle.Set();
                return true;
            };

            Transport.Send(TestQueueUri, new object[] { new PriorityTestMessage() });
       
            waitHandle.WaitOne(TimeSpan.FromSeconds(30), false);

            Assert.True(ok);
        }

        public class PriorityTestMessage : IPrioritySpecifyingMessage
        {


            public System.Messaging.MessagePriority DesiredPriority
            {
                get { return System.Messaging.MessagePriority.AboveNormal; }
            }
        }
    
    }
}
