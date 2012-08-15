// JavaScript Document
addEvent();

	
function createDlgDiv(){
	var top = $(window).height()/2 - 150;
	var left = $(window).width()/2 - 250;
	var div = document.createElement("div");
	$(div).attr("class","dialog");
	$(div).css({
		"position"	:	"fixed",
		"display"	:	"block", 
		"z-index"	:	"10003",
		"left"		:	left +"px",
		"top"		:	top + "px",
		"background":	"white"
	});
	return div;
}

function resizePosition(){
	var top = $(window).height()/2 - $(".dialog").height()/2;
	var left = $(window).width()/2 - $(".dialog").width()/2;
	$(".dialog").css({
		"top": top + "px",
		"left": left + "px"
	});
	if(top < 0){
		$(".dialog").css({"position":"absolute","top":"0px"});
	}
}

function cancelDlg(){
    $(".dialog").remove();
    $('#over_div').hide();
    $('#join_box').hide();
}


function addEvent(){
	window.onresize = function (){
		resizePosition();
	};	
	}
	
	
//	$('.relation_list').each(function(){
	$(this).children('li').find('em').unbind("click");
	$(this).children('li').find('em').bind("click",injoinxiquDlg);
	function injoinxiquDlg(){
		cancelDlg();
		var div = createDlgDiv();
		var postData = this.innerHTML;//此处内容需依据实际需要作调整，而不是当前值
		$.ajax({
			type:"POST",
			timeout:100,
			url:"http://127.0.0.1",
			data: postData,
			success:function(data){
				
			},error:function(XMLHttpRequest,textStatus,errorThrown){
				
			},complete:function(){
				//以下应是向后台请求的内容,应写在SUCCESS里，将服务器返回的数据填充正确并显示弹出框
				
				div.innerHTML = '<div class="msg_box w600">';
				div.innerHTML+='<div class="msg_title"><h2>加入兴趣群组</h2><span class="msg_close"><a href="#"><img src="images/msg_close.gif" width="50" height="40" /></a></span></div>';
				div.innerHTML+='<div class="msg_content"><div class="msg_c_l">';
				div.innerHTML+='<span><img src="images/MM01.gif" width="152" height="152" /></span>';
				div.innerHTML+='<span>如果我说我喜欢编程会不会不合群</span>';
				div.innerHTML+='</div><div class="msg_c_r w326">';
				div.innerHTML+='<span>和群组里面先到的同学说点什么吧：</span>';
				div.innerHTML+='<span><textarea name="textarea" class="textarea3" style="width:315px; height:120px;">个音符里的感情</textarea></span>';
				div.innerHTML+='<span class="btn_span"><a class="redlink" href="#">加  入</a></span>';
				div.innerHTML+='</div></div></div>';
				document.body.appendChild(div);
				resizePosition();
			}
		});
	}
		
	//	});
		
		

	$('.self_err').unbind("click");
	$('.self_err').bind("click",delete_xqDlg);
	function delete_xqDlg(){
		
		cancelDlg();
		var div = createDlgDiv();
		var postData = this.innerHTML;//此处内容需依据实际需要作调整，而不是当前值
		$.ajax({
			type:"POST",
			timeout:100,
			url:"http://127.0.0.1",
			data: postData,
			success:function(data){
				
			},error:function(XMLHttpRequest,textStatus,errorThrown){
				
			},complete:function(){
				//以下应是向后台请求的内容,应写在SUCCESS里，将服务器返回的数据填充正确并显示弹出框
				
				var strhtml = '<div class="msg_box w440">';
				strhtml+='<div class="msg_title"><h2>加入兴趣群组</h2><span class="msg_close"><a href="#"><img src="images/msg_close.gif" width="50" height="40" /></a></span></div>';
				strhtml+='<div class="msg_content"><div class="msg_c_l">';
				strhtml+='<span><img src="images/MM01.gif" width="152" height="152" /></span>';
				strhtml+='<span>如果我说我喜欢编程会不会不合群</span>';
				strhtml+='</div><div class="msg_c_r w155">';
				strhtml+='<span class="span_c"><p>如果我说我喜欢编程会不会不合群</p></span>';
				strhtml+='<span class="btn_span"><a class="redlink" href="#">加  入</a></span>';
				strhtml+='</div></div></div>';
				div.innerHTML=strhtml;
				document.body.appendChild(div);
				resizePosition();
			}
		});
	}




	$(function () {
	    var top = $(window).height() / 2 - 150;
	    var left = $(window).width() / 2 - 250;
	    $('#box_msg').attr("class", "dialog");
	    $('#box_msg').css({
	        "position": "fixed",
	        "z-index": "10003",
	        "left": left + "px",
	        "top": top + "px",
	        "background": "white"
	    });
	    $('#box_msg_addfans').attr("class", "dialog");
	    $('#box_msg_addfans').css({
	        "position": "fixed",
	        "z-index": "10003",
	        "left": left + "px",
	        "top": top + "px",
	        "background": "white"
	    });
	});
	