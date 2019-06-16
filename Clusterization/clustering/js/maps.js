function draww(){
	// var cnt = 0;
	// for (const cluster of clusters) {
	// 	cnt += cluster.Dots.length;
	// }
	document.body.appendChild("<canvas id='sample' style=\"border: 2px solid black\">Обновите браузер</canvas>");
	var ctx = document.getElementById("sample").getContext("2d");
	ctx.fillRect(3,4,100,100);
}