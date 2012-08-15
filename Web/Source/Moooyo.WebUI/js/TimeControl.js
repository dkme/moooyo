// JavaScript Document
<!--
 var Timer	= new Date();
 var years	= Timer.getFullYear();
 var months	= (Timer.getMonth()+1);
 var days	= Timer.getDate();
 var weeks	= Timer.getDay();
 var Rec_List	= "";
 Rec_List	= ",20020202,20020608,20020815,20021219,20010125,20011105,2001706,20030715,20031203,20030416,";
 
 function GetDays(the_year,the_month){
	 var Max_day = 31;
	 if (the_month==4 || the_month==6 || the_month==9 || the_month==11){
	 	Max_day = 30;
	 }else if(the_month==2){
	 	if(the_year%400==0){
			Max_day = 29;
		}else if(the_year%100==0){
			Max_day = 28;
		}else if(the_year%4==0){
			Max_day = 29;
		}else{
			Max_day = 28;
		}
	 }
	 return Max_day;
 }
 
 function ShowPlan(the_year,the_month,the_day){
	var selDate=document.getElementById("selDate");
 	var thisDate = the_year + "-" + (the_month+1) + "-" + the_day;
 	selDate.value=thisDate;
 	selDate.innerText=thisDate;
	LayerDate.style.visibility='hidden';
 }
 
 function HideDate(){
 	if(LayerDate.style.visibility!='hidden'){
		LayerDate.style.visibility='hidden';
	}
 }
 
 function Calendar(the_year,the_month){
 	var i = 0;
 	var FontColor;
 	var DateStr;
 	var New_Date = new Date(the_year,the_month,1)
 	var the_week = New_Date.getDay();
 	var Max_day  = GetDays(the_year,the_month+1)
	var dummy = 7-(the_week+Max_day)%7;
 	var Cal_str  = "";
 	Cal_str += "<table align=center width='100%'  style='border:1px solid #FF9900' cellpadding='1' class='timetable' cellspacing='1' bgcolor='#FF9900'>";
	Cal_str += "<tr bgcolor='#ffcc99'>";
	Cal_str += "<td width='14%' align=center>星期日</td>";
	Cal_str += "<td width='14%' align=center>星期一</td>";
	Cal_str += "<td width='14%' align=center>星期二</td>";
	Cal_str += "<td width='14%' align=center>星期三</td>";
	Cal_str += "<td width='14%' align=center>星期四</td>";
	Cal_str += "<td width='14%' align=center>星期五</td>";
	Cal_str += "<td width='14%' align=center>星期六</td>";
	Cal_str += "</tr><tr>\n";
	for(i=0;i<the_week;i++){
		Cal_str += "<td valign='top' align=center bgcolor='#ffffff' onmouseout=this.bgColor='#ffffff' onmouseover=this.bgColor='#f2f8ff'>&nbsp;</td>\n";
	}
	for(i=1;i<=Max_day;i++){
		FontColor = ((i+the_week)%7==1||(i+the_week)%7==0)?"red":"black"
		DateStr = "," + the_year + (the_month<9?("0"+(parseInt(the_month)+1)):(parseInt(the_month)+1)) + (i<10?("0"+i):i) + ",";
		Cal_str += "<td valign='top' align='center' bgcolor=" + (Rec_List.search(DateStr)!=-1?"#FFCC33":"white") + ((i==days&&the_year==years&&the_month==months-1)?" background=image/now.gif":"") + " onmouseout=this.bgColor='" + (Rec_List.search(DateStr)!=-1?"#FFCC33":"#ffffff") + "' onmouseover=this.bgColor='#f2f8ff'>"
		Cal_str += "<a href=javascript:ShowPlan(" + the_year + "," + the_month + "," + i + ") style='color: " + FontColor + ";'>" + i + "</a></td>\n";
		if((the_week+i)%7==0 && i!=Max_day) Cal_str += "</tr><tr>\n";
	}
	if(dummy < 7){
		for(i=1;i<=dummy;i++){
			Cal_str += "<td valign='top' align='center' bgcolor='#ffffff' onmouseout=this.bgColor='#ffffff' onmouseover=this.bgColor='#f2f8ff'>&nbsp;</td>\n";
		}
	}
	Cal_str += "</tr></table>";
	return(Cal_str);
 }
 
function DateChange(mode){
	var theYear=parseInt(ShowYear.innerText);
	var theMonth=parseInt(ShowMon.innerText);
	if(mode){
		theMonth++;
		if(theMonth>=13){
			theYear++;
			theMonth=1;
		}
	}else{
		theMonth--;
		if(theMonth<=0){
			theYear--;
			theMonth=12;
		}
	}
	ShowYear.innerText=theYear;
	ShowMon.innerText=theMonth;
	Cal_Tab.innerHTML=Calendar(theYear,theMonth-1);
}
function YearChange(){
	var theYear;
	theYear=prompt("Please input the year: (0 - 3000)",ShowYear.innerText);
	if(theYear==null || theYear=="") return false;
	theYear=parseInt(theYear);
	theMon=parseInt(ShowMon.innerText);
	if((theYear+"a")=="NaNa" || theYear>3000 || theYear<0){
		alert("输入错误！");
		return false;
	}else{
		ShowYear.innerText=theYear;
		ShowMon.innerText=theMon;
		Cal_Tab.innerHTML=Calendar(theYear,theMon-1);
	}
	setTimeout("LayerDate.style.visibility='visible'",10);
}
function MonChange(){
	var theMon;
	theMon=prompt("Please input the Month: (1 - 12)",ShowMon.innerText);
	if(theMon==null || theMon=="") return false;
	theMon=parseInt(theMon);
	theYear=parseInt(ShowYear.innerText);
	if((theMon+"a")=="NaNa" || theMon>12 || theMon<1){
		alert("输入错误！");
		return false;
	}else{
		ShowYear.innerText=theYear;
		ShowMon.innerText=theMon;
		Cal_Tab.innerHTML=Calendar(theYear,theMon-1);
	}
	setTimeout("LayerDate.style.visibility='visible'",10);
}
function showCalendar(){
	event.cancelBubble = true;
	LayerDate.style.top = event.srcElement.offsetTop + event.srcElement.offsetHeight + 2;
	LayerDate.style.left = event.srcElement.offsetLeft - 150;
	if(parseInt(LayerDate.style.left)<0) LayerDate.style.left = 0;
	LayerDate.style.visibility = LayerDate.style.visibility=='hidden'?'visible':'hidden';
	return false;
}