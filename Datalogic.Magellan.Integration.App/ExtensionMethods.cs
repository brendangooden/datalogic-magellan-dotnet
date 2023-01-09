using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Magellan.Integration.App
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Used for Cross Threaded access to UI Control and to return a value.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="itm"></param>
        /// <param name="action"></param>
        public static TOut Invoke<TIn, TOut>(this TIn itm, Func<TIn, TOut> action)
        {
            try
            {
                if (itm is null) return default;

                switch (itm)
                {
                    case Control { IsDisposed: true }:
                        return default;

                    case ISynchronizeInvoke { InvokeRequired: true } s:
                        return (TOut)s.Invoke(action, new object[] { s });

                    case ISynchronizeInvoke s:
                        return action((TIn)s);

                    default:
                        throw new NotSupportedException();
                }
            }
            catch (ObjectDisposedException)
            {
                return default; // ignored
            }
        }

        /// <summary>
        /// Used for Cross Threaded access to UI Controls.<br></br>
        /// For returning a value, use the other overload that takes a Func&lt;TIn, TOut&gt;
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="itm"></param>
        /// <param name="action"></param>
        public static void Invoke<TIn>(this TIn itm, Action<TIn> action)
        {
            try
            {
                switch (itm)
                {
                    case Control { IsDisposed: true }:
                        return;

                    case ISynchronizeInvoke { InvokeRequired: true } s:
                        s.Invoke(action, new object[] { s });
                        break;

                    case ISynchronizeInvoke s:
                        action((TIn)s);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
            catch (ObjectDisposedException)
            {
                // ignored
            }
        }
    }
}
