// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.Threading.Tasks;
using CSharpBinding.Parser;
using CSharpBinding.Refactoring;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.SharpDevelop;
using System;
using System.Linq;
using ICSharpCode.SharpDevelop.Editor;
using ICSharpCode.SharpDevelop.Parser;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.SharpDevelop.Refactoring;
using ICSharpCode.SharpDevelop.Workbench;
using NUnit.Framework;
using Rhino.Mocks;

namespace CSharpBinding.Tests
{
	[TestFixture]
	public class CSharpCodeGeneratorAddEventHandlerTests
	{
		string program = @"using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTest
{
	/// <summary>
	/// Description of M1ainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();		
		}
	}
}
";
		
		MockTextEditor textEditor;
		IProject project;
		CSharpCodeGenerator gen;
		
		static readonly IUnresolvedAssembly Corlib = new CecilLoader().LoadAssemblyFile(typeof(object).Assembly.Location);
		static readonly IUnresolvedAssembly WinFormsLib = new CecilLoader()
			.LoadAssemblyFile(typeof(System.Windows.Forms.Control).Assembly.Location);
		
		[SetUp]
		public void SetUp()
		{
			SD.InitializeForUnitTests();
			textEditor = new MockTextEditor();
			textEditor.Document.Text = program;
			var parseInfo = textEditor.CreateParseInformation();
			this.project = MockRepository.GenerateStrictMock<IProject>();
			var pc = new CSharpProjectContent().AddOrUpdateFiles(parseInfo.UnresolvedFile);
			pc = pc.AddAssemblyReferences(new[] { Corlib, WinFormsLib });
			var compilation = pc.CreateCompilation();
			SD.Services.AddService(typeof(IParserService), MockRepository.GenerateStrictMock<IParserService>());
			
			SD.ParserService.Stub(p => p.GetCachedParseInformation(textEditor.FileName)).Return(parseInfo);
			SD.ParserService.Stub(p => p.GetCompilation(project)).Return(compilation);
			SD.ParserService.Stub(p => p.GetCompilationForFile(textEditor.FileName)).Return(compilation);
			SD.ParserService.Stub(p => p.Parse(textEditor.FileName, textEditor.Document)).WhenCalled(
				i => {
					var syntaxTree = new CSharpParser().Parse(textEditor.Document.Text, textEditor.FileName);
					i.ReturnValue = new CSharpFullParseInformation(syntaxTree.ToTypeSystem(), null, syntaxTree);
				}).Return(parseInfo); // fake Return to make it work
			SD.ParserService.Stub(p => p.ParseAsync(textEditor.FileName, textEditor.Document))
				.Return(Task.FromResult(parseInfo));
			SD.Services.AddService(typeof(IFileService), MockRepository.GenerateStrictMock<IFileService>());
			IViewContent view = MockRepository.GenerateStrictMock<IViewContent>();
			view.Stub(v => v.GetService(typeof(ITextEditor))).Return(textEditor);
			SD.FileService.Stub(f => f.OpenFile(textEditor.FileName, false)).Return(view);
			SD.FileService.Stub(f => f.OpenFile(textEditor.FileName, true)).Return(view);
			gen = new CSharpCodeGenerator();
		}
		
		[TearDown]
		public void TearDown()
		{
			SD.TearDownForUnitTests();
		}
		
		static object[] AddEventHandlerCases = {
			new object[] { InsertEventHandlerBodyKind.Nothing, 
				@"using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTest
{
	/// <summary>
	/// Description of M1ainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();		
		}
		void Button1Click(object sender, EventArgs e)
		{
	
		}
	}
}
" },
			new object[] { InsertEventHandlerBodyKind.ThrowNotImplementedException, 
				@"using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTest
{
	/// <summary>
	/// Description of M1ainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();		
		}
		void Button1Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
" },
			new object[] { InsertEventHandlerBodyKind.TodoComment, @"using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsTest
{
	/// <summary>
	/// Description of M1ainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();		
		}
		void Button1Click(object sender, EventArgs e)
		{
			// TODO: Implement Button1Click
		}
	}
}
" },
		};
		
	
		[Test, TestCaseSource("AddEventHandlerCases")]
		public void AddEventHandler(InsertEventHandlerBodyKind bodyKind, string expectedResult)
		{
			var compilation = SD.ParserService.GetCompilationForFile(textEditor.FileName);
			var entity = FindEntity<ITypeDefinition>("MainForm");
			
			var type = compilation.FindType(new FullTypeName("System.Windows.Forms.Control"));
			var e = type.GetEvents(evt => evt.Name == "Click").FirstOrDefault();
			gen.InsertEventHandler(entity, "Button1Click", e, true, bodyKind);
			
			var result = textEditor.Document.Text;
			Assert.AreEqual(result, expectedResult);
		}
				
		T FindEntity<T>(string targetClass) where T : IEntity
		{
			var compilation = SD.ParserService.GetCompilationForFile(textEditor.FileName);
			var parseInfo = SD.ParserService.Parse(textEditor.FileName, textEditor.Document);
			
			int i = textEditor.Document.IndexOf(targetClass, 0, textEditor.Document.TextLength, StringComparison.Ordinal);
			Assert.Greater(i, -1);
			TextLocation location = textEditor.Document.GetLocation(i);
			var member = parseInfo.UnresolvedFile.GetMember(location.ToNRefactory());
			var type = parseInfo.UnresolvedFile.GetInnermostTypeDefinition(location.ToNRefactory());
			
			var context = new SimpleTypeResolveContext(compilation.MainAssembly);
			var rt = type.Resolve(context).GetDefinition();
			
			if (member != null) {
				return (T)member.CreateResolved(context.WithCurrentTypeDefinition(rt));
			}
			
			return (T)rt;
		}
	}
}
