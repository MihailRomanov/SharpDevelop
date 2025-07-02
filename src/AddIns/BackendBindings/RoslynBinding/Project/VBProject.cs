/*
 * Created by SharpDevelop.
 * User: romanov.m
 * Date: 02.07.2025
 * Time: 20:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.RoslynBinding
{
	/// <summary>
	/// Description of VBProject.
	/// </summary>
	public class VBProject : CompilableProject
	{
		public VBProject(ProjectLoadInformation prjLoadInfo) : base(prjLoadInfo)
		{}

		public VBProject(ProjectCreateInformation prjCreateInfo) : base(prjCreateInfo)
		{}
		
		#region implemented abstract members of CompilableProject

		public override string Language {
			get {
				return VBProjectBinding.LanguageName;
			}
		}

		#endregion
	}
}
