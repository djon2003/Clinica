<%@ Register TagPrefix="FTB" Namespace="FTBAssembly.CyberInternautes" Assembly="FTBAssembly" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeFile="WebTextPage.aspx.vb" Inherits="WebTextPage" ValidateRequest=false %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title id=PageTitle runat="server">WebTextPage</title>
<meta content="Microsoft Visual Studio.NET 7.0" name=GENERATOR>
<meta content="Visual Basic 7.0" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>

<style>
#FreeTextBox1_designEditor A {cursor:hand;}

BODY {
width:100%;height:100%;
}

#FreeTextBox1_designEditor {
width:100%;
height:100%;
}
</style>

<script type="text/javascript">
function launch() {
if (typeof(init) != "function") {
alert("Veuillez vous assurer que le compte internet du service internet (IIS) a accès au fichier 'WebTextPage.js'.\n Il sera impossible d'utiliser le mode d'édition de la boîte de texte web.");
} else {
init();
}
}
</script>

  </HEAD>
<body onload="launch();" onresize="IFrames_Resize(true)" bottomMargin=0 leftMargin=0 topMargin=0 rightMargin=0>

<table height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
  <tbody>
  <form id=Form1 method=post runat="server">
  <tr>
    <td id=TD1 vAlign=top width="100%" height="100%">
<FTB:FTBAssembly id="FreeTextBox1"
			runat="Server"
			Focus="true"
			 AutoGenerateToolbarsFromString="False" DesignModeCss="webtextcss.css" ReadOnly="False" AllowHtmlMode="True" FormatHtmlTagsToXhtml="False" />
<asp:Button id="AdjustSize" runat="server" Text="Button" style="DISPLAY:none"></asp:Button>
<asp:Button id=SaveButton runat="server" Text="Button" style="DISPLAY:none"></asp:Button>
<input type=button id="FocusButton" runat="server" Text="Button" style="DISPLAY:none" onclick="FTB_API['FreeTextBox1'].Focus();" >
</td></tr></form></tbody></table>

<!-- 
toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;SymbolsMenu,InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn"
toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
			DesignModeCss="designmode.css" -->
<!-- <img onload="FTBLoaded();" src=point.gif style="display:hidden"> -->
	</body>
</HTML>
