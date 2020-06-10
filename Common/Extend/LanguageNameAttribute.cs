///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月28日17:08:09
//CreateBy 		唐
//Content       多语言特性标记，用于设定每个语言所显示的值
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extend
{
    /// <summary>
    /// 多语言特性标记，用于设定每个语言所显示的值
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field, AllowMultiple = true)]
    public class LanguageNameAttribute : Attribute
    {
        public double Version;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="englishName"></param>
        public LanguageNameAttribute(string name, string englishName)

        {
            Name = name;
            EnglishName = englishName;
            Version = 1.0;
        }
        /// <summary>
        /// 中文名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; }
    }
}
