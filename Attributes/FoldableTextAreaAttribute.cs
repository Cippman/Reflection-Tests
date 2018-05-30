/*======= Copyright (c) Immerxive Srl, All rights reserved. =========================
 * 
 * Author: Alessandro Salani (Cippo)
 * 
 * Purpose: same attribute as unity's TextArea, but foldable.
 *
 * Notes: 
 * 
 * ==================================================================================
 */
using System;
using UnityEngine;

namespace CippSharp
{
	/// <summary>
	///   <para>Attribute to make a string be edited with a height-flexible and scrollable text area.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class FoldableTextAreaAttribute : PropertyAttribute
	{
		/// <summary>
		///   <para>The minimum amount of lines the text area will use.</para>
		/// </summary>
		public readonly int minLines;

		/// <summary>
		///   <para>The maximum amount of lines the text area can show before it starts using a scrollbar.</para>
		/// </summary>
		public readonly int maxLines;

		/// <summary>
		///   <para>Attribute to make a string be edited with a height-flexible and scrollable text area.</para>
		/// </summary>
		/// <param name="minLines">The minimum amount of lines the text area will use.</param>
		/// <param name="maxLines">The maximum amount of lines the text area can show before it starts using a scrollbar.</param>
		public FoldableTextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		/// <summary>
		///   <para>Attribute to make a string be edited with a height-flexible and scrollable text area.</para>
		/// </summary>
		/// <param name="minLines">The minimum amount of lines the text area will use.</param>
		/// <param name="maxLines">The maximum amount of lines the text area can show before it starts using a scrollbar.</param>
		public FoldableTextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}
	}
}