using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caliburn.Micro
{
    /// <summary>
    /// 标识本 Model 匹配 View 时，将视为指定的Model类型
    /// </summary>
    public class UseViewModelAttribute : Attribute
    {
        public UseViewModelAttribute(Type pModelType)
        {
            this._ModelType = pModelType;
        }

        private Type _ModelType;
        /// <summary>
        /// 只读，获取vm的类型
        /// </summary>
        public Type ModelType
        {
            get { return _ModelType; }
        }

    }
}
