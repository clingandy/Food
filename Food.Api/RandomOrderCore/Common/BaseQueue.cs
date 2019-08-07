using RandomOrderCore.Domains.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace RandomOrderCore.Common
{
    public class BaseQueue<T> where T : class, new()
    {
        private BatchBlock<T> _saveMsgBatchBlock;
        private const int ProcessingMsg = 1;    //正在处理：保存消息数据
        private const int UnProcessingMsg = 0;  //没有处理：保存消息数据
        private int _isProcessingMsg;  //是否正在处理：保存消息数据
        private int _triggerSpanTime = 0;    // 触发生效时间

        public event Action<IEnumerable<T>> ProcessItemFunction;     //处理方法
        public event EventHandler<EventArgs> ProcessException;       //异常处理方法

        /// <summary>
        /// 初始化队列
        /// </summary>
        /// <param name="triggerSpanTime">多少毫秒后触发处理</param>
        /// <param name="batchCount">合并多少数据后处理</param>
        public BaseQueue(int triggerSpanTime, int batchCount)
        {
            var saveMsgActionBlock = new ActionBlock<T[]>(entityArray =>
            {
                try
                {
                    ProcessItemFunction(entityArray.ToList());
                }
                catch (System.Exception ex)
                {
                    OnProcessException(ex, entityArray);
                }
            });

            _triggerSpanTime = triggerSpanTime < 500 ? 500 : triggerSpanTime;

            // 合并N条后保存
            _saveMsgBatchBlock = new BatchBlock<T>(batchCount);
            _saveMsgBatchBlock.LinkTo(saveMsgActionBlock);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="ex"></param>
        private void OnProcessException(System.Exception ex, T[] array)
        {
            var tempException = ProcessException;
            Interlocked.CompareExchange(ref ProcessException, null, null);

            if (tempException != null)
            {
                ProcessException(ex, new EventArgs<IEnumerable<T>>(array.ToList()));
            }
        }

        /// <summary>
        /// N秒后处理BatchBlock对象的消息数据
        /// </summary>
        private void TriggerBatch()
        {
            if (Interlocked.CompareExchange(ref _isProcessingMsg, ProcessingMsg, UnProcessingMsg) == UnProcessingMsg)
            {
                Task.Factory.StartNew(() =>
                {
                    SpinWait.SpinUntil(() => false, _triggerSpanTime);  //N秒后处理
                    _saveMsgBatchBlock.TriggerBatch();
                    Interlocked.Exchange(ref _isProcessingMsg, UnProcessingMsg);
                });
            }
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="msg"></param>
        public void Enqueue(T msg)
        {
            _saveMsgBatchBlock.Post(msg);
            TriggerBatch();
        }

    }
}
