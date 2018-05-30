using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CippSharp.Reflection
{
	public static class ReflectionUtility 
	{
		private const BindingFlags defaultFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
		
		public static List<PropertyInfo> GetProperties(Type type, BindingFlags bindingFlags = defaultFlags)
		{
			return type.GetProperties(bindingFlags).ToList();
		}

		public static string PropertyInfoToString(PropertyInfo propertyInfo)
		{
			return string.Format("{0} {1}", propertyInfo.PropertyType.Name, propertyInfo.Name);
		}

		public static List<FieldInfo> GetFields(Type type, BindingFlags bindingFlags = defaultFlags)
		{
			return type.GetFields(bindingFlags).ToList();
		}
		
		public static string FieldInfoToString (FieldInfo fieldInfo) 
		{
			return string.Format("{0} {1}", fieldInfo.FieldType.Name, fieldInfo.Name);
		}

		public static List<MethodInfo> GetMethods(Type type, BindingFlags bindingFlags = defaultFlags)
		{
			return type.GetMethods(bindingFlags).ToList();
		}

		public static string MethodInfoToString(MethodInfo methodInfo)
		{
			ParameterInfo[] parameterInfos = methodInfo.GetParameters();
			if (parameterInfos == null || parameterInfos.Length < 1)
			{
				return string.Format("{0} {1} ()", methodInfo.ReturnType.Name, methodInfo.Name);
			}
			else
			{
				string parameters = "";
				if (parameterInfos.Length == 1)
				{
					ParameterInfo parameterInfo = parameterInfos[0];
					parameters = string.Format("{0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name);
				}
				else if (parameterInfos.Length == 2)
				{
					ParameterInfo parameterInfo0 = parameterInfos[0];
					parameters += string.Format("{0} {1}", parameterInfo0.ParameterType.Name, parameterInfo0.Name);
					ParameterInfo parameterInfo1 = parameterInfos[1];
					parameters += string.Format(", {0} {1}", parameterInfo1.ParameterType.Name, parameterInfo1.Name);
				}
				else if (parameterInfos.Length >= 3)
				{
					for (int i = 0; i < parameterInfos.Length; i++)
					{
						ParameterInfo parameterInfo = parameterInfos[i];
						string tmp = "";
						if (i == 0)
						{
							tmp = string.Format("{0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name);
						}
						else if (i == parameterInfos.Length - 1)
						{
							tmp = string.Format(", {0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name);
						}
						else
						{
							tmp = string.Format(", {0} {1}, ", parameterInfo.ParameterType.Name, parameterInfo.Name);
						}

						parameters += tmp;
					}
				}

				return string.Format("{0} {1} ({2})", methodInfo.ReturnType.Name, methodInfo.Name, parameters);
			}
		}

		public static List<MemberInfo> GetMembers(Type type, BindingFlags bindingFlags)
		{
			return type.GetMembers(bindingFlags).ToList();
		}
	}
}
