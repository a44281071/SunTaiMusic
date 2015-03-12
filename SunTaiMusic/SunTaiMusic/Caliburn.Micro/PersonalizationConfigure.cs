using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Microsoft.Expression.Interactivity.Input;
using System.Windows.Input;

namespace Caliburn.Micro
{
    /// <summary>
    /// 为 Caliburn.Micro 框架订制个性化配置
    /// </summary>
    public class PersonalConfigure
    {

        /// <summary>
        /// 初始化所有设定，为 Caliburn.Micro 框架订制个性化配置
        /// </summary>
        public static void InitializeConfigure()
        {
            Regist_MessageBinder();
            Regist_KeyBinder();
            Regist_NamingConvention();
            RegistNameTransformer();
        }


        #region Register

        /// <summary>
        /// 附加方法参数的注册：[Action($model,$data)]
        /// </summary>
        private static void Regist_MessageBinder()
        {
            //绑定方法所在的DataContext
            MessageBinder.SpecialValues.Add("$model", dd => dd.Target);

            //控件的DataContext
            MessageBinder.SpecialValues.Add("$data", dd => dd.Source.DataContext);
        }

        /// <summary>
        /// 注册键盘按键： [Key Enter] = [MethodName] 
        /// <para>此功能实现有焦点的控件绑定键盘按键功能</para>
        /// </summary>
        private static void Regist_KeyBinder()
        {
            var trigger = Parser.CreateTrigger;  //获取委托

            //重写Trigger逻辑委托：[Key Enter]
            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    var defaults = ConventionManager.GetElementConvention(target.GetType());
                    return defaults.CreateTrigger();
                }

                var triggerDetail = triggerText.Replace("[", string.Empty)
                                               .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                // 解析“Key”字符逻辑
                if (splits[0] == "Key")
                {
                    var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                    return new KeyTrigger { Key = key, ActiveOnFocus = true };
                }

                return trigger(target, triggerText);
            };
        }

        /// <summary>
        /// 注册自定义View和ViewModel绑定规则
        /// </summary>
        private static void Regist_NamingConvention()
        {
            //var oldValue = ViewLocator.LocateTypeForModelType.Clone() as Func<Type, System.Windows.DependencyObject, object, Type>;
            var oldValue = ViewLocator.LocateTypeForModelType;

            ViewLocator.LocateTypeForModelType = (modelType, displayLocation, context) =>
            {
                // 获取 Attribute，获取可能的自定义转换
                var useViewAttribute = modelType.GetCustomAttributes(typeof(UseViewModelAttribute), true).Cast<UseViewModelAttribute>().FirstOrDefault();
                if (null != useViewAttribute)
                {
                    modelType = useViewAttribute.ModelType;
                }

                // 使用默认绑定继续执行
                var view = oldValue(modelType, displayLocation, context);
                return view;
            };
        }

        /// <summary>
        /// 注册自定义 View 和 ViewModel 名称替换规则
        /// </summary>
        private static void RegistNameTransformer()
        {
            ViewLocator.NameTransformer.AddRule("Screen$", "View");
        }

        #endregion

    }
}
