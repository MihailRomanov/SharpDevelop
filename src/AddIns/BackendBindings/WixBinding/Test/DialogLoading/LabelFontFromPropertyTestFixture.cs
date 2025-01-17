﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
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

using ICSharpCode.WixBinding;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using WixBinding;
using WixBinding.Tests.Utils;

namespace WixBinding.Tests.DialogLoading
{
	/// <summary>
	/// Tests the label font is retrieved using a Property element defined inside the
	/// document.
	/// </summary>
	[TestFixture]
	public class LabelFontFromPropertyTestFixture : DialogLoadingTestFixtureBase
	{
		string titleLabelFontName;
		double titleLabelFontSize;
		bool titleLabelFontBold;
		string descLabelFontName;
		double descLabelFontSize;
		bool descLabelFontBold;
		
		[OneTimeSetUp]
		public void SetUpFixture()
		{
			WixDocument doc = new WixDocument();
			doc.LoadXml(GetWixXml());
			WixDialog wixDialog = doc.CreateWixDialog("WelcomeDialog", new MockTextFileReader());
			using (Form dialog = wixDialog.CreateDialog(this)) {
				Label titleLabel = (Label)dialog.Controls[0];
				titleLabelFontName = titleLabel.Font.Name;
				titleLabelFontSize = titleLabel.Font.Size;
				titleLabelFontBold = titleLabel.Font.Bold;
				
				Label descLabel = (Label)dialog.Controls[1];
				descLabelFontName = descLabel.Font.Name;
				descLabelFontSize = descLabel.Font.Size;
				descLabelFontBold = descLabel.Font.Bold;
			}
		}
	
		[Test]
		public void TitleLabelFontName()
		{
			Assert.AreEqual("Verdana", titleLabelFontName);
		}
		
		[Test]
		public void TitleLabelFontSize()
		{
			Assert.AreEqual(13.0, titleLabelFontSize);
		}
		
		[Test]
		public void TitleLabelFontIsBold()
		{
			Assert.AreEqual(true, titleLabelFontBold);
		}
		
		[Test]
		public void DescriptionLabelFontName()
		{
			Assert.AreEqual("Arial", descLabelFontName);
		}
		
		[Test]
		public void DescriptionLabelFontSize()
		{
			Assert.AreEqual(10.0, descLabelFontSize);
		}
		
		[Test]
		public void DescriptionLabelFontIsBold()
		{
			Assert.AreEqual(false, descLabelFontBold);
		}
				
		string GetWixXml()
		{
			return "<Wix xmlns='http://schemas.microsoft.com/wix/2006/wi'>\r\n" +
				"\t<Fragment>\r\n" +
				"\t\t<UI>\r\n" +
				"\t\t\t<Property Id='BigFont'>{&amp;BigFontStyle}</Property>\r\n" +
				"\t\t\t<TextStyle Id='BigFontStyle' FaceName='Verdana' Size='13' Bold='yes' />\r\n" +
				"\t\t\t<Property Id='SmallFont'>{\\SmallFontStyle}</Property>\r\n" +
				"\t\t\t<TextStyle Id='SmallFontStyle' FaceName='Arial' Size='10'/>\r\n" +
				"\t\t\t<Dialog Id='WelcomeDialog' Height='270' Width='370'>\r\n" +
				"\t\t\t\t<Control Id='Title' Type='Text' X='135' Y='20' Width='220' Height='60' Transparent='yes' NoPrefix='yes'>\r\n" +
				"\t\t\t\t\t<Text>[BigFont]Welcome to the [ProductName] installation</Text>\r\n" +
				"\t\t\t\t</Control>\r\n" +
				"\t\t\t\t<Control Id='Description' Type='Text' X='135' Y='20' Width='220' Height='60' Transparent='yes' NoPrefix='yes'>\r\n" +
				"\t\t\t\t\t<Text>[SmallFont]Install text...</Text>\r\n" +
				"\t\t\t\t</Control>\r\n" +
				"\t\t\t</Dialog>\r\n" +
				"\t\t</UI>\r\n" +
				"\t</Fragment>\r\n" +
				"</Wix>";
		}
	}
}
