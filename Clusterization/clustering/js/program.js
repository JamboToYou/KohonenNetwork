var cellSize = 10;
var mapSize = 250;
var mapDelay = 60;
var textColor = "rgb(0, 0, 0)";
drawingCtx.font = "20px Arial";

function calcColor(val) {
	let r = val > 0.5 ? 255 : Math.sqrt(val * 2) * 255;
	let g = Math.sqrt(1 - Math.abs(1 - 2 * val)) * 255;
	let b = val < 0.5 ? 255 : Math.sqrt((1 - val) * 2) * 255;
	return `rgb(${r}, ${g}, ${b})`;
}

function calcColor1(val) {
	let r = val > 0.5 ? 255 : val * 2 * 255;
	let g = (1 - Math.abs(1 - 2 * val)) * 255;
	let b = val < 0.5 ? 255 : (1 - val) * 2 * 255;
	return `rgb(${r}, ${g}, ${b})`;
}

function drawMaps(nn) {
	for (let i = 0; i < nn.NeuronWeights.length; i++) {
		for (let j = 0; j < nn.NeuronWeights[i].length; j++) {
			for (let val = 0; val < nn.NeuronWeights[i][j].Coords.length; val++) {
				drawingCtx.fillStyle = calcColor(nn.NeuronWeights[i][j].Coords[val]);
				drawingCtx.fillRect((j * cellSize + (val % 3) * (mapSize + mapDelay)), (i * cellSize + Math.floor(val / 3) * (mapSize + mapDelay)), cellSize, cellSize);
				drawingCtx.fillStyle = textColor;
				drawingCtx.fillText(nn.Titles[val], (val % 3) * (mapSize + mapDelay), Math.floor(val / 3) * (mapSize + mapDelay) + 30 + mapSize, mapSize);
			}
		}
	}
}

fileInput.onchange = function () {
	file = fileInput.files[0];
	var reader = new FileReader();

	reader.onload = function() {
		var maps = JSON.parse(reader.result);
		drawMaps(maps);
	}
	reader.readAsText(file);
}

// drawingCtx.fillStyle = 'rgb(255, 0, 100)';
// drawingCtx.fillStyle = 'hsl(315, 100%, 50%)';
// drawingCtx.fillRect(3, 3, 30, 30);