var putDot = polymorph(
	function(x, y, color) {
		drawingCtx.fillStyle = `rgb(` +
			`${color.r},` +
			`${color.g},` +
			`${color.b})`;
		drawingCtx.fillRect(x, y, dotSize, dotSize);
	},

	function(x, y) {
		drawingCtx.fillRect(x, y, dotSize, dotSize);
	},

	function(dot) {
		drawingCtx.fillStyle = `rgb(` +
			`${dot.color.r},` +
			`${dot.color.g},` +
			`${dot.color.b})`;
		drawingCtx.fillRect(dot.x, dot.y, dotSize, dotSize);
	}
)

function setCurrentColor(hue) {
	drawingCtx.fillStyle = `hsl(${hue}, 100%, 50%)`;	
}

function putBigDot(dot) {
	drawingCtx.fillRect(
		dot.x - dotSize,
		dot.y - dotSize,
		dotSize * 2,
		dotSize * 2);
}