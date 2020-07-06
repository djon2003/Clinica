//Affiche/cache une partie d'une page
function ShowHidePart(ObjId, type) {
   if (type === "undefined") {
      type = 0;
   }

	var Obj = document.getElementById(ObjId);
   switch (type) {
   case 0 :
      if ((!Obj.style.display) || Obj.style.display == "") {Obj.style.display = "none";}
      else {Obj.style.display = "";}  
      break;
   case 1 :
      if ((!Obj.style.visibility) || Obj.style.visibility == "") {Obj.style.visibility = "hidden";}
      else {Obj.style.visibility = "";}
      break;
   }
}

function showPage(page) {
   parent.table.selectInTOC(page);
}

function getAbsPosition(element) {
   var pos = {top:0, left:0};
   while(element != document.body) {
      pos.top += element.offsetTop;
      pos.left += element.offsetLeft;
      element = element.parentNode;
   }

   return pos;
}

function getWindowSize() {
   var winW = 630, winH = 460;
   if (document.body && document.body.offsetWidth) {
    winW = document.body.offsetWidth;
    winH = document.body.offsetHeight;
   }
   if (document.compatMode=='CSS1Compat' &&
       document.documentElement &&
       document.documentElement.offsetWidth ) {
    winW = document.documentElement.offsetWidth;
    winH = document.documentElement.offsetHeight;
   }
   if (window.innerWidth && window.innerHeight) {
    winW = window.innerWidth;
    winH = window.innerHeight;
   }

   return {height: winH, width:winW};
}


var lastSelected = null;

function selectInTOC(page) {
   var selectedElement = document.getElementById('grow-' + page);
   var newSelected = [];
         
   if (selectedElement != null) {
         var pos = getAbsPosition(selectedElement);
         window.scroll(pos.left, pos.top - getWindowSize().height / 2);
         selectedElement.parentElement.style.background = "#DDDDDD";

         newSelected = selectParentTOC(page).reverse();

         newSelected.push(selectedElement.parentElement);
   }

   if (lastSelected != null) {
      for(var i = 0; i < lastSelected.length; i++) {
         if (i >= newSelected.length || newSelected[i] != lastSelected[i]) {
            lastSelected[i].style.background = "";
         }
      }
   }

   lastSelected = newSelected;
}

function selectParentTOC(page) {
   var newSelected = [];
   
   if (page.lastIndexOf("-") == -1) {return newSelected;}

   var curPage = page.substring(0, page.indexOf(".")).substring(0, page.lastIndexOf("-"));
   if (curPage == "") {return newSelected;}

   curPage += ".html";
   
   var group = document.getElementById(curPage);
   var grow = document.getElementById('grow-' + curPage);
   grow.innerHTML = "-";
   grow.parentElement.style.background = "#DDDDDD";
   group.style.display = "";

   newSelected.push(grow.parentElement);

   newSelected = newSelected.concat(selectParentTOC(curPage));

   return newSelected;
}
