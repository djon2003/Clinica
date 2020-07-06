var n=0;
var CurHTMLSetted;
var isTextChanged;
var CurExternal;
var didOnce = false;

function editorClicked(event) {
   var IFrame = document.getElementById('FreeTextBox1_designEditor');
   var pe = event.srcElement;
   if (pe.parentElement.tagName == "A" || pe.tagName == "A") {
      pe = (pe.parentElement.tagName == "A"?pe.parentElement:pe);
   //	if (didOnce == false) {
         external.EditorClick(pe.href);

         if (typeof(pe.onclick) == "function") {
            pe.onclick();
         } else if (typeof(pe.onclick) == "string" && pe.onclick != "") {
            var eventJS = pe.onclick;
            eval(eventJS);
         }
         //didOnce = true;
   //	} else {
   //		didOnce = false;
   //	}
   }
}


function errorHappened(msg, url, line, obj) {
   if (external && typeof(external.catchError) == "unknown" ) {
      external.catchError(msg, url, line, obj.document.body.outerHTML);
   }

   return true;
}

var curKeyUpEvent;

//To detect full loading
function init() {
    //alert("INIT");
/*
    var ftbEditor = document.getElementById('FreeTextBox1_designEditor');
    var reachedError = false;
    while (!reachedError && typeof (ftbEditor) !== "undefined") {
        //alert(ftbEditor.tagName + ':' + ftbEditor.style.height);
	try {
        	ftbEditor.style.height = "100%";
	        ftbEditor = ftbEditor.parentNode;
	} catch(ex) {reachedError = true;}
    }
*/    
   window.onerror = function(msg, url, line) {return errorHappened(msg, url, line, window);};
   var IFrame = document.getElementById('FreeTextBox1_designEditor');
   
   var KeyUp = function(e) {
      e = (e ? e : window.event);
      e = {keyCode : e.keyCode, shiftKey: e.shiftKey, srcElement: e.srcElement};
      curKeyUpEvent = e;

      var doubleTab = function() {
         //Test to ensure not selectable zone can't be tabbed. (Like in Rapport CSST initial, being in the first textarea and pressing tab makes the selection on the next group instead of the next textarea)
         //alert("dt");
         var e = curKeyUpEvent;
         //alert(e);
         //alert("in:" + e.keyCode);

var keys  = "";
for (var key in document.selection )
{
keys += key + "\n";
}

         var isMoveExists = document.selection.createRange && document.selection.createRange().move;
 //        alert("in2:" + document.selection.type);
   //      alert(e.keyCode + ":" + (!isMoveExists && e.keyCode == 9));
         if (!isMoveExists && e.keyCode == 9)
         {
            var my1 = document.createEventObject(event);
            my1.keyCode = 9;
            my1.shiftKey = e.shiftKey;
            e.srcElement.fireEvent("on"+e.type,my1);
            //e.keyCode = 0;
            //e.returnValue=false;
         }
      };

      setTimeout(doubleTab, 100);
   };

   var CheckElementFocus = function() {
      //alert("cef");
   };

   IFrame.onreadystatechange = function() {
      var IFrame = document.getElementById('FreeTextBox1_designEditor');

      if (document.readyState == "complete"){
         if (IFrame.contentWindow.document.readyState == "complete"){

            IFrame.contentWindow.document.body.onload=function() {
               var IFrame = document.getElementById('FreeTextBox1_designEditor');
               IFrame.contentWindow.document.attachEvent('onkeyup', TextChanging);
               //IFrame.contentWindow.document.attachEvent('onfocus', CheckElementFocus);
               //IFrame.contentWindow.document.attachEvent('onkeyup', KeyUp);
               IFrame.contentWindow.document.attachEvent('onclick',editorClicked);
               IFrame.contentWindow.onerror = function(msg, url, line) {return errorHappened(msg, url, line, IFrame.contentWindow);};
               IFrame.contentWindow.document.body.attachEvent('onpaste',ensureProperPaste);
               IFrames_Resize(true);
               FTBLoaded();
            };

         }
      }
   };
}

function pasteHtmlAtCaret(html) {
    var sel, range;
    if (window.getSelection) {
        // IE9 and non-IE
        sel = window.getSelection();
        if (sel.getRangeAt && sel.rangeCount) {
            range = sel.getRangeAt(0);
            range.deleteContents();

            // Range.createContextualFragment() would be useful here but is
            // non-standard and not supported in all browsers (IE9, for one)
            var el = document.createElement("div");
            el.innerHTML = html;
            var frag = document.createDocumentFragment(), node, lastNode;
            while ( (node = el.firstChild) ) {
                lastNode = frag.appendChild(node);
            }
            range.insertNode(frag);

            // Preserve the selection
            if (lastNode) {
                range = range.cloneRange();
                range.setStartAfter(lastNode);
                range.collapse(true);
                sel.removeAllRanges();
                sel.addRange(range);
            }
        }
    } else if (document.selection && document.selection.type != "Control") {
        // IE < 9
        document.selection.createRange().pasteHTML(html);
    }
}

var tagsToPasteNormally = ["textarea", "input"];

ensureProperPaste = function(e) {
   //Try to call external method that advise paste event
   try {
      external.pasted();
   } catch (ex) {
   }

   //Ensure pasting into some tags doesn't call external method
   for ( var i = 0; i < tagsToPasteNormally.length; i++) {
      if (tagsToPasteNormally[i] == e.srcElement.tagName.toLowerCase()) {
         return true;
      }
   }

   //Try to call external method of pasting
   try {
   external.pasteHTMLFromClipboard();
   return false;
   } catch (ex) {
   }

   //Call normal pasting method
   return true;
};


function IFrames_Resize(inResize){

   var CurPos = internalGetPos();

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
      SetPos(CurPos);

      if(inResize){

      //window.document.forms[0].submit();

      }

   } catch(err) {}

   return true;

}


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

   var initPos = 0;
   SetPos(initPos);
   external.PageLoaded();
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
   if (parent.FreeTextBox1_designEditor.document.selection.createRange && parent.FreeTextBox1_designEditor.document.selection.createRange().move)
   {
   	return Math.abs(parent.FreeTextBox1_designEditor.document.selection.createRange().move("character",-100000000));
   } else {
      return 0;
   }
}

function setFieldValue(id, value) {
   var field = (parent.FreeTextBox1_designEditor.document ? parent.FreeTextBox1_designEditor.document : document);
   field = field.getElementById(id);
   if (field) {
      field.value = value;
      field.focus();
   }   
}

function insertDate(fieldId) {
   var obj = parent.FreeTextBox1_designEditor.document.getElementById(fieldId);
   window.external.insertDate(obj.id, obj.value);
   obj.focus();
}

function SetPos(pos, domObject) {
	if (pos < 0) {pos=0;}

   if (domObject == undefined)
   {
      domObject = parent.FreeTextBox1_designEditor.document.selection;
   }

   var range;
   if (domObject.createRange) {
      range = domObject.createRange();
   } else if (domObject.createTextRange) {
      range = domObject.createTextRange();
   }

   if ((range.length && range.length>0) || (range.text && range.text.length>0)) {
      range.collapse();
   }

   //Reset position to zero
   range.moveStart("character",internalGetPos() * -1);
   range.moveEnd("character",internalGetPos() * -1);

   //Move to demanded position
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

function SearchAndSelect(text, searchPos, moveCursorToEndOfFoundText) {
	if (text == "") {return;}

   //Ensure variable is a boolean
   moveCursorToEndOfFoundText = moveCursorToEndOfFoundText == true;

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

   if (moveCursorToEndOfFoundText) {
      range.collapse();
      range.moveStart("character", text.length);
      range.select();
   }
}