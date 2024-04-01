using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Observers
{
    public class StackOperationsLogger
    {
        private readonly StringBuilder log = new StringBuilder();

        public string GetLog()
        {
            return log.ToString();
        }

        public void SubscribeOn(ObservableStack<int> stack)
        {
            stack.StackChanged += HandleStackChanged;
        }

        private void HandleStackChanged(object sender, StackEventData<int> eventData)
        {
            log.Append(eventData.ToString());
        }
    }

    public class ObservableStack<T>
    {
        public event EventHandler<StackEventData<T>> StackChanged;

        private readonly List<T> data = new List<T>();

        public void Push(T obj)
        {
            data.Add(obj);
            OnStackChanged(new StackEventData<T> { IsPushed = true, Value = obj });
        }

        public T Pop()
        {
            if (data.Count == 0)
                throw new InvalidOperationException();

            var result = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            OnStackChanged(new StackEventData<T> { IsPushed = false, Value = result });
            return result;
        }

        protected virtual void OnStackChanged(StackEventData<T> eventData)
        {
            StackChanged?.Invoke(this, eventData);
        }
    }
}