function alertbox(str){
		addscreen();
		if(str.length < 20) str = '<p style="margin:0;padding:10px 0">'+str+'</p>'; 
		document.getElementById('MessageContent').innerHTML = str;
		document.getElementById('Mes-Btn').innerHTML = '<a id="Exit">确定</a>';
		document.getElementById('Exit').onclick = function(){
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
	}
	function messagebox(str){
		addscreen();
		if(str.length < 20) str = '<p style="margin:0;padding:10px 0">'+str+'</p>'; 
		document.getElementById('MessageContent').innerHTML = str;
		document.getElementById('Mes-Btn').innerHTML = '<a id="Exit">我知道了</a>';
		document.getElementById('Exit').onclick = function(){
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
	}
	function confirmbox(str,fun){
		addscreen();
		if(str.length < 20) str = '<p style="margin:0;padding:10px 0">'+str+'</p>'; 
		document.getElementById('MessageContent').innerHTML = str;
		document.getElementById('Mes-Btn').innerHTML = '<a id="Exit">取消</a><a id="Ensure">确定</a>';
		document.getElementById('Exit').onclick = function(){
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
		document.getElementById('Ensure').onclick = function(){
			fun();
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
	}
	function promptbox(str,fun){
		addscreen();
		document.getElementById('MessageContent').innerHTML = '<textarea id="variable" class="Mes-Fill" placeholder="'+str+'"/>';
		document.getElementById('Mes-Btn').innerHTML = '<a id="Exit">取消</a><a id="Ensure">提交</a>';
		document.getElementById('Exit').onclick = function(){
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
		document.getElementById('Ensure').onclick = function(){
			var variable = document.getElementById('variable').value;
			fun(variable);
			document.body.removeChild(document.getElementById('ScreenCover'));
		}
	}
	function addscreen(){
		if(document.getElementById('ScreenCover'))
			document.body.removeChild(document.getElementById('ScreenCover'));
		var screen_cover = document.createElement("div");
		screen_cover.id = 'ScreenCover';
		document.body.appendChild(screen_cover);
		var screen_height = document.documentElement.clientHeight;
		var screen_width = document.documentElement.clientWidth;
		screen_cover.style.height = screen_height+'px';
		screen_cover.innerHTML = '<div id="MessageBox"><div id="MessageContent"></div><div id="Mes-Btn"></div></div>';
		var MessageBox = document.getElementById('MessageBox');
		var box_width = MessageBox.clientWidth;
		var box_height = MessageBox.clientHeight;
		MessageBox.style.left =( screen_width - box_width ) / 2 + 'px';
		MessageBox.style.top =( screen_height - box_height ) / 2  + 'px';
	}