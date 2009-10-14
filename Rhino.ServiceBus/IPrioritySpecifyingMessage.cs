using System.Messaging;
using System.Linq;
namespace Rhino.ServiceBus
{
    /// <summary>
    /// Interface that should be implemented by every messages wanting to specify a desired
    /// priority level
    /// </summary>
    public interface IPrioritySpecifyingMessage
    {
        MessagePriority DesiredPriority { get; }
    }
    
    public static class PriorityExtensions
    {
        /// <summary>
        /// Helper method for getting the priority of a message
        /// </summary>
        /// <param name="msg">Message to get the priority for</param>        
        public static MessagePriority GetPriorityForMessage(this object msg)
        {
            var priorityMsg = msg as IPrioritySpecifyingMessage;

            if (priorityMsg != null)
            {
                return priorityMsg.DesiredPriority;
            }
            else
            {
                return MessagePriority.Normal;
            }
        }

        public static bool AreSpecifyingSamePriority(this object[] msgs)
        {
            return msgs.Select(msg => msg.GetPriorityForMessage()).Distinct().Count() == 1;
        }
    }
}
