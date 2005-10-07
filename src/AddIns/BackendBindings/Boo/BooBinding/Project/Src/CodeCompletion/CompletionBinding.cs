﻿// <file>
//     <copyright see="prj:///doc/copyright.txt">2002-2005 AlphaSierraPapa</copyright>
//     <license see="prj:///doc/license.txt">GNU General Public License</license>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Globalization;

using ICSharpCode.Core;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;

namespace Grunwald.BooBinding.CodeCompletion
{
	public class CompletionBinding : DefaultCodeCompletionBinding
	{
		public CompletionBinding()
		{
			this.EnableXmlCommentCompletion = false;
		}
		
		bool IsInComment(SharpDevelopTextAreaControl editor)
		{
			ExpressionFinder ef = new ExpressionFinder(editor.FileName);
			int cursor = editor.ActiveTextAreaControl.Caret.Offset - 1;
			return ef.SimplifyCode(editor.Document.GetText(0, cursor + 1), cursor) == null;
		}
		
		public override bool HandleKeyword(SharpDevelopTextAreaControl editor, string word)
		{
			switch (word.ToLower(CultureInfo.InvariantCulture)) {
				case "import":
					editor.ShowCompletionWindow(new CodeCompletionDataProvider(new ExpressionResult("Global", ExpressionContext.Type)), ' ');
					return true;
				case "as":
				case "isa":
					if (IsInComment(editor)) return false;
					editor.ShowCompletionWindow(new CtrlSpaceCompletionDataProvider(ExpressionContext.Type), ' ');
					return true;
				default:
					return base.HandleKeyword(editor, word);
			}
		}
	}
}
