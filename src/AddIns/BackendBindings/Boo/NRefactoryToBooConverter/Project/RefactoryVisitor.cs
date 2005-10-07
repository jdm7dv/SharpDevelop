#region license
// Copyright (c) 2005, Daniel Grunwald (daniel@danielgrunwald.de)
// All rights reserved.
//
// NRefactoryToBoo is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// NRefactoryToBoo is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with NRefactoryToBoo; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
#endregion

using System;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.NRefactory.Parser.AST;

namespace NRefactoryToBooConverter
{
	/// <summary>
	/// Visitor that prepares the conversion by converting source-language specific constructs
	/// into "better suited" constructs.
	/// </summary>
	/// <example>
	/// ForStatements of the form "for(int i = Start; i &lt; End; i += Step)" are
	/// converted to "For i As Integer = Start To End Step Step" (VB-Style) which has the better-matching
	/// Boo representation of "for i as int in range(Start, End, Step):"
	/// </example>
	public class RefactoryVisitor : CSharpToVBNetConvertVisitor
	{
		public RefactoryVisitor()
		{
			base.RenameConflictingFieldNames = false; // do not rename fields to VB-style
		}
	}
}
