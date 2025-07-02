/*
 * Created by SharpDevelop.
 * User: romanov.m
 * Date: 02.07.2025
 * Time: 20:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.RoslynBinding
{
	/// <summary>
	/// Description of VBProjectBinding.
	/// </summary>
	public class VBProjectBinding : IProjectBinding
	{
		public const string LanguageName = "VB";
		
		#region IProjectBinding implementation

		public IProject LoadProject(ProjectLoadInformation info)
		{
			return new VBProject(info);
		}

		public IProject CreateProject(ProjectCreateInformation info)
		{
			return new VBProject(info);
		}

		public bool HandlingMissingProject {
			get {
				return false;
			}
		}

		#endregion
	}
}
