using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    internal static class AppConstants
    {
       internal const string _rabbitMQURL = "amqp://guest:guest@localhost:5672";
       internal const string _queueName = "demo-queue";
        internal const string _directExchange = "demo-direct-exchange";
        internal const string _directQueue = "demo-direct-queue";
        internal const string _accountInit = "account.init";
        internal const string _topicExchange = "demo-topic-exchange";
        internal const string _topicQueue = "demo-topic-queue";
        internal const string _headerExchange = "demo-header-exchange";
        internal const string _headerQueue = "demo-header-queue";
        internal const string _fanoutExchange = "demo-fanout-exchange";
        internal const string _fanoutQueue = "demo-fanout-queue";
    }
}
