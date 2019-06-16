fileInput.onchange = function () {
	file = fileInput.files[0];
	var reader = new FileReader();

	reader.onload = function() {
		var clusters = JSON.parse(reader.result);
		displayClusters(clusters);
	}
	reader.readAsText(file);
}

// drawingCtx.fillStyle = 'rgb(255, 0, 100)';
// drawingCtx.fillStyle = 'hsl(315, 100%, 50%)';
// drawingCtx.fillRect(3, 3, 30, 30);