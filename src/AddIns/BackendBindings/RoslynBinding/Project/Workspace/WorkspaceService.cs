
using System;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Project;

namespace ICSharpCode.RoslynBinding.Workspace
{
	/// <summary>
	/// Description of WorkspaceService.
	/// </summary>
	public class WorkspaceService : IWorkspaceService, IDisposable
	{
		IProjectService projectService;

		public WorkspaceService()
		{
			projectService = SD.GetRequiredService<IProjectService>();
		}

		#region IDisposable implementation

		public void Dispose()
		{
			
		}

		#endregion
	}
}
