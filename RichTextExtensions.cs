/*
======= Copyright (c) Immerxive Srl, All rights reserved. ===================

 Author: Cippo

 Purpose: Extensions to set or un-set rich text in strings. 
         It usually can be used for UI and Logs and other functionalities.

 Notes: 

=============================================================================
*/
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CippSharp.Reflection.Extensions
{
  public static class RichTextExtensions
    {
        /// <summary>
        /// Put the first Char of a string to UpperCase
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return string.Format("{0}{1}", value.First().ToString().ToUpper(), value.Substring(1));
        }

        /// <summary>
        /// Put the first valid Char of a string to UpperCase after specific chars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CapitalizeAfter(this string s, IEnumerable<char> chars)
        {
            var charsHash = new HashSet<char>(chars);
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < sb.Length - 2; i++)
            {
                if (charsHash.Contains(sb[i]) && sb[i + 1] == ' ')
                    sb[i + 2] = char.ToUpper(sb[i + 2]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Add italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Italic(this string value)
        {
            return string.Format("<i>{0}</i>", value);
        }

        /// <summary>
        /// Remove italic.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnItalic(this string value)
        {
            return value.Replace("<i>", string.Empty).Replace("</i>", string.Empty);
        }

        /// <summary>
        /// Add bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Bold(this string value)
        {
            return string.Format("<b>{0}</b>", value);
        }

        /// <summary>
        /// Remove bold
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnBold(this string value)
        {
            return value.Replace("<b>", string.Empty).Replace("</b>", string.Empty);
        }

        /// <summary>
        /// Set the string to that color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string OfColor(this string value, Color color)
        {
            return string.Format("<color=#{1}>{0}</color>", value, ColorUtility.ToHtmlStringRGBA(color));
        }

        /// <summary>
        /// Remove the specific color from a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string UnColor(this string value, Color color)
        {
            return value.Replace(string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(color)), string.Empty).Replace("</color>", string.Empty);
        }

        /// <summary>
        /// Remove any color from a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnColor(this string value)
        {
            return Regex.Replace(value.Replace("</color>", string.Empty), "<color=#.*?>", string.Empty);
        }

        /// <summary>
        /// Set the char size to a specific value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string OfSize(this string value, int size)
        {
            return string.Format("<size={1}>{0}</size>", value, size.ToString());
        }

        /// <summary>
        /// Remove the size from the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnSize(this string value)
        {
            return Regex.Replace(value.Replace("</size>", string.Empty), "<size=.*?>", string.Empty);
        }

        /// <summary>
        /// Remove any rich text utility from string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UnRichTextString(this string value)
        {
            return Regex.Replace(value, "<.*?>", string.Empty);
        }
	
    }

}
