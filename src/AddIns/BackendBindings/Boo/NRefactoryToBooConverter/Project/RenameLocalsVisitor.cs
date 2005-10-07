﻿#region license
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
using System.Collections.Generic;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.Ast.Visitors;

namespace NRefactoryToBooConverter
{
	public sealed class RenameLocalsVisitor : DepthFirstVisitor
	{
		public static void RenameLocals(Block block, StringComparer nameComparer)
		{
			FindVariableDeclarationsVisitor fvdv = new FindVariableDeclarationsVisitor();
			block.Accept(fvdv);
			List<DeclarationStatement> list = new List<DeclarationStatement>();
			foreach (DeclarationStatement decl in fvdv.Declarations) {
				DeclarationStatement conflict = null;
				int conflictIndex = -1;
				for (int i = 0; i < list.Count; i++) {
					if (nameComparer.Equals(list[i].Declaration.Name, decl.Declaration.Name)) {
						conflict = list[i];
						conflictIndex = i;
						break;
					}
				}
				if (conflict == null) {
					list.Add(decl);
				} else {
					// Handle conflict: try if "moveup" would be sufficient
					if (IsSameType(decl.Declaration.Type, conflict.Declaration.Type, nameComparer)) {
						// create declaration at beginning of class and
						// replace decl & conflict by assignment
						DeclarationStatement newDecl = new DeclarationStatement(conflict.LexicalInfo);
						newDecl.Declaration = new Declaration(conflict.Declaration.LexicalInfo, conflict.Declaration.Name, conflict.Declaration.Type);
						block.Insert(0, newDecl);
						ReplaceWithInitializer(decl);
						ReplaceWithInitializer(conflict);
						list[conflictIndex] = newDecl;
					} else {
						string newName = FindFreeName(decl.Declaration.Name, list, fvdv.Declarations, nameComparer);
						decl.ParentNode.Accept(new RenameLocalsVisitor(decl.Declaration.Name, newName, nameComparer));
						decl.Declaration.Name = newName;
					}
				}
			}
		}
		
		static string FindFreeName(string baseName, List<DeclarationStatement> list1, List<DeclarationStatement> list2, StringComparer nameComparer)
		{
			string tmp = baseName + "__";
			for (int i = 2;; i++) {
				string tryName = tmp + i.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
				bool found = false;
				foreach (DeclarationStatement d in list1) {
					if (nameComparer.Equals(d.Declaration.Name, tryName)) {
						found = true;
						break;
					}
				}
				if (found) continue;
				foreach (DeclarationStatement d in list2) {
					if (nameComparer.Equals(d.Declaration.Name, tryName)) {
						found = true;
						break;
					}
				}
				if (!found)
					return tryName;
			}
		}
		
		static void ReplaceWithInitializer(DeclarationStatement decl)
		{
			if (decl.Initializer == null) {
				decl.ReplaceBy(null);
			} else {
				ExpressionStatement statement = new ExpressionStatement(decl.LexicalInfo);
				statement.Expression = new BinaryExpression(decl.LexicalInfo, BinaryOperatorType.Assign,
				                                            new ReferenceExpression(decl.Declaration.LexicalInfo, decl.Declaration.Name),
				                                            decl.Initializer);
				decl.ReplaceBy(statement);
			}
		}
		
		static bool IsSameType(TypeReference a, TypeReference b, StringComparer nameComparer)
		{
			ArrayTypeReference arr1 = a as ArrayTypeReference;
			ArrayTypeReference arr2 = b as ArrayTypeReference;
			SimpleTypeReference s1 = a as SimpleTypeReference;
			SimpleTypeReference s2 = b as SimpleTypeReference;
			if (arr1 != null && arr2 != null) {
				if (arr1.Rank.Value != arr2.Rank.Value)
					return false;
				return IsSameType(arr1.ElementType, arr2.ElementType, nameComparer);
			} else if (s1 != null && s2 != null) {
				return nameComparer.Equals(s1.Name, s2.Name);
			} else {
				return false;
			}
		}
		
		string oldName, newName;
		StringComparer nameComparer;
		
		private RenameLocalsVisitor(string oldName, string newName, StringComparer nameComparer)
		{
			this.oldName = oldName;
			this.newName = newName;
			this.nameComparer = nameComparer;
		}
		
		public override void OnReferenceExpression(ReferenceExpression node)
		{
			if (nameComparer.Equals(node.Name, oldName)) {
				node.Name = newName;
			}
		}
	}
	
	public class FindVariableDeclarationsVisitor : DepthFirstVisitor
	{
		List<DeclarationStatement> declarations = new List<DeclarationStatement>();
		
		public List<DeclarationStatement> Declarations {
			get {
				return declarations;
			}
		}
		
		public override void OnCallableDefinition(CallableDefinition node) { }
		public override void OnCallableBlockExpression(CallableBlockExpression node) { }
		
		public override void OnDeclarationStatement(DeclarationStatement node)
		{
			declarations.Add(node);
			base.OnDeclarationStatement(node);
		}
	}
}
