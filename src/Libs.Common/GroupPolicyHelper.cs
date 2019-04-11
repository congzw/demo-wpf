using System;
using Libs.Common._Internals;
using Microsoft.Win32;

namespace Libs.Common
{
    public class GroupPolicyHelper : IGroupPolicyHelper
    {
        public object GetValue(string name, object defaultValue)
        {
            return ComputerGroupPolicyObject.GetPolicySetting(name, defaultValue);
        }

        public void SetValue(string name, object value, RegistryValueKind valueKind)
        {
            ComputerGroupPolicyObject.SetPolicySetting(name, value, valueKind);
        }
        
        #region for di extensions

        private static IGroupPolicyHelper _resolve()
        {
            var debugHelper = new GroupPolicyHelper();
            return debugHelper;
        }

        public static Func<IGroupPolicyHelper> Resolve = _resolve;

        #endregion
    }

    public interface IGroupPolicyHelper
    {
        object GetValue(string name, object defaultValue);
        void SetValue(string name, object value, RegistryValueKind valueKind);
    }
}
