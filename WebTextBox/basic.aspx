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
<script language=javascript>

var CurExternal;
var didOnce = false;

function editorClicked(event) {
var IFrame = document.getElementById('FreeTextBox1_designEditor');
var pe = event.srcElement;
if (pe.parentElement.tagName == "A" || pe.tagName == "A") {
	pe = (pe.parentElement.tagName == "A"?pe.parentElement:pe);
//	if (didOnce == false) {
		external.EditorClick(pe.href);
		//didOnce = true;
//	} else {
//		didOnce = false;
//	}
}
}


//To detect full loading
function init(){
var IFrame = document.getElementById('FreeTextBox1_designEditor');
//page.onreadystatechange = function() {if (document.readyState == "complete"){var page = document.getElementById('page');if (page.contentWindow.document.readyState == "complete"){page.contentWindow.document.body.onload= hideBackButton;}}};

IFrame.onreadystatechange = function() {
var IFrame = document.getElementById('FreeTextBox1_designEditor');

if (document.readyState == "complete"){
if (IFrame.contentWindow.document.readyState == "complete"){

IFrame.contentWindow.document.body.onload=function() {
var IFrame = document.getElementById('FreeTextBox1_designEditor');
IFrame.contentWindow.document.attachEvent('onkeyup', TextChanging);
IFrame.contentWindow.document.attachEvent('onclick',editorClicked);
IFrames_Resize(true);
FTBLoaded();
};

}
}

};
}


function IFrames_Resize(inResize){

//var CurPos = internalGetPos();

var HTML;
HTML="";
try {
	HTML = FTB_API['FreeTextBox1'].GetHtml();

var fo = document.getElementById('FreeTextBox1_toolbarArea');
var de = document.getElementById('FreeTextBox1_designEditor');
var he = document.getElementById('FreeTextBox1_htmlEditorArea');
var pp = document.getElementById('FreeTextBox1_htmlEditorArea');

var offset = (document.body.offsetHeight - 13 - fo.offsetHeight)  + "px";
var curWidth = (document.body.offsetWidth - 10) + "px";

de.style.height = offset;
de.style.width = curWidth;

he.style.height = offset;
he.style.width = curWidth;

pp.style.height = offset;
pp.style.width = curWidth;

FTB_API['FreeTextBox1'].SetHtml(HTML);
//SetPos(CurPos);

if(inResize){

//window.document.forms[0].submit();

}

} catch(err) {}

return true;

}
//onresize="IFrames_Resize(true)"
</script>
  </HEAD>
<body onload="init();" onresize="IFrames_Resize(true)" bottomMargin=0 leftMargin=0 topMargin=0 rightMargin=0>

<table height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
  <tbody>
  <form id=Form1 method=post runat="server">
  <tr>
    <td id=TD1 vAlign=top width="100%">
<FTB:FTBAssembly id="FreeTextBox1"
			runat="Server"
			Focus="true"
			 AutoGenerateToolbarsFromString="False" DesignModeCss="webtextcss.css" ReadOnly="False" AllowHtmlMode="True" FormatHtmlTagsToXhtml="False" />
<asp:Button id="AdjustSize" runat="server" Text="Button" style="DISPLAY:none"></asp:Button>
<asp:Button id=SaveButton runat="server" Text="Button" style="DISPLAY:none"></asp:Button>
<input type=button id="FocusButton" runat="server" Text="Button" style="DISPLAY:none" onclick="FTB_API['FreeTextBox1'].Focus();" >
</td></tr></form></tbody></table>

<script language="JavaScript">
var n=0;
var CurHTMLSetted;
var isTextChanged;
 function TextChanging(event) {
if (isTextChanged == 'undefined' || isTextChanged == false) {
var IFrame = document.getElementById('FreeTextBox1_designEditor');
//IFrame.contentWindow.document.body.innerHTML += "<br>";
//var h = IFrame.contentWindow.document.body.outerHTML;
	var HTML = FTB_API['FreeTextBox1'].GetHtml();
//alert(h);
//alert(HTML);
	if (CurHTMLSetted == HTML) return;
}


isTextChanged = true;

if (!(external)) {return;}

external.TextChanged(CurHTMLSetted);
}

function FTBLoaded() {
if (!(external)) return;

CurExternal = external;

for(var i = 0; i < 100; i++) {
	for(var j = 0; j < 100; j++) {
		var toolBarObject = document.getElementById('FreeTextBox1_' + i + '_' + j);
		if (toolBarObject != "undefined" && toolBarObject != null) {
			var tagName = (toolBarObject.tagName?toolBarObject.tagName:"");

			if (tagName != "SELECT") {toolBarObject.attachEvent('onclick',function() {isTextChanged=true;TextChanging("");});}
			else {toolBarObject.attachEvent('onchange',function() {isTextChanged=true;TextChanging("");});}
		}
	}
}

external.PageLoaded();
SetPos(initPos);
}


function InsertHTML(HTML) {
	if (FTB_API['FreeTextBox1']) {FTB_API['FreeTextBox1'].InsertHtml(HTML);}
}

function SurroundHTML(BeforeHTML,AfterHTML) {
	if (FTB_API['FreeTextBox1']) {FTB_API['FreeTextBox1'].SurroundHtml(BeforeHTML,AfterHTML);}
}

function SetHTML(HTML) {
if (FTB_API['FreeTextBox1']) {
FTB_API['FreeTextBox1'].SetHtml(HTML);
CurHTMLSetted = FTB_API['FreeTextBox1'].GetHtml();
isTextChanged = false;
}
}

function GetHTML(WaitingNo) {
if (!(external)) {return;}

if (FTB_API['FreeTextBox1']) {external.SendTextTo(FTB_API['FreeTextBox1'].GetHtml(), WaitingNo);}
}

function AddLink() {
if (!(external)) {return;}
	external.AddLink();
}

function AddImage() {
if (!(external)) {return;}
	external.AddImage();
}

function SetFocus() {
	if (FTB_API['FreeTextBox1']) {FTB_API['FreeTextBox1'].Focus();}
}

function GetPos(WaitingNo) {
if (!(external)) {return;}
	if (FTB_API['FreeTextBox1']) {external.SendPosTo(internalGetPos(), WaitingNo);}
	else {external.SendPosTo(0, WaitingNo);}
}

function internalGetPos() {
	return Math.abs(parent.FreeTextBox1_designEditor.document.selection.createRange().move("character",-100000000));
}

function SetPos(pos) {
	if (pos < 0) {pos=0;}

	var range = parent.FreeTextBox1_designEditor.document.selection.createRange();
	if (range.text.length>0) {
		range = document.selection.createRange();
		range.collapse();
		range.select();
	}
	else {
		range.moveStart("character",internalGetPos() * -1);
		range.moveEnd("character",internalGetPos() * -1);
		range.select();
	}

	range = parent.FreeTextBox1_designEditor.document.selection.createRange();
	range.moveStart("character",pos);
	range.select();
}

function SelectText(pos, length) {
	if (pos < 0) {pos=0;}

	SetPos(pos);
	range = parent.FreeTextBox1_designEditor.document.selection.createRange();
	range.moveEnd("character",length);
	range.select();
}

function SearchAndSelect(text, searchPos) {
	if (text == "") {return;}

	SetPos(searchPos);
	var range = parent.FreeTextBox1_designEditor.document.selection.createRange();
	var found = range.findText(text,searchPos);
	if (found) {
		range.select();
	}
	else {
		SetPos(0);
		range = parent.FreeTextBox1_designEditor.document.selection.createRange();
		range.findText(text);
		range.select();
	}
}

</script>
<!-- 
toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;SymbolsMenu,InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn"
toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
			DesignModeCss="designmode.css" -->
<!-- <img onload="FTBLoaded();" src=point.gif style="display:hidden"> -->
	</body>
</HTML>
