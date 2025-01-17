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

namespace WixBinding.Tests.DialogXmlGeneration
{
	/// <summary>
	/// Adds a new radio button to the radio button group.
	/// </summary>
	[TestFixture]
	public class RadioButtonAddedTestFixture : DialogLoadingTestFixtureBase
	{
		XmlElement declineRadioButtonElement;
		
		[OneTimeSetUp]
		public void SetUpFixture()
		{
			WixDocument doc = new WixDocument();
			doc.LoadXml(GetWixXml());
			CreatedComponents.Clear();
			WixDialog wixDialog = doc.CreateWixDialog("AcceptLicenseDialog", new MockTextFileReader());
			using (Form dialog = wixDialog.CreateDialog(this)) {

				Panel radioButtonGroup = (Panel)dialog.Controls[0];
				RadioButton declineRadioButton = new RadioButton();
				declineRadioButton.Left = 10;
				declineRadioButton.Top = 20;
				declineRadioButton.Width = 200;
				declineRadioButton.Height = 30;
				declineRadioButton.Text = "I do not accept";
				radioButtonGroup.Controls.Add(declineRadioButton);
				
				XmlElement dialogElement = wixDialog.UpdateDialogElement(dialog);
				XmlElement radioButtonGroupElement = (XmlElement)dialogElement.SelectSingleNode("w:Control[@Id='Buttons']", new WixNamespaceManager(dialogElement.OwnerDocument.NameTable));
				
				XmlNodeList radioButtonElements = radioButtonGroupElement.SelectNodes("//w:RadioButtonGroup/w:RadioButton", new WixNamespaceManager(dialogElement.OwnerDocument.NameTable));
				declineRadioButtonElement = (XmlElement)radioButtonElements[1];
			}
		}
		
		[Test]
		public void DeclineRadioButtonX()
		{
			int expectedX = Convert.ToInt32(10 / WixDialog.InstallerUnit);
			Assert.AreEqual(expectedX.ToString(), declineRadioButtonElement.GetAttribute("X"));
		}
		
		[Test]
		public void DeclineRadioButtonY()
		{
			int expectedY = Convert.ToInt32(20 / WixDialog.InstallerUnit);
			Assert.AreEqual(expectedY.ToString(), declineRadioButtonElement.GetAttribute("Y"));
		}
		
		[Test]
		public void DeclineRadioButtonHeight()
		{
			int expectedHeight = Convert.ToInt32(30 / WixDialog.InstallerUnit);
			Assert.AreEqual(expectedHeight.ToString(), declineRadioButtonElement.GetAttribute("Height"));
		}
		
		[Test]
		public void DeclineRadioButtonWidth()
		{
			int expectedWidth = Convert.ToInt32(200 / WixDialog.InstallerUnit);
			Assert.AreEqual(expectedWidth.ToString(), declineRadioButtonElement.GetAttribute("Width"));
		}
		
		[Test]
		public void DeclineRadioButtonText()
		{
			Assert.AreEqual("I do not accept", declineRadioButtonElement.GetAttribute("Text"));
		}

		string GetWixXml()
		{
			return "<Wix xmlns='http://schemas.microsoft.com/wix/2006/wi'>\r\n" +
				"\t<Fragment>\r\n" +
				"\t\t<UI>\r\n" +
				"\t\t\t<Dialog Id='AcceptLicenseDialog' Height='270' Width='370'>\r\n" +
				"\t\t\t\t<Control Id='Buttons' Type='RadioButtonGroup' X='20' Y='187' Width='330' Height='40' Property='AcceptLicense'/>\r\n" +
				"\t\t\t</Dialog>\r\n" +
				"\t\t\t<RadioButtonGroup Property='AcceptLicense'>\r\n" +
				"\t\t\t\t<RadioButton Text='I accept' X='5' Y='0' Width='300' Height='15' Value='Yes'/>\r\n" +
				"\t\t\t</RadioButtonGroup>\r\n" +
				"\t\t</UI>\r\n" +
				"\t</Fragment>\r\n" +
				"</Wix>";
		}
	}
}
